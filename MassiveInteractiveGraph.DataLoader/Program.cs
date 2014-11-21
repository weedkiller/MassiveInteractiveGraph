using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveInteractiveGraph.DataLoader.ServiceReference1;

namespace MassiveInteractiveGraph.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("testing...");
            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                try
                {
                    var service = new Service1Client();

                    var data = service.GetData(0);

                    Console.WriteLine(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
