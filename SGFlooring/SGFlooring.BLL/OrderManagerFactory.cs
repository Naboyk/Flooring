using SGFlooring.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            string orderLive = ConfigurationManager.AppSettings["Orders"].ToString();
            //string orderTrainer = ConfigurationManager.AppSettings["Training"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrdersTestRepository());

                case "Production":
                    return new OrderManager(new LiveOrderRepository(orderLive));

                //case "Training":
                //    return new OrderManager(new TrainerRepository(orderTrainer));

                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
