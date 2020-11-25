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
            // Initialize license object
            Aspose.Drawing.License license = new Aspose.Drawing.License();
            // Load license in FileStream
            FileStream myStream = new FileStream("Aspose.Drawing.lic", FileMode.Open);
            // Set license
            license.SetLicense(myStream);
            Console.WriteLine("License set successfully.");
            //ExEnd: LoadLicenseFromStream
        }
        
        static void SetMeteredLicense()
        {
            //ExStart: SetMeteredLicense
            // Initialize metered object
            Aspose.Drawing.Metered metered = new Aspose.Drawing.Metered();
            // Set metered public and private keys
            metered.SetMeteredKey("*****", "*****");
            
            // DO PROCESSING
            
            // Get metered data amount and/or credits
            decimal amount = Aspose.Drawing.Metered.GetConsumptionQuantity();
            decimal credits = Aspose.Drawing.Metered.GetConsumptionCredit();
    
            // Display information
            Console.WriteLine("Amount Consumed : " + amount.ToString());
            Console.WriteLine("Credits Consumed : " + credits.ToString());
            //ExEnd: SetMeteredLicense
        }
    }
}
