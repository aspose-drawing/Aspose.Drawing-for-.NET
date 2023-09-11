using System.Diagnostics;

namespace Aspose.Drawing.Showcases
{
    class RunShowcases
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
            {
                LoadLicenseFromFile(args[0]);
            }

            try
            {
                CelticHeart.Run();
                CarBody.Run();
                LollipopFont.Run();
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("You need to launch the script Examples/Data/Showcases/prepare.cmd before the running of these showcases.");
                Console.Write("Also you need to have the installed ffmpeg. Would you like to open the download page? [y|N] ");
                string? answer = Console.ReadLine();
                if (answer == "y" || answer == "Y")
                {
                    Process.Start(new ProcessStartInfo { FileName = @"https://ffmpeg.org/download.html", UseShellExecute = true });
                }
            }
        }

        public static string GetDataDir()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "../../../../Data/Showcases/");
        }

        private static void LoadLicenseFromFile(string licensePath)
        {
            System.Drawing.AsposeDrawing.License license = new System.Drawing.AsposeDrawing.License();
            license.SetLicense(licensePath);
        }
    }
}