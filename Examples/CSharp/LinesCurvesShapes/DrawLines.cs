using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class DrawLines
    {
        public static void Run()
        {
            //ExStart: DrawLines
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawLine(pen, 10, 700, 500, 10);
            graphics.DrawLine(pen, 500, 10, 990, 700);

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\DrawLines_out.png");
            //ExEnd: DrawLines
        }
    }
}
