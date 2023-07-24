using System;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Aspose.Drawing.Examples.CSharp.UseCases
{
	internal class TextOnImage
	{
		public static void Run()
		{
			using (var image = Image.FromFile(Path.Combine(RunExamples.GetDataDir(), "UseCases", "girl.jpg")))
			{
				var graphics = Graphics.FromImage(image);

				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				graphics.PageUnit = GraphicsUnit.Pixel;

				SolidBrush brush = new SolidBrush(Color.Navy);
				Font font = new Font("Calibri", 20, FontStyle.Italic);

				int padding = 5;

				string text = "Happy Birthday!";
				var words = text.Split(' ');

				int extentWidth = 0;
				int extentHeight = 0;

				words.ToList().ForEach(word => { var stringSize = graphics.MeasureString(word, font); extentWidth = Math.Max(extentWidth, (int)stringSize.Width + padding); extentHeight += (int)stringSize.Height; });

				Rectangle rectangle = new Rectangle(image.Width - padding - extentWidth, image.Height - padding - extentHeight, extentWidth, extentHeight);
				graphics.DrawString(text, font, brush, rectangle);

				image.Save(Path.Combine(RunExamples.GetDataDir(), "UseCases", "girl_card.jpg"));
			}
		}
	}
}
