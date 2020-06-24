using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class DrawEllipse
    {
        public static void Run()
        {
            //ExStart: DrawEllipse
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawEllipse(pen, 10, 10, 900, 700);

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\DrawEllipse_out.png");
            //ExEnd: DrawEllipse
        }
    }
}
