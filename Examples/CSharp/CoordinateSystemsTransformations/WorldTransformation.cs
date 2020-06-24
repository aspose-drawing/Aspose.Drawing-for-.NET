using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class WorldTransformation
    {
        public static void Run()
        {
            //ExStart: WorldTransformation
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            // Set the transformation that maps world coordinates to page coordinates:
            graphics.TranslateTransform(500, 400);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawRectangle(pen, 0, 0, 300, 200);

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\WorldTransformation_out.png");
            //ExEnd: WorldTransformation
        }
    }
}
