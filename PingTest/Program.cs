using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> serversList = new List<string>();
            //serversList.Add("microsoft.com");
            //serversList.Add("google.com");
            serversList.Add("magnit-info.ru");
            //serversList.Add("192.168.1.1");


            using (TextWriter tw = new StreamWriter(@"C:\Users\duck\MEGA\tiny_files\study\ping_log.txt"))
            {
                Ping ping = new System.Net.NetworkInformation.Ping();

                PingReply pingReply = null;

                foreach (string server in serversList)
                {
                    pingReply = ping.Send(server);

                    if (pingReply.Status != IPStatus.TimedOut)
                    {
                        Console.WriteLine(server); //server
                        Console.WriteLine(pingReply.Address); //IP
                        Console.WriteLine(pingReply.Status); //Статус
                        Console.WriteLine(pingReply.RoundtripTime); //Время ответа
                        Console.WriteLine(pingReply.Options.Ttl); //TTL
                        Console.WriteLine(pingReply.Options.DontFragment); //Фрагментирование
                        Console.WriteLine(pingReply.Buffer.Length); //Размер буфера
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(server); //server
                        Console.WriteLine(pingReply.Status);
                        Console.WriteLine();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}