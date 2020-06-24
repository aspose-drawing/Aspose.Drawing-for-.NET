using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class DrawPolygon
    {
        public static void Run()
        {
            //ExStart: DrawPolygon
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawPolygon(pen, new Point[] { new Point(100, 100), new Point(500, 700), new Point(900, 100) });

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\DrawPolygon_out.png");
            //ExEnd: DrawPolygon
        }
    }
}
