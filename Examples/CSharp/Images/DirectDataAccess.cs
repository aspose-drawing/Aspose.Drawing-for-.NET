using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Images
{
    class DirectDataAccess
    {
        public static void Run()
        {
            //ExStart: DirectDataAccess
            Bitmap sourceBitmap = new Bitmap(RunExamples.GetDataDir() + @"Images\aspose_logo.png");
            Bitmap targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            // Directly copy pixel data from the source to the target bitmap:
            int[] pixels = new int[sourceBitmap.Width * sourceBitmap.Height];
            sourceBitmap.ReadArgb32Pixels(pixels);
            targetBitmap.WriteArgb32Pixels(pixels);

            targetBitmap.Save(RunExamples.GetDataDir() + @"Images\DirectDataAccess_out.png");
            //ExEnd: DirectDataAccess
        }
    }
}
