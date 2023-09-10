using System.Drawing;
using System.Drawing.Drawing2D;

namespace Aspose.Drawing.Showcases
{
    internal class LollipopFont
    {
        private static readonly bool makeVideo = false;
        private static readonly string rootDirectory = Path.Combine(RunShowcases.GetDataDir(), "LollipopFont");
        private static readonly string outputDirectory = Path.Combine(rootDirectory, "out");
        private static readonly string inputDirectory = rootDirectory;
        private static readonly string word = "Lollipop";
        private static readonly int frames = 500;
        private static readonly Size frameSize = new(1920, 1080);
        private static readonly int fontSize = 288;
        private static readonly int horizon = 700;
        private static readonly int seed = 54;
        private static float nutDiameter = 0;

        public static void Run()
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            for (int i = 0; i < (makeVideo ? frames : 1); ++i)
            {
                nutDiameter = 35.0f - (i % 20);
                Bitmap image1 = DrawWord(word);
                Bitmap bitmap = DrawReflection(image1, frameSize.Width, frameSize.Height);

                string fileName = string.Format("{0}/{1:d4}.png", outputDirectory, i + 1);
                bitmap.Save(fileName);
                Console.WriteLine(fileName);
            }
        }

        private static Bitmap DrawWord(string word)
        {
            int w = word.Length * fontSize;
            int h = fontSize * 3;
            int margin = 5;

            Bitmap bitmap = new(w, h, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            graphics.Clear(Color.Black);

            FontFamily fontFamily = new("Arial");
            StringFormat stringFormat = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle rectangle = new(0, fontSize, w, fontSize);

            GraphicsPath path = new();
            path.AddString(word, fontFamily, (int)FontStyle.Bold, fontSize, rectangle, stringFormat);

            GraphicsPath path2 = new();
            path2.AddString(word, fontFamily, (int)FontStyle.Bold, fontSize, rectangle, stringFormat);

            VisitPoints(path, path2, graphics, AddEllipseToPath);

            Random r = new(seed);
            int half = 15; // of pen amount
            int doubleStroke = 3 * 2; // the double width of stroke is increment of real pen width
            List<Color> colors = GetColors(half, r);
            Pen widest = new(colors[0], half * 2 * doubleStroke);

            for (int i = 0; i < half * 2; ++i)
            {
                Color c = colors[i];
                Pen pen = new(c, (half * 2 - i) * doubleStroke)
                {
                    LineJoin = LineJoin.Round
                };

                if (i == 0)
                {
                    widest = pen;
                }

                graphics.DrawPath(pen, path2);
            }

            LinearGradientBrush brush = new(
                rectangle,
                Color.White,
                Color.Gray,
                LinearGradientMode.Vertical);
            graphics.FillPath(brush, path);

            VisitPoints(path, path, graphics, DrawAndFillEllipse);

            path2.Widen(widest);
            bitmap = Crop(bitmap, path2, margin);

            return bitmap;
        }

        private static Bitmap Crop(Bitmap bitmap, GraphicsPath path, int margin)
        {
            RectangleF bounds = path.GetBounds();
            RectangleF bounds2 = new(
                bounds.X - margin,
                bounds.Y - margin,
                bounds.Width + margin * 2,
                bounds.Height + margin * 2);
            return bitmap.Clone(bounds2, bitmap.PixelFormat);
        }

        private static Color Blend(Color color, Color backColor, double amount)
        {
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        private static List<Color> GetColors(int half, Random r)
        {
            int start = 0;
            int range = 255;
            List<Color> colors = new();

            int count = half * 2;
            for (int i = 0; i < count; ++i)
            {
                Color c;
                if (i < half)
                {
                    c = Color.FromArgb(start + r.Next(range), start + r.Next(range), start + r.Next(range));
                    colors.Add(c);
                }
                else
                {
                    Color o = colors[i % half];
                    c = Color.FromArgb(255 - o.R, 255 - o.G, 255 - o.B);
                    colors.Add(c);
                }
            }

            List<Color> palette = new();
            Bitmap image1 = new(Path.Combine(inputDirectory, "palette.png"));
            int w = image1.Width;
            int index = r.Next(w);
            int step = w / count;

            for (int i = 0; i < count; ++i)
            {
                index += step;
                index %= w - 1;
                Color c = image1.GetPixel(index, 0);
                palette.Add(c);
            }

            List<Color> result = new();
            int interval = 4;
            for (int i = 0; i < count; ++i)
            {
                Color c;
                if (i % interval == 0)
                {
                    c = Blend(palette[i], colors[i], 0.7);
                }
                else if (i % interval == 1)
                {
                    c = Blend(palette[i], colors[i], 0.4);
                }
                else if (i % interval == 2)
                {
                    c = Blend(palette[i], colors[i], 0.2);
                }
                else
                {
                    c = Blend(palette[i], colors[i], 0.1);
                }
                result.Add(c);
            }

            return result;
        }

        private static double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private static void VisitPoints(
            GraphicsPath path,
            GraphicsPath path2,
            Graphics graphics,
            Action<RectangleF, Graphics, GraphicsPath> handler)
        {
            PointF[] points = path.PathPoints;
            PointF prev = new(0, 0);
            float diameter = nutDiameter;
            for (int i = 0; i < path.PointCount; ++i)
            {
                PointF point = points[i];
                if (i % 5 == 0 && Distance(point, prev) > 12)
                {
                    RectangleF rectangle = new(
                        point.X - diameter / 2,
                        point.Y - diameter / 2,
                        diameter,
                        diameter);
                    handler(rectangle, graphics, path2);
                }
                prev = point;
            }
        }

        private static void DrawAndFillEllipse(RectangleF rectangle, Graphics graphics, GraphicsPath path)
        {
            graphics.FillEllipse(Brushes.White, rectangle);
            graphics.DrawEllipse(Pens.LightGray, rectangle);
        }

        private static void AddEllipseToPath(RectangleF rectangle, Graphics graphics, GraphicsPath path)
        {
            path.AddEllipse(rectangle);
        }

        public static Bitmap DrawReflection(Bitmap image1, int w, int h)
        {
            Bitmap bitmap = new(w, h, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.Clear(Color.Black);

            float bw = image1.Width;
            float bh = image1.Height;

            graphics.DrawImage(image1, new RectangleF(
                w / 2 - bw / 2,
                horizon - bh,
                bw,
                bh));

            graphics.DrawImage(image1, new RectangleF(
                w / 2 - bw / 2,
                horizon + bh / 2,
                bw,
                -bh / 2));

            RectangleF rectangle = new(
                w / 2 - bw / 2,
                horizon,
                bw,
                bh / 2);

            LinearGradientBrush brush = new(
                rectangle,
                Color.FromArgb(150, 0, 0, 0),
                Color.FromArgb(255, 0, 0, 0),
                LinearGradientMode.Vertical);
            graphics.FillRectangle(brush, rectangle);

            return bitmap;
        }
    }
}
