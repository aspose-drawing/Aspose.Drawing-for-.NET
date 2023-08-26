using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Aspose.Drawing.Showcases
{
    internal class CarBody
    {
        private static Brush edgeBrush = Brushes.Magenta;
        private static readonly string rootDirectory = Path.Combine(RunShowcases.GetDataDir(), "CarBody");
        private static readonly string outputDirectory = Path.Combine(rootDirectory, "out");
        private static readonly string inputDirectory = rootDirectory;
        private static List<Bitmap> frames = new List<Bitmap>();

        public static void Run()
        {
            int w = 1024;
            int h = 438;

            GraphicsPath carBody = MakeCarBody(h);

            frames.Add(
                DrawFrame(
                    w, h, carBody,
                    Color.FromArgb(120, 0, 80, 80),
                    Color.White, "", 0, 0, null, null, 0, 0));

            frames.Add(
                DrawFrame(
                    w, h, carBody,
                    Color.FromArgb(80, 180, 80, 0),
                    Color.White, "", 0, 0, null, null, 0, 0));

            frames.Add(
                DrawFrame(
                    w, h, carBody,
                    Color.FromArgb(80, 80, 80, 180),
                    Color.FromArgb(120, 0, 255, 255), "KEWIN", 350, h - 277,
                    new Font("Arial", 140, FontStyle.Bold, GraphicsUnit.Pixel),
                    Path.Combine(inputDirectory, "brit-01.png"), 333, h - 253));

            frames.Add(
                DrawFrame(
                    w, h, carBody,
                    Color.FromArgb(120, 255, 255, 0),
                    Color.FromArgb(120, 255, 0, 0), "ANCHOR", 250, h - 285,
                    new Font("Verdana", 120, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel),
                    Path.Combine(inputDirectory, "anchor-01.png"), 275, h - 290));

            float step = 0.5f;
            float percent = 0;
            int frameNumber = 0;

            // tmp
            Bitmap frame1 = frames[0];
            Bitmap frame2 = frames[1];
            Bitmap mixedFrame = Mix(w, h, frame1, frame2, percent);

            for (int f1 = 0; f1 < frames.Count; f1++)
            {
                int f2 = (f1 + 1) % frames.Count;
                frame1 = frames[f1];
                frame2 = frames[f2];

                for (int i = 0; i < 200; i++)
                {
                    ++frameNumber;
                    percent = i * step % 100;
                    mixedFrame = Mix(w, h, frame1, frame2, percent);
                    DrawEdge(w, h, carBody, mixedFrame, percent);

                    string fileName = string.Format("{0}/{1:d5}.png", outputDirectory, frameNumber);
                    mixedFrame.Save(fileName);
                    Console.WriteLine(fileName);
                }
            }

            mixedFrame.Save(Path.Combine(inputDirectory, "carbody.png"));
        }

        private static Bitmap Mix(int w, int h, Bitmap frame1, Bitmap frame2, float percent)
        {
            Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;

            float edgePosition = w * percent / 100;

            RectangleF rect1 = new RectangleF(edgePosition, 0, w - edgePosition, h);
            g.DrawImage(frame1, rect1, rect1, GraphicsUnit.Pixel);

            RectangleF rect2 = new RectangleF(0, 0, edgePosition, h);
            g.DrawImage(frame2, rect2, rect2, GraphicsUnit.Pixel);

            return bitmap;
        }

        private static void DrawEdge(int w, int h, GraphicsPath carBody, Bitmap frame, float percent)
        {
            float edgeSize = 5.0f;
            float edgePosition = w * percent / 100;

            Rectangle rect = new Rectangle(
                (int)(edgePosition - edgeSize / 2), 0,
                (int)(edgeSize), h);

            Region edge = new Region(carBody);
            edge.Intersect(rect);

            Graphics g = Graphics.FromImage(frame);        
            g.FillRegion(edgeBrush, edge);
        }

        private static Bitmap DrawFrame(
            int w, int h, GraphicsPath carBody,
            Color bgColor,
            Color textColor, string text, int textX, int textY, Font font,
            string logoFile, int logoX, int logoY)
        {
            Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            Bitmap rasterCar = new Bitmap(Path.Combine(inputDirectory, "carbody-sedan-04.png"));
            g.DrawImage(rasterCar, new RectangleF(0, 0, w, h));



            // draw car body
            Brush brush1 = new SolidBrush(bgColor);
            g.FillPath(brush1, carBody);



            // make the pattern of painting for car body: logo + text (both optional)
            if (!string.IsNullOrEmpty(logoFile) || !string.IsNullOrWhiteSpace(text))
            {
                Bitmap bitmap2 = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                Graphics g2 = Graphics.FromImage(bitmap2);
                g2.SmoothingMode = SmoothingMode.HighQuality;
                g2.TextRenderingHint = TextRenderingHint.AntiAlias;

                g2.Clip = new Region(carBody);

                // logo
                if (!string.IsNullOrEmpty(logoFile))
                {
                    Bitmap logo = new Bitmap(logoFile);
                    g2.DrawImage(logo, new RectangleF(logoX, logoY, logo.Width, logo.Height));
                }

                // banner text
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Brush brush2 = new SolidBrush(textColor);
                    g2.DrawString(text, font, brush2, textX, textY);
                }



                // painting of car body by pattern
                g.Clip = new Region(carBody);

                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, 0.7f, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                });

                imageAttributes.SetColorMatrix(
                    colorMatrix,
                    ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                PointF[] destParallelogram = new PointF[]
                {
                    new PointF( 0, 0 ),
                    new PointF( w, 0 ),
                    new PointF( 0, h ),
                };

                g.DrawImage(
                    bitmap2,
                    destParallelogram,
                    new RectangleF(0, 0, w, h),
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }

            return bitmap;
        }

        private static GraphicsPath MakeCarBody(int h)
        {
            // external contour
            GraphicsPath path1 = new GraphicsPath();

            // hood
            path1.AddBezier(
                99, h - 199,
                99, h - 261,
                299f, h - 264,
                318f, h - 268);

            // windshield
            path1.AddBezier(
                318, h - 268,
                359, h - 293,
                434, h - 327,
                465, h - 330);

            // roof and rear glass
            path1.AddBezier(
                465, h - 330,
                650, h - 351,
                706, h - 333,
                806, h - 281);

            // trunk top line
            path1.AddBezier(
                806, h - 281,
                860, h - 273,
                880, h - 280,
                882, h - 268);

            // trunk rear line
            path1.AddBezier(
                882, h - 268,
                883, h - 254,
                898, h - 259,
                893, h - 203);

            // rear bumper, rear line
            path1.AddBezier(
                893, h - 203,
                908, h - 192,
                905, h - 194,
                902, h - 171);

            // rear bumper, bottom line
            path1.AddBezier(
                902, h - 171,
                898, h - 133,
                827, h - 128,
                802, h - 122);

            // rear arc, rear angle
            path1.AddBezier(
                802, h - 122,
                798, h - 121,
                796, h - 125,
                796, h - 128);

            // rear arc, rear half
            path1.AddBezier(
                796, h - 128,
                803, h - 208,
                741, h - 210,
                732, h - 209);

            // rear arc, front half
            path1.AddBezier(
                732, h - 209,
                687, h - 209,
                664, h - 176,
                665, h - 136);

            // rear arc, front angle
            path1.AddBezier(
                665, h - 136,
                665, h - 125,
                659, h - 120,
                651, h - 120);

            // bottom between arcs
            path1.AddLine(
                651, h - 120,
                311, h - 120);

            // front arc, rear angle
            path1.AddBezier(
                311, h - 120,
                307, h - 120,
                304, h - 124,
                304, h - 128);

            // front arc, rear half
            path1.AddBezier(
                304, h - 128,
                311, h - 195,
                257, h - 209,
                238, h - 209);

            // front arc, front half
            path1.AddBezier(
                238, h - 209,
                213, h - 209,
                168, h - 195,
                172, h - 131);

            // front arc, front angle
            path1.AddBezier(
                172, h - 131,
                173, h - 121,
                168, h - 122,
                164, h - 123);

            // front bumper, bottom line
            path1.AddBezier(
                164, h - 123,
                103, h - 130,
                94, h - 134,
                93, h - 146);

            // front bumper, front line
            path1.AddBezier(
                93, h - 146,
                93, h - 202,
                84, h - 192,
                99, h - 199); // the first point of contour



            // headlight
            GraphicsPath path2 = new GraphicsPath();

            // front line
            path2.AddBezier(
                106, h - 196,
                106, h - 213,
                120, h - 224,
                128, h - 226);

            // top line and rear line
            path2.AddBezier(
                128, h - 226,
                182, h - 222,
                155, h - 224,
                148, h - 196);

            // bottom line
            path2.CloseFigure();

            //g.DrawPath(pen1, path2);
            path1.AddPath(path2, false);



            // rear lights
            GraphicsPath path3 = new GraphicsPath();

            // front line
            path3.AddBezier(
                855, h - 226,
                854, h - 236,
                851, h - 241,
                848, h - 246);

            // top line
            path3.AddBezier(
                848, h - 246,
                869, h - 247,
                879, h - 251,
                888, h - 254);

            // rear line
            path3.AddBezier(
                888, h - 254,
                894, h - 245,
                894, h - 234,
                894, h - 226);

            // bottom line
            path3.CloseFigure();

            //g.DrawPath(pen1, path3);
            path1.AddPath(path3, false);



            // side glasses
            GraphicsPath path4 = new GraphicsPath();

            // front line
            path4.AddBezier(
                379, h - 256,
                386, h - 254,
                385, h - 284,
                377, h - 281);

            // top line
            path4.AddBezier(
                377, h - 281,
                447, h - 329,
                554, h - 328,
                621, h - 323);

            // rear line
            path4.AddBezier(
                621, h - 323,
                686, h - 318,
                734, h - 287,
                752, h - 278);

            // rear angle
            path4.AddBezier(
                752, h - 278,
                770, h - 267,
                758, h - 264,
                754, h - 264);

            // bottom line
            path4.CloseFigure();

            //g.DrawPath(pen1, path4);
            path1.AddPath(path4, false);



            // windshield
            GraphicsPath path5 = new GraphicsPath();

            // bottom line
            path5.AddBezier(
                468, h - 325,
                451, h - 322,
                390, h - 305,
                320, h - 258);

            // bottom angle
            path5.AddBezier(
                320, h - 258,
                303, h - 257,
                310, h - 264,
                318, h - 268);

            // front/top line
            path5.AddBezier(
                318, h - 268,
                359, h - 293,
                434, h - 327,
                465, h - 330);

            // rear line
            path5.CloseFigure();

            //g.DrawPath(pen1, path5);
            path1.AddPath(path5, false);



            // rear glass
            GraphicsPath path6 = new GraphicsPath();

            // top/rear line
            path6.AddBezier(
                696, h - 326,
                711, h - 323,
                734, h - 319,
                806, h - 281);

            // bottom angle
            path6.AddBezier(
                806, h - 281,
                803, h - 277,
                797, h - 276,
                793, h - 277);

            // bottom line
            path6.AddBezier(
                793, h - 277,
                741, h - 301,
                716, h - 311,
                691, h - 318);

            // front line
            path6.CloseFigure();

            //g.DrawPath(pen1, path6);
            path1.AddPath(path6, false);



            // turn signal
            GraphicsPath path7 = new GraphicsPath();

            path7.AddRectangle(new Rectangle(305, h - 227, 10, 5));

            //g.DrawPath(pen1, path7);
            path1.AddPath(path7, false);

            return path1;
        }
    }
}
