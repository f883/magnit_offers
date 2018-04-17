using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace simpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            char p1 = 's';
            char p2 = '1';
            char p3 = 'ы';


            Console.WriteLine(Char.IsLetter(p1));
            Console.WriteLine(Char.IsLetter(p2));
            Console.WriteLine(Char.IsLetter(p3));


            Console.Read(); // перенести в модуль поиска
        }
    }
}
