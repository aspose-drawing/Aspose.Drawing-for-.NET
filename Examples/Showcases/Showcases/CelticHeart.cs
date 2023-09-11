using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Aspose.Drawing.Showcases
{
    internal class CelticHeart
    {
        private static readonly bool makeVideo = false;
        private static readonly float ribbonInnerWidth = 48;
        private static readonly float ribbonStroke = 4;
        private static readonly Pen penInner = new(Color.FromArgb(255, 60, 60, 90), ribbonInnerWidth);
        private static readonly Pen penBorder = new(Color.White, ribbonInnerWidth + ribbonStroke * 2);
        private static readonly Pen penWide = new(Color.Red, ribbonInnerWidth + ribbonStroke * 2 + 8);
        private static readonly float signWidth = 32;
        private static readonly float emSize = 32;
        private static readonly FontFamily fontFamily = new("Segoe UI Historic");
        private static readonly Font font = new(fontFamily, emSize);
        private static readonly Brush fontBrush = new SolidBrush(Color.White);
        private static readonly string text = "ᛦᛨᛩᛪ᛭ᛮᛯᛰᚠᛅᛆᛇᛈᛉᛊᛋᛏᛐᛒᛓᛗᛘᛚᛝᛞᛟᛠᛡᛢᛣᛥᚨᚩᚪᚫᚬᚭᚮᚯᚰᚱᚳᚴᚷᚸᚹᚺᚻᚼᚾᚿᛀ";
        private static readonly string rootDirectory = Path.Combine(RunShowcases.GetDataDir(), "CelticHeart");
        private static readonly string outputDirectory = Path.Combine(rootDirectory, "out");
        private static readonly string inputDirectory1 = makeVideo ? Path.Combine(rootDirectory, "RooftopClouds_out") : rootDirectory;
        private static readonly string inputDirectory2 = Path.Combine(rootDirectory, "StarrySky_out");
        private static readonly Color bgColor = Color.Transparent;
        private static readonly Random random = new(0);
        private static readonly int k = 60; // Frame number for transition from one key position to another on the creeping line.
        private static readonly int frameLimit1 = 934;
        private static readonly int frameLimit2 = 722;
        private static readonly int bgMixLimit = 150;
        private static readonly int bgCycleLimit = frameLimit1 + frameLimit2 - bgMixLimit * 2;
        private static readonly int frameLimit = (3 * 60 + 46) * 30; // 3:46 (Nakarada.mp3), 226 seconds * 30 fps = 6780 frames
        private static int frameNumber = 0;
        private static int frameNumber1 = 0;
        private static int frameNumber2 = 0;
        private static int bgMixNumber = 0;

        public static void Run()
        {
            int w = 1120;
            int h = 900;

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Bitmap bitmap = new(w, h);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            GraphicsPath path1 = new();
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

            GraphicsPath path2 = new();
            path2.AddBezier(567, 212, 1020, 58, 969, 594, 564, 829);
            path2.AddBezier(564, 829, 163, 597, 116, 60, 567, 212);
            path2.CloseFigure();

            GraphicsPath flatPath1 = (GraphicsPath)path1.Clone();
            GraphicsPath flatPath2 = (GraphicsPath)path2.Clone();

            flatPath1.Flatten();
            flatPath2.Flatten();

            List<Node> nodes = new();
            List<Span> segments = new();
            List<Span> segments1 = new();
            List<Span> segments2 = new();

            PointF[] points1 = flatPath1.PathPoints;
            PointF[] points2 = flatPath2.PathPoints;

            AddSpans(segments1, points1);
            AddSpans(segments2, points2);
            segments.AddRange(segments2);
            segments.AddRange(segments1);

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
                        out bool segments_intersect, out PointF intersection_point);

                    if (segments_intersect)
                    {
                        Node node = new();
                        segA.Node = node;
                        segB.Node = node;
                        node.Point = intersection_point;
                        node.SegA = segA;
                        node.SegB = segB;
                        node.Otherwise = num % 2 == 0;
                        nodes.Add(node);
                        num++;
                    }
                }
            }

            Node last = nodes[^1];
            last.Otherwise = !last.Otherwise;

            List<Ribbon> ribbons = new();
            MakeRibbons(ribbons, segments2);
            MakeRibbons(ribbons, segments1);
            CalcShifts(ribbons, g);

            ImageAttributes imageAttributes = new();
            ColorMatrix colorMatrix = new(
                new float[][]
                {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, 0.55f, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                }
            );
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

            RectangleF rect = new(0, 0, w, h);

            if (makeVideo)
            {
                for (int i = 0; i < frameLimit; i++)
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
                frameNumber = 151;
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

        private static void CalcFrameNumbers()
        {
            int frameIndex = (frameNumber - 1) % bgCycleLimit;

            if (frameIndex >= 0 && frameIndex < bgMixLimit)
            {
                frameNumber1 = frameIndex + 1;
                frameNumber2 = frameLimit2 - bgMixLimit + frameIndex + 1;
                bgMixNumber = frameNumber1;
            }
            else if (frameIndex >= bgMixLimit && frameIndex < frameLimit1 - bgMixLimit)
            {
                frameNumber1 = frameIndex + 1;
                frameNumber2 = 0;
                bgMixNumber = frameNumber2;
            }
            else if (frameIndex >= frameLimit1 - bgMixLimit && frameIndex < frameLimit1)
            {
                frameNumber1 = frameIndex + 1;
                frameNumber2 = frameIndex - (frameLimit1 - bgMixLimit) + 1;
                bgMixNumber = frameNumber2;
            }
            else if (frameIndex >= frameLimit1 && frameIndex < bgCycleLimit)
            {
                frameNumber1 = 0;
                frameNumber2 = frameIndex - (frameLimit1 - bgMixLimit) + 1;
                bgMixNumber = frameNumber1;
            }
        }

        private static Bitmap Mix(Bitmap bitmap, PointF[] destParallelogram,
            RectangleF rect, ImageAttributes imageAttributes)
        {
            CalcFrameNumbers();
            Bitmap frame = BackgroundMix();
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

        private static Bitmap BackgroundMix()
        {
            if (frameNumber1 == 0)
            {
                string bgFile = Path.Combine(inputDirectory2, $"{frameNumber2:d5}.png");
                Bitmap frame = new(bgFile);
                return frame;
            }

            if (frameNumber2 == 0)
            {
                string bgFile = Path.Combine(inputDirectory1, $"{frameNumber1:d5}.png");
                Bitmap frame = new(bgFile);
                return frame;
            }

            float transparencyStep = 1.0f / (bgMixLimit + 1);
            float transparency = transparencyStep * bgMixNumber;
            if (frameNumber1 < frameNumber2)
            {
                transparency = 1.0f - transparency;
            }

            float x = 0;
            float y = 0;
            float w = 1920;
            float h = 1080;
            PointF[] destParallelogram = new PointF[]
            {
                new PointF( x, y ),
                new PointF( x + w, y ),
                new PointF( x, y + h ),
            };

            ImageAttributes imageAttributes = new();
            ColorMatrix colorMatrix = new(
                new float[][]
                {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, transparency, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                }
            );
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            string bgFile1 = Path.Combine(inputDirectory1, $"{frameNumber1:d5}.png");
            Bitmap frame1 = new(bgFile1);

            string bgFile2 = Path.Combine(inputDirectory2, $"{frameNumber2:d5}.png");
            Bitmap frame2 = new(bgFile2);

            Graphics g1 = Graphics.FromImage(frame1);
            g1.CompositingQuality = CompositingQuality.HighQuality;

            g1.DrawImage(
                frame2,
                destParallelogram,
                new RectangleF(0, 0, frame2.Width, frame2.Height),
                GraphicsUnit.Pixel,
                imageAttributes);

            return frame1;
        }

        private static void DrawRibbons(List<Ribbon> ribbons, Graphics g)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                DrawRibbon(ribbons[i], g);
            }
        }

        private static void DrawRibbon(Ribbon ribbon, Graphics g)
        {
            g.DrawPath(penBorder, ribbon.Path);
            g.DrawPath(penInner, ribbon.Path);

            GraphicsPath inner = (GraphicsPath)ribbon.Path.Clone();
            inner.Widen(penBorder);

            DrawTextOnPath(g, ribbon.Text, ribbon);

            if (ribbon.Node1 is null || ribbon.Node3 is null)
            {
                return;
            }

            PathGradientBrush brush1 = MakeBrush(ribbon.Node1.Point);
            g.FillPath(brush1, inner);

            PathGradientBrush brush3 = MakeBrush(ribbon.Node3.Point);
            g.FillPath(brush3, inner);
        }

        private static PathGradientBrush MakeBrush(PointF pt)
        {
            float r = 80;
            GraphicsPath path = new();
            path.AddEllipse(pt.X - r, pt.Y - r, r * 2, r * 2);

            PathGradientBrush brush = new(path) { CenterColor = Color.FromArgb(255, 0, 0, 0) };
            Color[] colors = { Color.FromArgb(0, 60, 60, 90) };
            brush.SurroundColors = colors;

            return brush;
        }

        private static void MakeRibbons(List<Ribbon> ribbons, List<Span> segments)
        {
            Span seg = segments[0];

            while (true)
            {
                while (seg.Node == null)
                {
                    seg = segments.Next(seg);
                }

                if (seg.Done)
                {
                    return;
                }

                seg.Done = true;

                Ribbon ribbon = new();
                ribbons.Add(ribbon);
                ribbon.Node1 = seg.Node;
                ribbon.Node1.Ribbon1 = ribbon;

                List<Span> spans = new() { seg };

                do
                {
                    seg = segments.Next(seg);
                    spans.Add(seg);
                }
                while (seg.Node == null);

                ribbon.Node2 = seg.Node;
                ribbon.Node2.Ribbon2 = ribbon;

                do
                {
                    seg = segments.Next(seg);
                    spans.Add(seg);
                }
                while (seg.Node == null);

                ribbon.Node3 = seg.Node;
                ribbon.Node3.Ribbon3 = ribbon;

                for (int i = 0; i < spans.Count; i++)
                {
                    Span span = spans[i];

                    if (i == 0 && span.Node is not null) // the first
                    {
                        ribbon.Path.AddLine(span.Node.Point, span[1]);
                    }
                    else if (i == spans.Count - 1 && span.Node is not null) // the last
                    {
                        ribbon.Path.AddLine(span[0], span.Node.Point);
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
                if (ribbon.Node2 is not null)
                {
                    Node node = ribbon.Node2;

                    GraphicsPath part1 = ribbon.Path;
                    GraphicsPath part2 = MakeUnderPart(ribbon.Node2, segments1, segments2);

                    GraphicsPath widenPart1 = (GraphicsPath)part1.Clone();
                    GraphicsPath widenPart2 = (GraphicsPath)part2.Clone();

                    widenPart1.Widen(penWide);
                    widenPart2.Widen(penWide);

                    Region interParts = new(widenPart1);
                    interParts.Intersect(new Region(widenPart2));

                    g.Clip = interParts;
                    g.Clear(bgColor);

                    if (node.Ribbon1 is not null && node.Ribbon2 is not null && node.Ribbon3 is not null)
                    {
                        DrawRibbon(node.Ribbon1, g);
                        DrawRibbon(node.Ribbon3, g);
                        DrawRibbon(node.Ribbon2, g);
                    }

                    g.ResetClip();
                }
            }
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
            GraphicsPath part = new();
            if (node.SegA is null || node.SegB is null)
            {
                return part;
            }

            int r = 80;
            PointF iPt = node.Point;
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

        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool segments_intersect, out PointF intersection)
        {
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;
            float denominator = dy12 * dx34 - dx12 * dy34;
            float t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (float.IsInfinity(t1))
            {
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                return;
            }
            float t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            segments_intersect = (t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1);
        }

        private static void DrawTextOnPath(Graphics g, string txt, Ribbon ribbon)
        {
            for (int i = 1; i < ribbon.Shifts.Count - 1; i++)
            {
                string sub = txt.Substring(i, 1);

                Shift shift1 = ribbon.Shifts[i];
                Shift shift2 = ribbon.Shifts[i + 1];

                int n = (frameNumber - 1) % k;
                Shift shift = new(
                    shift1.X + (shift2.X - shift1.X) / k * n,
                    shift1.Y + (shift2.Y - shift1.Y) / k * n,
                    shift1.Angle + SmallestAngleDiff(shift1.Angle, shift2.Angle) / k * n,
                    shift1.Hint + (shift2.Hint - shift1.Hint) / k * n);

                DrawTextOnSegment(g, sub, shift);
            }
        }

        private static void DrawTextOnSegment(Graphics g, string txt, Shift shift)
        {
            GraphicsState state = g.Save();

            g.TranslateTransform(0, shift.Hint, MatrixOrder.Append);
            g.RotateTransform(shift.Angle, MatrixOrder.Append);
            g.TranslateTransform(shift.X, shift.Y, MatrixOrder.Append);

            GraphicsPath path = new();
            path.AddString(txt, fontFamily, 0, emSize, new Point(0, 0), null);
            g.FillPath(fontBrush, path);

            g.Restore(state);
        }

        private static void CalcShifts(List<Ribbon> ribbons, Graphics g)
        {
            for (int i = 0; i < ribbons.Count; i++)
            {
                CalcShiftsOnPath(g, ribbons[i].Text, ribbons[i]);
            }
        }

        private static void CalcShiftsOnSegment(Graphics g, string txt, ref int first_ch,
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

            float hint = 0.39f;
            float limit = 60.0f;
            float diff = SmallestAngleDiff(prev_angle, angle);

            if (!float.IsNaN(prev_angle) && diff > limit)
            {
                hint *= 1.3f;
            }

            prev_angle = angle;

            ribbon.Shifts.Add(new Shift(start_point.X, start_point.Y, angle,
                -g.MeasureString(chars_that_fit, font).Height * hint));

            first_ch++;
            start_point = new PointF(
                start_point.X + dx * signWidth,
                start_point.Y + dy * signWidth);
        }

        private static void CalcShiftsOnPath(Graphics g, string txt, Ribbon ribbon)
        {
            int start_ch = 0;
            PointF start_point = ribbon.Path.PathPoints[0];
            float prev_angle = float.NaN;
            for (int i = 1; i < ribbon.Path.PointCount - 1; i++)
            {
                PointF end_point = ribbon.Path.PathPoints[i];
                CalcShiftsOnSegment(g, txt, ref start_ch,
                    ref start_point, end_point, ref prev_angle, ribbon);
                if (start_ch >= txt.Length) break;
            }

            Shift shift1 = ribbon.Shifts[0];
            Shift shift2 = ribbon.Shifts[1];

            Shift shift = new(
                shift1.X - (shift1.X - shift2.X),
                shift1.Y - (shift1.Y - shift2.Y),
                shift1.Angle - SmallestAngleDiff(shift1.Angle, shift2.Angle),
                shift1.Hint - (shift1.Hint - shift2.Hint));

            ribbon.Shifts.Insert(0, shift);

            shift1 = ribbon.Shifts[^2];
            shift2 = ribbon.Shifts[^1];

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
            return text2[i] + t[..^1];
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

        private static double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
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

        internal class Span
        {
            public Node? Node;
            public bool Done;
            private readonly PointF[] points = new PointF[2];

            public Span(PointF start, PointF end)
            {
                this.points[0] = start;
                this.points[1] = end;
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
            public Span? SegA;
            public Span? SegB;
            public bool Otherwise;
            public Ribbon? Ribbon1;
            public Ribbon? Ribbon2;
            public Ribbon? Ribbon3;
        }

        internal class Ribbon
        {
            public GraphicsPath Path = new();
            public string Text = RandomString();
            public List<Shift> Shifts = new();
            public Node? Node1;
            public Node? Node2;
            public Node? Node3;
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
        public static T Next<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                return item;
            }

            int nextIndex = index + 1;
            return nextIndex < list.Count ? list[nextIndex] : list[0];
        }

        public static T Prev<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                return item;
            }

            int prevIndex = index - 1;
            return prevIndex < 0 ? list[list.Count - 1] : list[prevIndex];
        }
    }
}
