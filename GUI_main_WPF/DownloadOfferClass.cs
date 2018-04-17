using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using HtmlAgilityPack;
using System.Xml.Serialization;
using IronPython.Runtime;

/*
    Картинки сохраняются во временную папку под случайными названиями. 
    Это нужно, чтобы при обновлении не было коллизий, из-за которых возможны ошибки
*/

namespace GUI_main_WPF
{
    class DownloadOffer
    {
        private static LoadingWindow loadingWindow = null;

        /// <summary>
        /// Скачивает изображение, возвращает путь к этому изображению
        /// </summary>
        /// <returns></returns>
        private static string GetPictureReturnDir(string url)
        {
            WebClient wc = new WebClient();
            string randomFileName = Path.GetRandomFileName();
            randomFileName = randomFileName.Replace(".", "");
            string fullName = Constant.SMALL_PICS_DIR_NAME + @"\" + randomFileName + ".jpg";
            //MessageBox.Show(fullName);
            if (url == "#zero#")
                fullName = "#zero#";
            else
            {
                try
                {
                    wc.DownloadFile(url, fullName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);

                }
            }
            return fullName;
        }

        /// <summary>
        /// Скачивает и преобразует страницу в юникод
        /// </summary>
        /// <returns>Страница в виде строки</returns>
        private static string DownloadPage(string url) 
        {
            Settings stng = Files.ReadSettings();
            string[] cookie1 =
            {
                "MG_CITY_ID",
                ".magnit-info.ru",
                stng.CityID
            };
            string[] cookie2 =
            {
                "MG_REGION_ID",
                ".magnit-info.ru",
                stng.RegionID
            };

            string[][] cookies = { cookie1, cookie2}; // установка кукисов

            CookieCollection cookieCollection = new CookieCollection();
            foreach (string[] cookie in cookies)
            {
                Cookie c = new Cookie();
                c.Name = cookie[0];
                c.Value = cookie[2];
                c.Domain = cookie[1];
                c.Expires = new DateTime(2022, 3, 23); // дата истечения
                cookieCollection.Add(c);
            }

            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url); // запрос
            
            cookieContainer.Add(cookieCollection);
            request.CookieContainer = cookieContainer;

            HttpWebResponse res = null;
            try
            {
                res = (HttpWebResponse)request.GetResponse(); // ответ
            }
            catch
            {
                MessageBox.Show("Нет доступа к сети или сайт недоступен.");
                loadingWindow.Close();
                return "#error#";
            }
            
            Stream receiveStream = res.GetResponseStream();

            byte[] bytes = ReadFully(receiveStream);
            string text = Encoding.GetEncoding(1251).GetString(bytes);
            text = text.Replace("&quot;", "'"); // замена кода ковычек на ковычки
            return text;
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Получение данных полной версии новости
        /// </summary>
        private static void GetFullAction(string href, out string largeBody, out string largePicDir)
        {
            // Алгоритм работы: 
            // скачивается полный текст акции
            // выделяется блок акции
            // выделяется ссылка на изображение
            // скачивается изображение, записывается локальная ссылка на него
            // выделяется полный текст новости
            // возвращается полный текст новости и локальная ссылка на картинку 

            string html = DownloadPage(href);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var action = htmlDoc.DocumentNode.SelectSingleNode("//div[attribute::class=\"content-block\"]");
            largeBody = action.InnerText.Trim();
            
            string picHref;
            try
            {
                picHref = "http://magnit-info.ru" + action.SelectSingleNode("//img[attribute::class=\"action-page-top-img\"]").Attributes["src"].Value;
            }
            catch (NullReferenceException)
            {
                picHref = "#zero#";
            }
            largePicDir = GetPictureReturnDir(picHref);
        }
        
        private static bool IsElemInArray(string[] array, string elem)
        {
            foreach(string i in array)
            {
                if (i == elem)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Получение массива акций
        /// </summary>
        public static Offer[] GetArray(LoadingWindow lw)
        {
            Offer[] offerArray = { };
            loadingWindow = lw;
            Offer[] oldOffers = Files.ReadOfferBase();

            try // если папки нет, она создаётся. если есть, то ничего не делается
            {
                Directory.CreateDirectory(Constant.DATA_DIR_NAME); // нужна для хранения временных файлов
            }
            catch { }
            try
            {
                Directory.CreateDirectory(Constant.SMALL_PICS_DIR_NAME); // нужна для хранения изображений
            }
            catch { }

            int pageNumber = 0; // нужна для загрузки акций с нескольких страниц
            bool repeating = false; // нужна для проверки на повтор
            string[] hrefsWithoutRepeating = new string[0]; // нужна для хранения ссылок на уже загруженные акции

            Console.WriteLine("###### start loading ######");

            while (!repeating)
                {
                pageNumber++;
                string html = DownloadPage(@"http://magnit-info.ru/buyers/actions/shops/" + "?PAGEN_2=" + pageNumber);
                if (html == "#error#") // проверка на присутствие интернет соединения
                    return oldOffers;
                
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//div[attribute::class=\"promo-card__bottom promo-card__bottom_small\"]");
                
                foreach (var node in htmlNodes)
                {
                    string header = node.SelectSingleNode("*/div[attribute::class=\"promo-card__bottom-title\"]").InnerText;
                    string body = node.SelectSingleNode("*/p").InnerText;
                    string href = "http://magnit-info.ru" + node.SelectSingleNode("a").Attributes["href"].Value;

                    if (!IsElemInArray(hrefsWithoutRepeating, href))
                    {
                        Array.Resize(ref hrefsWithoutRepeating, hrefsWithoutRepeating.Length + 1);
                        hrefsWithoutRepeating[hrefsWithoutRepeating.Length - 1] = href;
                    }
                    else
                    {
                        repeating = true;
                        break;
                    }
                    string pattern = "//img[attribute::alt=\"" + header + "\"]";
                    string picHref = "http://magnit-info.ru/" + node.SelectSingleNode(pattern).Attributes["src"].Value;
                    
                    Offer newOffer = new Offer();
                    newOffer.header = header;
                    newOffer.body = body;
                    newOffer.largeBody = href;
                    newOffer.picDirectory = picHref;
                    // вместо подробного описания и локальной ссылки на картинку передаются ссылки на сайт
                    // это нужно для возможности распараллелить загрузку подробных описаний акций
                    // в блоке ниже ссылка на сайт заменяется локальной ссылкой или текстом
                    Array.Resize(ref offerArray, offerArray.Length + 1);
                    offerArray[offerArray.Length - 1] = newOffer;
                }
                Console.WriteLine("page #{0} loaded.", pageNumber);
            }

            Files.RemoveNonUsingPics();

            foreach (Offer offer in offerArray)
            {
                LoadImagesAndTextFullAction(offer);
            }

            /* // заготовка для многопоточности
            var thread = new Thread(LoadImagesAndTextFullAction);
            foreach (Offer offer in offerArray)
            {
                thread.Start((object)offer);
                thread = new Thread(LoadImagesAndTextFullAction);
            }
            while(thread.IsAlive)
            { }
            */

            Files.SaveOfferBase(offerArray);
            Console.WriteLine("###### loading done ######");
            return offerArray;
        }

        private static void LoadImagesAndTextFullAction(object obj)
        {
            if (obj.GetType() != typeof(Offer))
                return;
            Offer offer = (Offer)obj;
            offer.picDirectory = GetPictureReturnDir(offer.picDirectory);
            string largeBody, largePicDirectory;
            GetFullAction(offer.largeBody, out largeBody, out largePicDirectory);

            if (largePicDirectory == "#zero#") // обнуление подробного описания
                largeBody = "#zero#"; // если объявление баганое, парсер может ухватить 
            // лишний текст из других акций, из-за чего будут баги при поиске

            offer.largeBody = largeBody;
            offer.largePicDirectory = largePicDirectory;
            Console.WriteLine("action \"{0}\" loaded.", offer.header);
        }
    }
}