using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.GraphicsFileFormats
{
    class LoadSave
    {
        public static void Run()
        {
            LoadAndSave("bmp");
            LoadAndSave("gif");
            LoadAndSave("jpg");
            LoadAndSave("png");
            LoadAndSave("tiff");
        }

        private static void LoadAndSave(string graphicsFileFormats)
        {
            new Bitmap(RunExamples.GetDataDir() + @"GraphicsFileFormats\image." + graphicsFileFormats).Save(RunExamples.GetDataDir() + @"GraphicsFileFormats\image_out." + graphicsFileFormats);
        }
    }
}
