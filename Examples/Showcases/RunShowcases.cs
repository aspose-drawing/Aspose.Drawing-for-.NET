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

            CelticHeart.Run(); // Launch prepare.cmd before this.
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