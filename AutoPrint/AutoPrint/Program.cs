using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPrint
{
    class Program
    {
        static void Main()
        {
            Console.Title = "AutoPrint";
            if (!File.Exists("Settings.xml"))
            {
                Settings.Create();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSettings.xml file created - Setup Settings.xml before starting program.\n\n");
                Console.ResetColor();
                Console.WriteLine("Press enter if read.");
                Console.ReadLine();
            }
            else
            {
                Settings.Load();

                var printer = new Printer();
                printer.Start();
            }
        }
    }
}
