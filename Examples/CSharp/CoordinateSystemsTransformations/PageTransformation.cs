using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class PageTransformation
    {
        public static void Run()
        {
            //ExStart: PageTransformation
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            // Set the transformation that maps page coordinates to device coordinates:
            graphics.PageUnit = GraphicsUnit.Inch;

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 0.1f);
            graphics.DrawRectangle(pen, 1, 1, 1, 1);

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\PageTransformation_out.png");
            //ExEnd: PageTransformation
        }
    }
}
