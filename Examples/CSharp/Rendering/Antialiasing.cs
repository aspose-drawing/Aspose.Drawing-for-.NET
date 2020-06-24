using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Rendering
{
    class Antialiasing
    {
        public static void Run()
        {
            //ExStart: Antialiasing
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(Color.FromKnownColor(KnownColor.White));

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Black), 1);
            graphics.DrawEllipse(pen, 10, 10, 980, 780);
            graphics.DrawCurve(pen, new Point[] { new Point(10, 700), new Point(250, 500), new Point(500, 10), new Point(750, 500), new Point(990, 700) });
            graphics.DrawLine(pen, 20, 20, 980, 780);

            bitmap.Save(RunExamples.GetDataDir() + @"Rendering\Antialiasing_out.png");
            //ExEnd: Antialiasing
        }
    }
}
