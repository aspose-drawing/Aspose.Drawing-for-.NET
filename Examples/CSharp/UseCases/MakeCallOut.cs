using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.Drawing.Examples.CSharp.UseCases
{
	internal class MakeCallOut
	{
		public static void Run()
		{
			// callout with size
			using (var image = Image.FromFile(Path.Combine(RunExamples.GetDataDir(), "UseCases", "gears.png")))
			{
				var graphics = Graphics.FromImage(image);

				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				graphics.PageUnit = GraphicsUnit.Pixel;

				DrawCallOut(graphics, new PointF(107, 55), new PointF(179, 5), 74, "mm");

				DrawCallOut(graphics, new PointF(111, 146), new PointF(29, 180), 28, "mm");

				image.Save(Path.Combine(RunExamples.GetDataDir(), "UseCases", "gears_with_callout.jpg"));
			}

			void DrawCallOut(Graphics graphic, PointF startAnchor, PointF endAnchor, int value, string unit)
			{
				Pen pen = new Pen(Color.DarkGray, 1);

				Font font = new Font("Arial", 10, FontStyle.Bold);

				string outputValue = $"{value} {unit}";

				var textSize = graphic.MeasureString(outputValue, font);

				int diameterSymbolSize = 12;
				int spaceSize = 3;

				textSize.Width += diameterSymbolSize + spaceSize;

				float callOutMiddleX = endAnchor.X > startAnchor.X ? endAnchor.X - textSize.Width : endAnchor.X + textSize.Width;
				float callOutMiddleY = endAnchor.Y > startAnchor.Y ? endAnchor.Y - textSize.Height : endAnchor.Y + textSize.Height;

				graphic.DrawLine(pen, startAnchor.X, startAnchor.Y, callOutMiddleX, callOutMiddleY);

				float textAnchorX = Math.Min(callOutMiddleX, endAnchor.X);
				float textAnchorY = callOutMiddleY;
				graphic.DrawLine(pen, callOutMiddleX, callOutMiddleY, textAnchorX == callOutMiddleX ? textAnchorX + textSize.Width : textAnchorX, callOutMiddleY);

				graphic.DrawEllipse(pen, new Rectangle((int)textAnchorX + spaceSize, (int)(textAnchorY - textSize.Height) + spaceSize, 10, 10));
				graphic.DrawLine(pen, (int)textAnchorX + 1, (int)textAnchorY - 1, (int)textAnchorX + diameterSymbolSize + 2, (int)textAnchorY - diameterSymbolSize - 2);

				SolidBrush brush = new SolidBrush(Color.DarkGray);

				graphic.DrawString(outputValue, font, brush, (int)textAnchorX + diameterSymbolSize + spaceSize, (int)(textAnchorY - textSize.Height));
			}
		}
	}
}
