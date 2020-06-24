using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Images
{
    class Display
    {
        public static void Run()
        {
            //ExStart: Display
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Load an image:
            Bitmap image = new Bitmap(RunExamples.GetDataDir() + @"Images\aspose_logo.png");

            // Draw the image:
            graphics.DrawImage(image, 0, 0);

            bitmap.Save(RunExamples.GetDataDir() + @"Images\Display_out.png");
            //ExEnd: Display
        }
    }
}
