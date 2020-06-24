using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class GlobalTransformation
    {
        public static void Run()
        {
            //ExStart: GlobalTransformation
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            // Set a transformation that applies to every drawn item:
            graphics.RotateTransform(15);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawEllipse(pen, 300, 300, 400, 200);

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\GlobalTransformation_out.png");
            //ExEnd: GlobalTransformation
        }
    }
}
