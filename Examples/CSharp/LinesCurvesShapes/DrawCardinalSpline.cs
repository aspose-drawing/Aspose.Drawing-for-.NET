using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class DrawCardinalSpline
    {
        public static void Run()
        {
            //ExStart: DrawCardinalSpline
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawCurve(pen, new Point[] { new Point(10, 700), new Point(250, 500), new Point(500, 10), new Point(750, 500), new Point(990, 700) });

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\DrawCardinalSpline_out.png");
            //ExEnd: DrawCardinalSpline
        }
    }
}
