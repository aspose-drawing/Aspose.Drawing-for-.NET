using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.Drawing.Examples.CSharp.UseCases
{
    internal class PhotoFrame
    {
        public static void Run()
        {
            // Create photo frame
            using (var image = Image.FromFile(Path.Combine(RunExamples.GetDataDir(), "UseCases", "cat.jpg")))
            {
                var graphics = Graphics.FromImage(image);

                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.PageUnit = GraphicsUnit.Pixel;

                var pen = new Pen(Color.Magenta, 1);

                int gap = 2;

                graphics.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);
                graphics.DrawRectangle(pen, gap, gap, image.Width - gap - 1, image.Height - gap - 1);

                image.Save(Path.Combine(RunExamples.GetDataDir(), "UseCases", "cat_with_honor_out.jpg"));
            }
        }
    }
}
