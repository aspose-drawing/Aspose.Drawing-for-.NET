using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Images
{
    class Scale
    {
        public static void Run()
        {
            //ExStart: Scale
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            Bitmap image = new Bitmap(RunExamples.GetDataDir() + @"Images\aspose_logo.png");

            // Scale the image 5x:
            Rectangle expansionRectangle = new Rectangle(0, 0, image.Width * 5, image.Height * 5);
            graphics.DrawImage(image, expansionRectangle);

            bitmap.Save(RunExamples.GetDataDir() + @"Images\Scale_out.png");
            //ExEnd: Scale
        }
    }
}
