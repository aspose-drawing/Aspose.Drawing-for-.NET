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
			System.Drawing.AsposeDrawing.License license = new System.Drawing.AsposeDrawing.License();
            // Set license
            license.SetLicense("Aspose.Drawing.lic");
            Console.WriteLine("License set successfully.");
            //ExEnd: LoadLicenseFromFile
        }

        static void LoadLicenseFromStream()
        {
			//ExStart: LoadLicenseFromStream
			// Initialize license object
			System.Drawing.AsposeDrawing.License license = new System.Drawing.AsposeDrawing.License();
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
			System.Drawing.AsposeDrawing.Metered metered = new System.Drawing.AsposeDrawing.Metered();
            // Set metered public and private keys
            metered.SetMeteredKey("*****", "*****");
            
            // DO PROCESSING
            
            // Get metered data amount and/or credits
            decimal amount = System.Drawing.AsposeDrawing.Metered.GetConsumptionQuantity();
            decimal credits = System.Drawing.AsposeDrawing.Metered.GetConsumptionCredit();
    
            // Display information
            Console.WriteLine("Amount Consumed : " + amount.ToString());
            Console.WriteLine("Credits Consumed : " + credits.ToString());
            //ExEnd: SetMeteredLicense
        }
    }
}
