using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = new Host();
            Console.WriteLine(" --> Enter 's' to start");
            //Console.WriteLine(" --> Enter 'e' to stop");
            //Console.WriteLine(" --> Enter 'd' to Dispose");
            Console.WriteLine(" --> Enter 'q' to quit");
      
            string w = "n";
            while (w != "q")
            {
                if (w == "s")
                {
                    w = "n";
                    Console.WriteLine("wait for it...");
                    host.Start();
                }
                
                if (w == "e")
                {
                    w = "n";
                    host.Stop();
                }
                if (w == "d")
                {
                    w = "n";
                    host.Start();
                    host.Dispose();
                }
                
                w = Console.ReadLine();
            }
        }
    }
}
