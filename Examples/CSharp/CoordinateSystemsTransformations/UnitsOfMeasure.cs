using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.CoordinateSystemsTransformations
{
    class UnitsOfMeasure
    {
        public static void Run()
        {
            //ExStart: UnitsOfMeasure
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

            // 1 point is 1/72 inch.
            graphics.PageUnit = GraphicsUnit.Point;
            graphics.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.Red), 36f), 72, 72, 72, 72);

            // 1 mm is 1/25.4 inch.
            graphics.PageUnit = GraphicsUnit.Millimeter;
            graphics.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.Green), 6.35f), 25.4f, 25.4f, 25.4f, 25.4f);

            graphics.PageUnit = GraphicsUnit.Inch;
            graphics.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.Blue), 0.125f), 1, 1, 1, 1);

            bitmap.Save(RunExamples.GetDataDir() + @"CoordinateSystemsTransformations\UnitsOfMeasure_out.png");
            //ExEnd: UnitsOfMeasure
        }
    }
}
