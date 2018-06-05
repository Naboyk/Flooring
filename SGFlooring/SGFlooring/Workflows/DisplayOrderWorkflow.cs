using SGFlooring.BLL;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGFlooring.Workflows
{
    public class DisplayOrderWorkflow : ConsoleIO
    {
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Order Lookup");
            SeperatorBar();
            int orderNumber = QueryOrderNumber();
            DateTime orderDate = QueryDate();

            try
            {
                OrderResponse response = manager.DisplayOrder(orderNumber, orderDate);

                if (response.Success)
                {
                    DisplayOrder(response.Order);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An error occured");
                    Console.WriteLine(response.Message);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There was an issue with your input, redirecting you back to the me");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

        }
    }
}
