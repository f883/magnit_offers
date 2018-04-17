using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HtmlAgilityPack;

namespace CookiesTest
{
    [Serializable]
    public class RegionAndCity
    {
        public string regionName;
        public string regionID;
        public string cityName;
        public string cityID;

        public RegionAndCity()
        { }
    }

    class Program
    {
        public static byte[] Downloader()
        {
            // здесь будет массив куки, пользователь будет выбирать город
            string[] cookie1 =
            {
                "MG_CITY_ID",
                ".magnit-info.ru",
                "4341"
            };
            string[] cookie2 =
            {
                "MG_CITY_NAME",
                ".magnit-info.ru",
                "%CF%E5%F0%EC%FC"
            };
            string[] cookie3 =
            {
                "MG_REGION_ID",
                ".magnit-info.ru",
                "830"
            };
            string[] cookie4 =
            {
                "MG_REGION_NAME",
                ".magnit-info.ru",
                "%CF%E5%F0%EC%F1%EA%E8%E9+%EA%F0%E0%E9"
            };

            string[][] cookies = { cookie1, cookie2, cookie3, cookie4 }; // установка кукисов для Перми
            string url = @"http://magnit-info.ru/buyers/actions/shops/";
            
            CookieCollection cookieCollection = new CookieCollection();
            foreach (string[] cookie in cookies)
            {
                Cookie c = new Cookie(); // Создали экземпляр Куки
                c.Name = cookie[0];
                c.Value = cookie[2];// Сделали из него куки для запроса
                c.Domain = cookie[1];
                c.Expires = new DateTime(2022, 3, 23); // Если надо, ставим дату истечения
                cookieCollection.Add(c);
            }

            CookieContainer cookieContainer = new CookieContainer(); // Инициализированный контейнер Куки для нашего запроса
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url); // Наш запрос

            cookieContainer.Add(cookieCollection); // Добавили в этот контейнер ранее созданный куки
            //request.CookieContainer = new CookieContainer(); // хз зачем
            request.CookieContainer = cookieContainer; // Прицепили к нему куки.  
            
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();// Получаем ответ
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            
            byte[] bytes = new byte[0];
            int temp = (byte)ReceiveStream.ReadByte();

            while (temp != -1) // можно оптимизировать, если брать не по одному байту, а по несколько
            {
                Array.Resize(ref bytes, bytes.Length + 1);
                bytes[bytes.Length - 1] = (byte)temp;
                temp = ReceiveStream.ReadByte();
            }
            return bytes;
        }

        public static void SaveSettings(RegionAndCity[] settings) // сохранение информации в базу данных
        {
            XmlSerializer formatter = new XmlSerializer(typeof(RegionAndCity[]));
            using (FileStream fs = new FileStream(@"C:\Users\duck\MEGA\tiny_files\study\all_regions.xml", FileMode.Create))
            {
                formatter.Serialize(fs, settings);
            }
        }

        public static RegionAndCity[] ReadSettings() // загрузка информации из базы данных
        {
            RegionAndCity[] settings = new RegionAndCity[0];
            XmlSerializer formatter = new XmlSerializer(typeof(RegionAndCity[]));
            try
            {
                using (FileStream fs = new FileStream(@"C:\Users\duck\MEGA\tiny_files\study\all_regions.xml", FileMode.Open))
                {
                    settings = (RegionAndCity[]) formatter.Deserialize(fs);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return settings;
        }
        /*
        private static string[] GetFullAction(string href)
        {
            // скачивается полный текст акции
            // выделяется блок акции
            // выделяется ссылка на изображение
            // скачивается изображение, записывается локальная ссылка на него
            // выделяется полный текст новости
            // возвращается полный текст новости и локальная ссылка на картинку 

            string[] result = new string[2]; // 0-й элемент это текст акции, 1-й элемент это локальная ссылка на картинку
            string html = DownloadPage(href);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var temp = htmlDoc.DocumentNode.SelectSingleNode("//div[attribute::class=\"content-block\"]");
            result[0] = temp.InnerText;

            // добавить обработку ошибок
            string picHref = "";
            try
            {
                picHref = "http://magnit-info.ru" + temp.SelectSingleNode("//img[attribute::class=\"action-page-top-img\"]").Attributes["src"].Value;
            }
            catch (NullReferenceException)
            {
                picHref = "#zero#";
            }
            // ломается, если картинки нет
            result[1] = GetPictureReturnDir(picHref);

            return result;
        }*/

        static void Main()
        {
            Console.WriteLine("Start downloading...");
            byte[] bytes = Downloader();
            string dir = @"C:\Users\duck\MEGA\tiny_files\study\magnit_page.html";

            string text = Encoding.GetEncoding(1251).GetString(bytes);
            text = text.Replace("&quot;", "'"); // замена кода ковычек на ковычки

            RegionAndCity[] regsCities = new RegionAndCity[0];
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(text);

            var firstNode = htmlDoc.DocumentNode.SelectSingleNode("//select[attribute::class=\"choose-city-popup__select\"]");
            //Console.WriteLine(temp.InnerHtml);
            var nodes = firstNode.SelectNodes("//option[attribute::id=\"js-select-region\"]");
            foreach (var node in nodes)
            {
                RegionAndCity reg = new RegionAndCity();
                reg.regionName = node.InnerText;
                reg.regionID = node.Attributes["value"].Value;
                Array.Resize(ref regsCities, regsCities.Length + 1);
                regsCities[regsCities.Length - 1] = reg;
            }
            SaveSettings(regsCities);
            // выделение области с краями
            // в цикле: создание нового объекта типа регСити, выделение региона, добавление его в массив
            // сериализация полученного массива 

            // save to file 
            //File.WriteAllText(dir, text, Encoding.UTF8);

            //Console.ReadLine();
        }
    }
}