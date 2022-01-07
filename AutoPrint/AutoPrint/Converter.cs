using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace AutoPrint
{
    class Converter
    {
        public static string ConvertToPDF(string file)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            string newFile;

            DrawImage(gfx, file, (int)page.Width, (int)page.Height);

            document.Save(newFile = $"{file.Split('.')[0]}.pdf");

            return newFile;
        }

        static void DrawImage(XGraphics gfx, string path, int width, int height)
        {
            var image = XImage.FromFile(path);
            gfx.DrawImage(image, 0, 0, width, height);
            image.Dispose();
        }
    }
}
