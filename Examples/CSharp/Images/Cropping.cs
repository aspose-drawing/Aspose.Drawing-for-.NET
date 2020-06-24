using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Images
{
    class Cropping
    {
        public static void Run()
        {
            //ExStart: Cropping
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            Bitmap image = new Bitmap(RunExamples.GetDataDir() + @"Images\aspose_logo.png");

            // Select the top left part of the image with the logo:
            Rectangle sourceRectangle = new Rectangle(0, 0, 50, 40);
            Rectangle destinationRectangle = sourceRectangle;
            graphics.DrawImage(image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);

            bitmap.Save(RunExamples.GetDataDir() + @"Images\Cropping_out.png");
            //ExEnd: Cropping
        }
    }
}
