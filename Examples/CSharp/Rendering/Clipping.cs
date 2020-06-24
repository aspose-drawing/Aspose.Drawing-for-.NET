using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Aspose.Drawing.Examples.CSharp.Rendering
{
    class Clipping
    {
        public static void Run()
        {
            //ExStart: Clipping
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Rectangle rectangle = new Rectangle(200, 200, 600, 400);
            GraphicsPath clipPath = new GraphicsPath();
            clipPath.AddEllipse(rectangle);
            graphics.SetClip(clipPath);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Brush brush = new SolidBrush(Color.FromKnownColor(KnownColor.White));
            Font arial = new Font("Arial", 20, FontStyle.Regular);
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas sapien tellus, mattis et condimentum eget, commodo ut ipsum. Maecenas elit sapien, tempus sit amet mauris sit amet, hendrerit laoreet nisi. Nulla facilisi. Sed commodo, mauris eget porta commodo, nunc tellus volutpat mi, eu auctor diam libero vel neque. Vestibulum nec mattis dui, nec molestie nisl. Etiam in magna felis. Praesent non nulla tortor. Integer nec convallis purus. Fusce vitae mollis mauris. Cras efficitur dui at mi viverra scelerisque. Morbi quis magna elit. Nulla facilisis id ante sit amet fringilla. Sed iaculis consectetur lectus a interdum. Etiam ut sollicitudin lectus, et congue lectus.";
            graphics.DrawString(text, arial, brush, rectangle, stringFormat);

            bitmap.Save(RunExamples.GetDataDir() + @"Rendering\Clipping_out.png");
            //ExEnd: Clipping
        }
    }
}
