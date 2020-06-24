using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aspose.Drawing.Examples.CSharp.LinesCurvesShapes
{
    class FillRegion
    {
        public static void Run()
        {
            //ExStart: FillRegion
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(new Point[] { new Point(100, 400), new Point(500, 100), new Point(900, 400), new Point(500, 700) });
            Region region = new Region(path);

            GraphicsPath innerPath = new GraphicsPath();
            innerPath.AddRectangle(new Rectangle(300, 300, 400, 200));
            region.Exclude(innerPath);

            Brush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Blue));
            graphics.FillRegion(brush, region);

            bitmap.Save(RunExamples.GetDataDir() + @"LinesCurvesShapes\FillRegion_out.png");
            //ExEnd: FillRegion
        }
    }
}
