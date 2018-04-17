using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threadingTest
{
    class MyThread
    {
        public Thread thrd;
        public int Count { set; get; }

        public MyThread(string name)
        {
            Count = 0;
            thrd = new Thread(this.run);
            thrd.Name = name;
            thrd.Start();
        }

        public void run()
        {
            Console.WriteLine("Start "+thrd.Name);
            do
            {
                Thread.Sleep(500);
                Console.WriteLine("In thread " + thrd.Name + ", count = " + Count);
                Count++;
            } while (Count < 10);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Основной поток стартовал.");
            MyThread mt1 = new MyThread("Потомок #1");
            MyThread mt2 = new MyThread("Потомок #2");
            MyThread mt3 = new MyThread("Потомок #3");
            do
            {
                Console.Write(".");
                Thread.Sleep(100);
            } while (mt1.Count != 10 || mt2.Count != 10 || mt3.Count != 10);
            Console.WriteLine("Основной поток завершен");
        }
    }

}
