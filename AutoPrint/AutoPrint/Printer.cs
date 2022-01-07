using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PDFtoPrinter;

namespace AutoPrint
{
    class Printer
    {
        // Startpoint of Printer class.
        public void Start()
        {
            while (true)
            {
                if (CheckPath())
                {
                    string[] files = Directory.GetFiles(Settings.PATH);
                    var queue = new List<string>();

                    foreach (var file in files)
                    {
                        if (CheckEnding(file))
                        {
                            queue.Add(file);
                        }
                    }
                    Display(queue);

                    if (queue.Count != 0)
                    {
                        Print(queue[0]);
                        Thread.Sleep(2000);
                        while (File.Exists(queue[0]))
                        {
                            try
                            {
                                File.Delete(queue[0]);
                            }
                            catch (Exception)
                            { 
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error: Path doesn't exist - Press enter if corrected.");
                    Console.ReadLine();
                }
            }
        }

        // Displays all Stats and printer status.
        void Display(List<string> queue)
        {
            Console.Clear();
            Console.WriteLine($"///////////////");
            Console.WriteLine($"Queued files: {queue.Count}\n");

            foreach (var file in queue)
            {
                Console.WriteLine(file);
            }
        }

        // Print file.
        void Print(string file)
        {
            var printer = new PDFtoPrinterPrinter();
            printer.Print(new PrintingOptions(Settings.PRINTER_NAME, file));
        }

        // Check for all accepted file-endings.
        bool CheckEnding(string file)
        {
            foreach (var ending in Settings.ENDINGS)
            {
                if (file.Split('.')[1] == ending)
                {
                    return true;
                }
            }
            return false;
        }

        // Check if the directory path exists.
        bool CheckPath()
        {
            if (Directory.Exists(Settings.PATH))
            {
                return true;
            }
            return false;
        }
    }
}
