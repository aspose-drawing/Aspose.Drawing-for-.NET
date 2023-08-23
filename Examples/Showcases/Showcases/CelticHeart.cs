using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Aspose.Drawing.Showcases
{
    internal class CelticHeart
    {
        private static readonly bool makeVideo = false;
        private static Pen penBorder = new Pen(Color.White, 48 + 4 * 2);
        private static Pen penWide = new Pen(Color.Red, 48 + 4 * 2 + 8);
        private static Pen pen1 = new Pen(Color.FromArgb(255, 60, 60, 90), 48);
        private static Font font = new Font("Segoe UI Historic", 28, FontStyle.Regular);
        private static Brush fontBrush = new SolidBrush(Color.White);
        private static string text = "ᛦᛨᛩᛪ᛭ᛮᛯᛰᚠᛅᛆᛇᛈᛉᛊᛋᛏᛐᛒᛓᛗᛘᛚᛝᛞᛟᛠᛡᛢᛣᛥᚨᚩᚪᚫᚬᚭᚮᚯᚰᚱᚳᚴᚷᚸᚹᚺᚻᚼᚾᚿᛀ";
        private static float signWidth = 32;
        private static int k = 60; // frames per change a sign to next one
        private static int frameNumber = 0;
        private static int frameLimit = 934;
        private static string rootDirectory = Path.Combine(RunShowcases.GetDataDir(), "CelticHeart");
        private static string outputDirectory = Path.Combine(rootDirectory, "out");
        private static string inputDirectory = Path.Combine(rootDirectory, "RooftopClouds_out");
        private static Color bgColor = Color.FromArgb(0, 255, 255, 255);
        private static Font font2 = new Font("Consolas", 10, FontStyle.Regular);
        private static int count = 0;
        private static Random random = new Random(0);
        private static StringFormat strformat = new StringFormat();
        private static FontFamily fontFamily = new FontFamily("Segoe UI Historic");
        private static float emSize = 32.0f;

        public static void Run()
        {
            int w = 1120;
            int h = 900;

            Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            GraphicsPath path1 = new GraphicsPath();
            path1.AddBezier(567, 712, 411, 600, 302, 412, 352, 332);
            path1.AddBezier(352, 332, 407, 254, 498, 261, 689, 394);
            path1.AddBezier(689, 394, 878, 531, 900, 670, 900, 670);
            path1.AddBezier(900, 670, 680, 724, 546, 551, 513, 505);
            path1.AddBezier(513, 505, 482, 466, 348, 300, 566, 79);
            path1.AddBezier(566, 79, 780, 303, 650, 467, 622, 506);
            path1.AddBezier(622, 506, 584, 549, 450, 725, 232, 667);
            path1.AddBezier(232, 667, 232, 667, 252, 533, 441, 394);
            path1.AddBezier(441, 394, 632, 261, 712, 254, 772, 322);
            path1.AddBezier(772, 322, 834, 398, 725, 601, 567, 712);
            path1.CloseFigure();

            GraphicsPath path2 = new GraphicsPath();
            path2.AddBezier(567, 212, 1020, 58, 969, 594, 564, 829);
            path2.AddBezier(564, 829, 163, 597, 116, 60, 567, 212);
            path2.CloseFigure();

            GraphicsPath flatPath1 = (GraphicsPath)path1.Clone();
            GraphicsPath flatPath2 = (GraphicsPath)path2.Clone();

            flatPath1.Flatten();
            flatPath2.Flatten();

            List<Node> nodes = new List<Node>();
            List<Span> segments = new List<Span>();
            List<Span> segments1 = new List<Span>();
            List<Span> segments2 = new List<Span>();

            PointF[] points1 = flatPath1.PathPoints;
            PointF[] points2 = flatPath2.PathPoints;

            AddSpans(segments1, points1);
            AddSpans(segments2, points2);
            segments.AddRange(segments2);
            segments.AddRange(segments1);

            bool lines_intersect;
            bool segments_intersect;
            PointF intersection_point;
            PointF close_p1;
            PointF close_p2;
            int num = 0;
            for (int i = 0; i < segments.Count; i++)
            {
                for (int j = i + 1; j < segments.Count; j++)
                {
                    Span segA = segments[i];
                    Span segB = segments[j];

                    if (segA[0].Equals(segB[0]) || segA[0].Equals(segB[1]) ||
                        segA[1].Equals(segB[0]) || segA[1].Equals(segB[1]))
                    {
                        continue;
                    }

                    FindIntersection(
                        segA[0], segA[1], segB[0], segB[1],
                        out lines_intersect, out segments_intersect,
                        out intersection_point,
                        out close_p1, out close_p2);

                    if (segments_intersect)
                    {
                        Node node = new Node();
                        segA.node = node;
                        segB.node = node;
                        node.Point = intersection_point;
                        node.SegA = segA;
                        node.SegB = segB;
                        node.Otherwise = num % 2 == 0;
                        nodes.Add(node);
                        num++;
                    }
                }
            }

            Node last = nodes[nodes.Count - 1];
            last.Otherwise = !last.Otherwise;

            List<Ribbon> ribbons = new List<Ribbon>();
            MakeRibbons(ribbons, segments2);
            MakeRibbons(ribbons, segments1);
            CalcShifts(ribbons, g);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] { 1, 0, 0, 0, 0 },
                new float[] { 0, 1, 0, 0, 0 },
                new float[] { 0, 0, 1, 0, 0 },
                new float[] { 0, 0, 0, 0.55f, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            });

            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            float x = 1920 / 2 - w / 2;
            float y = 1080 / 2 - h / 2;

            PointF[] destParallelogram = new PointF[]
            {
                new PointF( x, y ),
                new PointF( x + w, y ),
                new PointF( x, y + h ),
            };

            RectangleF rect = new RectangleF(0, 0, w, h);

            if (makeVideo)
            {
                for (int i = 0; i <= frameLimit - 1; i++)
                {
                    if (i % k == 0)
                    {
                        ShiftRibbonStrings(ribbons);
                    }

                    ++frameNumber;
                    g.Clear(bgColor);
                    DrawRibbons(ribbons, g);
                    RedrawRibbons(ribbons, segments1, segments2, g);
                    Bitmap frame = Mix(bitmap, destParallelogram, rect, imageAttributes);

                    string fileName = Path.Combine(outputDirectory, $"{frameNumber:d5}.png");
                    frame.Save(fileName);
                    Console.WriteLine(fileName);
                }
            }
            else
            {
                frameNumber = 1;
                for (int i = 0; i < 1; i++)
                {
                    ShiftRibbonStrings(ribbons);
                }
                g.Clear(bgColor);
                DrawRibbons(ribbons, g);
                RedrawRibbons(ribbons, segments1, segments2, g);
                Bitmap frame = Mix(bitmap, destParallelogram, rect, imageAttributes);

                string fileName = Path.Combine(outputDirectory, "CelticHeart.png");
                frame.Save(fileName);
                Console.WriteLine(fileName);
            }
        }

        private static Bitmap Mix(Bitmap bitmap, PointF[] destParallelogram,
            RectangleF rect, ImageAttributes imageAttributes)
        {
            string bgFile = Path.Combine(inputDirectory, $"{frameNumber:d5}.png");
            Bitmap frame = new Bitmap(bgFile);
            Graphics g2 = Graphics.FromImage(frame);
            g2.CompositingQuality = CompositingQuality.HighQuality;

            g2.DrawImage(
                bitmap,
                destParallelogram,
                rect,
                GraphicsUnit.Pixel,
                imageAttributes);

            return frame;
        }

        private static void CalcShifts(List<Ribbon> ribbons, Graphics g)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                CalcShiftsOnPath(g, font, ribbons[i].Text, ribbons[i]);
            }
        }

        private static void DrawRibbon(Ribbon ribbon, Graphics g, int i)
        {
            g.DrawPath(penBorder, ribbon.Path);
            g.DrawPath(pen1, ribbon.Path);

            GraphicsPath inner = (GraphicsPath)ribbon.Path.Clone();
            inner.Widen(penBorder);

            DrawTextOnPath(g, fontBrush, font, ribbon.Text, ribbon);

            PathGradientBrush brush1 = MakeBrush(ribbon.Node1.Point);
            g.FillPath(brush1, inner);

            PathGradientBrush brush3 = MakeBrush(ribbon.Node3.Point);
            g.FillPath(brush3, inner);
        }

        private static float NormalizeAngle(float angle)
        {
            angle %= 360;
            return angle < 0 ? angle += 360 : angle;
        }

        private static float SmallestAngleDiff(float alpha, float beta)
        {
            float diff = 180 - Math.Abs(Math.Abs(NormalizeAngle(alpha) - NormalizeAngle(beta)) - 180);

            if (alpha - beta > 180)
            {
                alpha -= 360;
            }
            else if (beta - alpha > 180)
            {
                alpha -= 360;
            }

            return alpha > beta ? -diff : diff;
        }

        private static PathGradientBrush MakeBrush(PointF pt)
        {
            float r = 80;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(pt.X - r, pt.Y - r, r * 2, r * 2);

            PathGradientBrush brush = new PathGradientBrush(path);
            brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
            Color[] colors = { Color.FromArgb(0, 60, 60, 90) };
            brush.SurroundColors = colors;

            return brush;
        }

        private static void DrawRibbons(List<Ribbon> ribbons, Graphics g)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                DrawRibbon(ribbons[i], g, i);
            }
        }

        private static void MakeRibbons(List<Ribbon> ribbons, List<Span> segments)
        {
            Span seg = segments[0];

            while (true)
            {
                while (seg.node == null)
                {
                    seg = segments.Next(seg);
                }

                if (seg.Done)
                {
                    return;
                }

                seg.Done = true;

                Ribbon ribbon = new Ribbon();
                ribbons.Add(ribbon);
                ribbon.Node1 = seg.node;
                ribbon.Node1.Ribbon1 = ribbon;

                List<Span> spans = new List<Span>();
                spans.Add(seg);

                do
                {
                    seg = segments.Next(seg);
                    spans.Add(seg);
                }
                while (seg.node == null);

                ribbon.Node2 = seg.node;
                ribbon.Node2.Ribbon2 = ribbon;

                do
                {
                    seg = segments.Next(seg);
                    spans.Add(seg);
                }
                while (seg.node == null);

                ribbon.Node3 = seg.node;
                ribbon.Node3.Ribbon3 = ribbon;

                for (int i = 0; i < spans.Count; i++)
                {
                    Span span = spans[i];

                    if (i == 0) // the first
                    {
                        ribbon.Path.AddLine(span.node.Point, span[1]);
                    }
                    else if (i == spans.Count - 1) // the last
                    {
                        ribbon.Path.AddLine(span[0], span.node.Point);
                    }
                    else
                    {
                        ribbon.Path.AddLine(span[0], span[1]);
                    }
                }
            }
        }

        private static void RedrawRibbons(
            List<Ribbon> ribbons, List<Span> segments1, List<Span> segments2, Graphics g)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                Ribbon ribbon = ribbons[i];
                Node node = ribbon.Node2;

                GraphicsPath part1 = ribbon.Path;
                GraphicsPath part2 = MakeUnderPart(ribbon.Node2, segments1, segments2);

                GraphicsPath widenPart1 = (GraphicsPath)part1.Clone();
                GraphicsPath widenPart2 = (GraphicsPath)part2.Clone();

                widenPart1.Widen(penWide);
                widenPart2.Widen(penWide);

                Region interParts = new Region(widenPart1);
                interParts.Intersect(new Region(widenPart2));

                g.Clip = interParts;
                g.Clear(bgColor);

                DrawRibbon(node.Ribbon1, g, -1);
                DrawRibbon(node.Ribbon3, g, -1);
                DrawRibbon(node.Ribbon2, g, i);

                g.ResetClip();
            }
        }

        private static double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        private static void AddSpans(List<Span> segments, PointF[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
                segments.Add(new Span(points[i], points[j]));
            }
        }

        private static GraphicsPath MakeUnderPart(
            Node node, List<Span> segments1, List<Span> segments2)
        {
            int r = 80;
            PointF iPt = node.Point;

            GraphicsPath part = new GraphicsPath();
            Span seg = node.Otherwise ? node.SegA : node.SegB;
            List<Span> segs = segments1.Contains(seg) ? segments1 : segments2;

            int stop = 100;
            int c;
            for (c = 0; c < stop; c++)
            {
                seg = segs.Prev(seg);
                if (Distance(iPt, seg[0]) > r ||
                    Distance(iPt, seg[1]) > r)
                {
                    break;
                }
            }

            for (int j = -c; j < stop; j++)
            {
                part.AddLine(seg[0], seg[1]);
                seg = segs.Next(seg);
                if (Distance(iPt, seg[0]) > r ||
                    Distance(iPt, seg[1]) > r)
                {
                    break;
                }
            }

            return part;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        private static void DrawTextOnPath(Graphics gr, Brush brush, Font font, string txt, Ribbon ribbon)
        {
            for (int i = 1; i < ribbon.Shifts.Count - 1; i++)
            {
                string sub = txt.Substring(i, 1);

                Shift shift1 = ribbon.Shifts[i];
                Shift shift2 = ribbon.Shifts[i + 1];

                int n = (frameNumber - 1) % k;
                Shift shift = new Shift(
                    shift1.X + (shift2.X - shift1.X) / k * n,
                    shift1.Y + (shift2.Y - shift1.Y) / k * n,
                    shift1.Angle + SmallestAngleDiff(shift1.Angle, shift2.Angle) / k * n,
                    shift1.Hint + (shift2.Hint - shift1.Hint) / k * n);

                DrawTextOnSegment(gr, brush, font, sub, shift);
            }
        }

        private static void DrawTextOnSegment(Graphics gr, Brush brush,
            Font font, string txt, Shift shift)
        {
            GraphicsState state = gr.Save();

            gr.TranslateTransform(0, shift.Hint, MatrixOrder.Append);
            gr.RotateTransform(shift.Angle, MatrixOrder.Append);
            gr.TranslateTransform(shift.X, shift.Y, MatrixOrder.Append);

            //gr.DrawString(txt, font, brush, 0, 0);

            GraphicsPath path = new GraphicsPath();
            path.AddString(txt, fontFamily, 0, emSize, new Point(0, 0), strformat);
            gr.FillPath(brush, path);

            gr.Restore(state);
        }

        private static void CalcShiftsOnSegment(Graphics gr, Font font, string txt, ref int first_ch,
            ref PointF start_point, PointF end_point,
            ref float prev_angle, Ribbon ribbon)
        {
            if (first_ch >= txt.Length)
            {
                return;
            }

            float dx = end_point.X - start_point.X;
            float dy = end_point.Y - start_point.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            dx /= dist;
            dy /= dist;

            if (signWidth > dist)
            {
                return;
            }

            string chars_that_fit =
                txt.Substring(first_ch, 1);

            float angle = (float)(180 * Math.Atan2(dy, dx) / Math.PI);

            float hint = 0.45f;
            float limit = 60.0f;
            float diff = SmallestAngleDiff(prev_angle, angle);

            if (!float.IsNaN(prev_angle) && diff > limit)
            {
                hint *= 1.3f;
            }

            prev_angle = angle;

            ribbon.Shifts.Add(new Shift(start_point.X, start_point.Y, angle,
                -gr.MeasureString(chars_that_fit, font).Height * hint));

            first_ch++;
            start_point = new PointF(
                start_point.X + dx * signWidth,
                start_point.Y + dy * signWidth);
        }

        private static void CalcShiftsOnPath(Graphics gr, Font font, string txt, Ribbon ribbon)
        {
            int start_ch = 0;
            PointF start_point = ribbon.Path.PathPoints[0];
            float prev_angle = float.NaN;
            for (int i = 1; i < ribbon.Path.PointCount - 1; i++)
            {
                PointF end_point = ribbon.Path.PathPoints[i];
                CalcShiftsOnSegment(gr, font, txt, ref start_ch,
                    ref start_point, end_point, ref prev_angle, ribbon);
                if (start_ch >= txt.Length) break;
            }

            Shift shift1 = ribbon.Shifts[0];
            Shift shift2 = ribbon.Shifts[1];

            Shift shift = new Shift(
                shift1.X - (shift1.X - shift2.X),
                shift1.Y - (shift1.Y - shift2.Y),
                shift1.Angle - SmallestAngleDiff(shift1.Angle, shift2.Angle),
                shift1.Hint - (shift1.Hint - shift2.Hint));

            ribbon.Shifts.Insert(0, shift);

            shift1 = ribbon.Shifts[ribbon.Shifts.Count - 2];
            shift2 = ribbon.Shifts[ribbon.Shifts.Count - 1];

            shift = new Shift(
                shift2.X + (shift2.X - shift1.X),
                shift2.Y + (shift2.Y - shift1.Y),
                shift2.Angle + SmallestAngleDiff(shift2.Angle, shift1.Angle),
                shift2.Hint + (shift2.Hint - shift1.Hint));

            ribbon.Shifts.Add(shift);
        }

        private static string ShiftString(string t)
        {
            string text2 = text.Remove(text.IndexOf(t[0]), 1);
            int i = random.Next(0, text2.Length - 1);
            return text2[i] + t.Substring(0, t.Length - 1);
        }

        private static string RandomString()
        {
            string text2 = text;
            for (int i = 0; i < text2.Length; i++)
            {
                text2 = ShiftString(text2);
            }
            return text2;
        }

        private static void ShiftRibbonStrings(List<Ribbon> ribbons)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                ribbons[i].Text = ShiftString(ribbons[i].Text);
            }
        }

        internal class Span
        {
            public Node node;
            public bool Done;
            private PointF[] points;

            public Span(PointF start, PointF end)
            {
                this.points = new PointF[2] { start, end };
            }

            public PointF this[int index]
            {
                get { return this.points[index]; }
                set { this.points[index] = value; }
            }
        }

        internal class Node
        {
            public PointF Point;
            public Span SegA;
            public Span SegB;
            public bool Otherwise;
            public Ribbon Ribbon1;
            public Ribbon Ribbon2;
            public Ribbon Ribbon3;
        }

        internal class Ribbon
        {
            public GraphicsPath Path = new GraphicsPath();
            public string Text = RandomString();
            public List<Shift> Shifts = new List<Shift>();
            public Node Node1;
            public Node Node2;
            public Node Node3;
        }

        internal class Shift
        {
            public float X;
            public float Y;
            public float Angle;
            public float Hint;

            public Shift(float x, float y, float angle, float hint)
            {
                this.X = x;
                this.Y = y;
                this.Angle = angle;
                this.Hint = hint;
            }
        }
    }

    public static class ListExtensions
    {
        public static T? Next<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                return default(T);
            }

            int nextIndex = index + 1;
            return nextIndex < list.Count ? list[nextIndex] : list[0];
        }

        public static T? Prev<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                return default(T);
            }

            int prevIndex = index - 1;
            return prevIndex < 0 ? list[list.Count - 1] : list[prevIndex];
        }
    }
}
