using System.IO;

namespace Aspose.Drawing.Examples.CSharp
{
    class RunExamples
    {
        static void Main(string[] args)
        {
            //ConvertImage.Run();

            // Uncomment if you have a license:
            // LoadLicenseFromFile();

            LinesCurvesShapes.DrawLines.Run();
            LinesCurvesShapes.DrawRectangle.Run();
            LinesCurvesShapes.DrawEllipse.Run();
            LinesCurvesShapes.DrawArc.Run();
            LinesCurvesShapes.DrawPolygon.Run();
            LinesCurvesShapes.DrawCardinalSpline.Run();
            LinesCurvesShapes.DrawBezierSpline.Run();
            LinesCurvesShapes.DrawClosedCurve.Run();
            LinesCurvesShapes.DrawPath.Run();
            LinesCurvesShapes.FillRegion.Run();

            Pens.Colors.Run();
            Pens.Width.Run();
            Pens.Join.Run();

            Brushes.Solid.Run();

            TextFonts.InstalledFonts.Run();
            TextFonts.DrawText.Run();
            TextFonts.FormatText.Run();
            TextFonts.Hinting.Run();

            Rendering.Antialiasing.Run();
            Rendering.Clipping.Run();
            Rendering.AlphaBlending.Run();

            CoordinateSystemsTransformations.WorldTransformation.Run();
            CoordinateSystemsTransformations.PageTransformation.Run();
            CoordinateSystemsTransformations.UnitsOfMeasure.Run();
            CoordinateSystemsTransformations.MatrixTransformations.Run();
            CoordinateSystemsTransformations.GlobalTransformation.Run();
            CoordinateSystemsTransformations.LocalTransformation.Run();

            Images.LoadSave.Run();
            Images.Display.Run();
            Images.Scale.Run();
            Images.Cropping.Run();
            Images.DirectDataAccess.Run();

            GraphicsFileFormats.LoadSave.Run();
        }

        public static string GetDataDir()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string startDirectory = null;
            if (parent != null)
            {
                var directoryInfo = parent.Parent;
                if (directoryInfo != null)
                {
                    startDirectory = directoryInfo.FullName;
                }
            }
            else
            {
                startDirectory = parent.FullName;
            }
            return Path.Combine(startDirectory, "Data\\");
        }

        private static void LoadLicenseFromFile()
        {
            // Initialize license object
            Aspose.Drawing.License license = new Aspose.Drawing.License();
            // Set license
            license.SetLicense("Aspose.Drawing.lic");
        }
    }
}
