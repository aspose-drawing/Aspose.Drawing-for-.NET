using System.Drawing;
using System.Drawing.Text;

namespace Aspose.Drawing.Examples.CSharp.TextFonts
{
    class DrawText
    {
        public static void Run()
        {
            //ExStart: DrawText
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.Clear(Color.FromKnownColor(KnownColor.White));

            Brush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 1);
            Font arial = new Font("Arial", 20, FontStyle.Regular);
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas sapien tellus, mattis et condimentum eget, commodo ut ipsum. Maecenas elit sapien, tempus sit amet mauris sit amet, hendrerit laoreet nisi. Nulla facilisi. Sed commodo, mauris eget porta commodo, nunc tellus volutpat mi, eu auctor diam libero vel neque. Vestibulum nec mattis dui, nec molestie nisl. Etiam in magna felis. Praesent non nulla tortor. Integer nec convallis purus. Fusce vitae mollis mauris. Cras efficitur dui at mi viverra scelerisque. Morbi quis magna elit. Nulla facilisis id ante sit amet fringilla. Sed iaculis consectetur lectus a interdum. Etiam ut sollicitudin lectus, et congue lectus.";
            Rectangle rectangle = new Rectangle(100, 100, 800, 600);
            graphics.DrawRectangle(pen, rectangle);
            graphics.DrawString(text, arial, brush, rectangle);

            bitmap.Save(RunExamples.GetDataDir() + @"TextFonts\DrawText_out.png");
            //ExEnd: DrawText
        }
    }
}
