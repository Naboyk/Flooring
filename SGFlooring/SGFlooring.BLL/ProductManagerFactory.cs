using SGFlooring.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            string productFileLocation = ConfigurationManager.AppSettings["Product"].ToString();
            switch (mode)
            {
                case "Production":
                case "Training":
                case "Test":
                    return new ProductManager(new LiveProductRepository(productFileLocation));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
