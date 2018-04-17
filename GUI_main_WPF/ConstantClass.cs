using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_main_WPF
{
    class Constant
        // все константы хранятся здесь
    {
        internal static string DATA_DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Magnit_Actions";
        internal static string DATA_DIR_NAME = DATA_DIR + @"\_offer_pictures";
        internal static string SMALL_PICS_DIR_NAME = DATA_DIR_NAME + @"\" + "_pics"; // нужна для хранения маленьких картинок из карточек
        internal static string XML_DATA_BASE_NAME = DATA_DIR + @"\offers.xml";
        internal static string XML_SETTINGS_FILE_NAME = DATA_DIR + @"\settings.xml";
        internal static string XML_REGIONS_AND_CITIES_BASE_NAME = DATA_DIR + @"\regions.xml";
    }
}
