using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Rendering
{
    class AlphaBlending
    {
        public static void Run()
        {
            //ExStart: AlphaBlending
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(128, 255, 0, 0)), 300, 100, 400, 400);
            graphics.FillEllipse(new SolidBrush(Color.FromArgb(128, 0, 255, 0)), 200, 300, 400, 400);
            graphics.FillEllipse(new SolidBrush(Color.FromArgb(128, 0, 0, 255)), 400, 300, 400, 400);

            bitmap.Save(RunExamples.GetDataDir() + @"Rendering\AlphaBlending_out.png");
            //ExEnd: AlphaBlending
        }
    }
}
