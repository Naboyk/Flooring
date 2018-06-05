using SGFlooring.BLL;
using SGFlooring.Models;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Workflows
{
    public class RemoveOrderWorkflow : ConsoleIO
    {
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();

            Console.WriteLine("Remove Order");
            SeperatorBar();

            int orderNumber = QueryOrderNumber();
            DateTime orderDate = QueryDate();

            RemoveOrderResponse response = manager.RemoveOrder(orderNumber, orderDate);
            DisplayOrder(response.Order);

            if(YorN($"Are you sure you want to remove your order?") == "Y")
            {
                if (response.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    DisplayOrder(response.Order);
                    Console.WriteLine("Your order has been successfully removed");
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
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Order was not deleted");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
           
        }
    }
}
