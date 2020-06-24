using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class LocalTransformation
    {
        public static void Run()
        {
            //ExStart: LocalTransformation
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(300, 300, 400, 200);

            // Set a transformation that applies to the specific path to be drawn:
            Matrix matrix = new Matrix();
            matrix.RotateAt(45, new Point(500, 400));
            path.Transform(matrix);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawPath(pen, path);

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\LocalTransformation_out.png");
            //ExEnd: LocalTransformation
        }
    }
}
