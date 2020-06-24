using System.Drawing;

namespace Aspose.Drawing.Examples.CSharp.Images
{
    class LoadSave
    {
        public static void Run()
        {
            //ExStart: LoadSave
            // Load the image:
            Bitmap bitmap = new Bitmap(RunExamples.GetDataDir() + @"Images\aspose_logo.png");

            // Save the image:
            bitmap.Save(RunExamples.GetDataDir() + @"Images\LoadSave_out.png");
            //ExEnd: LoadSave
        }
    }
}
