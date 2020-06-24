using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.Drawing.Examples.CSharp.Licensing
{
    public class Licensing
    {
        static void LoadLicenseFromFile()
        {
            //ExStart: LoadLicenseFromFile
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize license object
            Aspose.Drawing.License license = new Aspose.Drawing.License();
            // Set license
            license.SetLicense("Aspose.Drawing.lic");
            Console.WriteLine("License set successfully.");
            //ExEnd: LoadLicenseFromFile
        }

        static void LoadLicenseFromStream()
        {
            //ExStart: LoadLicenseFromStream
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize license object
            Aspose.Drawing.License license = new Aspose.Drawing.License();
            // Load license in FileStream
            FileStream myStream = new FileStream("Aspose.Drawing.lic", FileMode.Open);
            // Set license
            license.SetLicense(myStream);
            Console.WriteLine("License set successfully.");
            //ExEnd: LoadLicenseFromStream
        }
    }
}
