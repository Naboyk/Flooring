using SGFlooring.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class TaxManagerFactory
    {
        public static TaxManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            string taxFileLocation = ConfigurationManager.AppSettings["Tax"].ToString();

            switch (mode)
            {
                case "Production":
                case "Training":
                case "Tests":
                    return new TaxManager(new LiveTaxRepository(taxFileLocation));

                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
