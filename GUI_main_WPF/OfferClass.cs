using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_main_WPF
{
    [Serializable]
    public class Offer
    {
        public string header { get; set; }
        public string body { get; set; }
        public string picDirectory { get; set; }
        public string largePicDirectory { get; set; }
        public string largeBody { get; set; }

        public Offer()
        {}

        public Offer(string header, string body, string largeBody, string largePicDir)
        {
            this.header = header;
            this.body = body;
            //this.href = href;
            this.picDirectory = "";
            this.largePicDirectory = largePicDir;
            this.largeBody = largeBody;
        }

        public void Show()
        {
            Console.WriteLine("header:" + this.header);
            Console.WriteLine("body:" + this.body);
            //Console.WriteLine("href:" + this.href);
            Console.WriteLine("picture directory:" + this.picDirectory);
        }
    }
}
