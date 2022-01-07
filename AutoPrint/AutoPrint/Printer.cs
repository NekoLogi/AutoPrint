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
        static string FILE { get; set; }

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
                        FILE = file;
                        if (CheckEnding(FILE))
                        {
                            queue.Add(FILE);
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
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error: Path doesn't exist - Press enter if corrected.");
                    Console.ReadLine();
                }
            }
        }

        // Displays what is in the queue.
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
                if (file.Split('.')[1] == "pdf")
                {
                    return true;
                }
                else if (file.Split('.')[1] == ending)
                {
                    FILE = Converter.ConvertToPDF(file);
                    File.Delete(file);
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
