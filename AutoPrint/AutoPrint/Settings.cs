using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoPrint
{
    class Settings
    {
        public static string PATH { get; set; }
        public static string[] ENDINGS { get; set; }
        public static string PRINTER_NAME { get; set; }

        public static void Create()
        {
            try
            {
                var document = new XmlDocument();
                var main_node = document.CreateElement("Settings");
                document.AppendChild(main_node);

                var path_node = document.CreateElement("Path");
                path_node.InnerText = "";
                main_node.AppendChild(path_node);

                var endings_node = document.CreateElement("Endings");
                endings_node.InnerText = "";
                main_node.AppendChild(endings_node);

                var printer_node = document.CreateElement("Printer_name");
                printer_node.InnerText = "";
                main_node.AppendChild(printer_node);

                document.Save("Settings.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Load()
        {
            using (var xml = new XmlTextReader("Settings.xml"))
            {
                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element && xml.Name == "Path")
                        {
                            PATH = xml.ReadString();
                        }
                        else if (xml.NodeType == XmlNodeType.Element && xml.Name == "Endings")
                        {
                            ENDINGS = xml.ReadString().Split(',');
                        }
                        else if (xml.NodeType == XmlNodeType.Element && xml.Name == "Printer_name")
                        {
                            PRINTER_NAME = xml.ReadString();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
