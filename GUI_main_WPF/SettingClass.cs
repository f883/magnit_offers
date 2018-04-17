using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_main_WPF
{
    [Serializable]
    public class Settings
        // класс нужен для хранения настроек, установленных пользователем
    {
        public string CityName;
        public string CityID;
        public string RegionName;
        public string RegionID;
        public int CityInListIndex;
        public int RegionInListIndex;

        public Settings()
        {  }

        public Settings(string cityName, string cityID, string regionName, string regionID)
        {
            this.CityName = cityName;
            this.CityID = cityID;
            this.RegionName = regionName;
            this.RegionID = regionID;
        }
    }
}
