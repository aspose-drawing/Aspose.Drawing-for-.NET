using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class MatrixTransformations
    {
        public static void Run()
        {
            //ExStart: MatrixTransformations
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            Rectangle originalRentangle = new Rectangle(300, 300, 300, 200);
            TransformPath(graphics, originalRentangle, (matrix) => matrix.Rotate(15.0f));
            TransformPath(graphics, originalRentangle, (matrix) => matrix.Translate(-250, -250));
            TransformPath(graphics, originalRentangle, (matrix) => matrix.Scale(0.3f, 0.3f));

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\MatrixTransformations_out.png");
            //ExEnd: MatrixTransformations
        }

        //ExStart: MatrixTransformationsTransformPath
        private static void TransformPath(Graphics graphics, Rectangle originalRentangle, Action<Matrix> transform)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(originalRentangle);

            Matrix matrix = new Matrix();
            transform(matrix);
            path.Transform(matrix);

            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            graphics.DrawPath(pen, path);
        }
        //ExEnd: MatrixTransformationsTransformPath
    }
}
