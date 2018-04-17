using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace GUI_main_WPF
{
    class Files
        // здесь находятся все взаимодействия программы с внешними файлами и базами данных
    {
        public static void RemoveNonUsingPics()
        {
            string[] pics = { };
            try
            {
                pics = Directory.GetFiles(Constant.SMALL_PICS_DIR_NAME);
            }
            catch
            { }
            foreach (string pic in pics)
            {
                try
                {
                    File.Delete(pic);
                }
                catch (Exception e)
                { }
            }
        }

        public static void SaveOfferBase(Offer[] personArray) // сохранение информации в базу данных
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Offer[]));
            string dir = Constant.XML_DATA_BASE_NAME;
            using (FileStream fs = new FileStream(dir, FileMode.Create))
            {
                formatter.Serialize(fs, personArray);
            }
        }

        public static Offer[] ReadOfferBase() // загрузка информации из базы данных
        {
            Offer[] offerArray = new Offer[0];
            XmlSerializer formatter = new XmlSerializer(typeof(Offer[]));
            string dir = Constant.XML_DATA_BASE_NAME;
            try
            {
                using (FileStream fs = new FileStream(dir, FileMode.Open))
                {
                    offerArray = (Offer[])formatter.Deserialize(fs);
                }
            }
            catch // если массива нет, то делать ничего не надо
            { }
            return offerArray;
        }

        public static void SaveSettings(Settings settings) // сохранение информации в базу данных
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));
            string dir = Constant.XML_SETTINGS_FILE_NAME;
            using (FileStream fs = new FileStream(dir, FileMode.Create))
            {
                formatter.Serialize(fs, settings);
            }
        }

        public static Settings ReadSettings() // загрузка информации из базы данных
        {
            Settings settings = new Settings();
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));
            string dir = Constant.XML_SETTINGS_FILE_NAME;
            try
            {
                using (FileStream fs = new FileStream(dir, FileMode.Open))
                {
                    settings = (Settings)formatter.Deserialize(fs);
                }
            }
            catch // если массива нет, то делать ничего не надо
            { }
            return settings;
        }

        public static Region[] ReadCityDataBase()
        {
            Region[] regs = new Region[0];
            XmlSerializer formatter = new XmlSerializer(typeof(Region[]));
            string dir = Constant.XML_REGIONS_AND_CITIES_BASE_NAME;
            try
            {
                using (FileStream fs = new FileStream(dir, FileMode.Open))
                {
                    regs = (Region[])formatter.Deserialize(fs);
                }
            }
            catch
            {
                MessageBox.Show("Не найдена база городов, работа приложения невозможна.");
            }
            return regs;
        }
    }
}
