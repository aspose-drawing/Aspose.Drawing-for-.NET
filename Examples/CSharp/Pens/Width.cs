using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Pens
{
    class Width
    {
        public static void Run()
        {
            //ExStart: Width
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 1; i < 8; ++i)
            {
                Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), i);
                graphics.DrawLine(pen, 100, i * 100, 900, i * 100);
            }

            bitmap.Save(RunExamples.GetDataDir() + @"Pens\Width_out.png");
            //ExEnd: Width
        }
    }
}
