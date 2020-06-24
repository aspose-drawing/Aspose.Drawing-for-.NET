using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aspose.Drawing.Examples.CSharp.Pens
{
    class Join
    {
        public static void Run()
        {
            //ExStart: Join
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            DrawPath(graphics, LineJoin.Bevel, 200);
            DrawPath(graphics, LineJoin.Round, 400);

            bitmap.Save(RunExamples.GetDataDir() + @"Pens\Join_out.png");
            //ExEnd: Join
        }

        //ExStart: PenJoinDrawPath
        private static void DrawPath(Graphics graphics, LineJoin join, int y)
        {
            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 30);
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(100, y, 200, y);
            path.AddLine(200, y, 200, y + 100);
            pen.LineJoin = join;
            graphics.DrawPath(pen, path);
        }
        //ExEnd: PenJoinDrawPath
    }
}
