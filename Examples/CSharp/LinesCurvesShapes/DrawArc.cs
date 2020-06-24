using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class DrawArc
    {
        public static void Run()
        {
            //ExStart: DrawArc
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawArc(pen, 0, 0, 700, 700, 0, 180);

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\DrawArc_out.png");
            //ExEnd: DrawArc
        }
    }
}
