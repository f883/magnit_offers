using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_main_WPF
{
    [Serializable]
    public class City
    {
        public string cityName { get; set; }
        public string cityID { get; set; }

        public City()
        {
        }
    }

    [Serializable]
    public class Region
    {
        public string regionName { get; set; }
        public string regionID { get; set; }
        public City[] city { get; set; }

        public Region()
        {
        }
    }
}