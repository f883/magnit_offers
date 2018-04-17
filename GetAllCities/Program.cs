using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Web;

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

    [Serializable]
    public class City
    {
        public string cityName;
        public string cityID;

        public City()
        { }
    }

    [Serializable]
    public class Region
    {
        public string regionName;
        public string regionID;
        public City[] city;

        public Region()
        { }
    }

    class Program
    {
        public static string Downloader(string cityID)
        {
            Console.WriteLine("Start downloading...");
            // здесь будет массив куки, пользователь будет выбирать город
            string[] cookie3 =
            {
                "MG_REGION_ID",
                ".magnit-info.ru",
                cityID
            };

            string[][] cookies = {cookie3}; // установка кукисов для Перми
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

            string text = Encoding.GetEncoding(1251).GetString(bytes);
            text = text.Replace("&quot;", "'"); // замена кода ковычек на ковычки

            Console.WriteLine("Done.");
            return text;
        }

        public static void SaveSettings(Region[] settings) // сохранение информации в базу данных
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Region[]));
            using (FileStream fs = new FileStream(@"C:\Users\duck\MEGA\tiny_files\study\regions_and_cities.xml", FileMode.Create))
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
                    settings = (RegionAndCity[])formatter.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return settings;
        }

        static void Main()
        {
            Region[] newRegions = new Region[0];
            RegionAndCity[] regs = ReadSettings();
            foreach (var reg in regs)
            {
                Console.WriteLine("###############################################");
                Console.WriteLine(reg.regionName);
                Console.WriteLine("###############################################");
                string text = Downloader(reg.regionID);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(text);

                
                // достать из страницы набор городов

                var firstNode = htmlDoc.DocumentNode.SelectSingleNode("descendant::div[attribute::class=\"choose-city-popup__columns\"]");
                // получена куча городов
                //Console.WriteLine(firstNode.InnerHtml);

                City[] cities = new City[0];
                var nodes = firstNode.SelectNodes("descendant::li");
                foreach (var node in nodes)
                {
                    City newCity = new City();
                    
                    newCity.cityName = node.InnerText;
                    try
                    {
                        newCity.cityID = node.SelectSingleNode("descendant::a").Attributes["data-id"].Value;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Array.Resize(ref cities, cities.Length + 1);
                    cities[cities.Length - 1] = newCity;
                    Console.WriteLine(cities[cities.Length - 1].cityName);
                }
                
                Region newReg = new Region();
                newReg.regionID = reg.regionID;
                newReg.regionName = reg.regionName;
                newReg.city = cities;

                Array.Resize(ref newRegions, newRegions.Length + 1);
                newRegions[newRegions.Length - 1] = newReg;
            }
            SaveSettings(newRegions);
            Console.WriteLine("All done.");
            Console.ReadLine();
        }
    }
}