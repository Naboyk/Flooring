using SGFlooring.BLL;
using SGFlooring.Models;
using SGFlooring.Models.Responses;
using System;
using System.Linq;


namespace SGFlooring.Workflows
{
    public class AddOrderWorkflow : ConsoleIO
    {
        public void Execute()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Order newOrder = new Order();
            OrderManager orderManager = OrderManagerFactory.Create();
            TaxManager taxManager = TaxManagerFactory.Create();
            ProductManager productManager = ProductManagerFactory.Create();

            Console.WriteLine("Add Order");
            SeperatorBar();

            //Query User Info
            newOrder.Date = QueryDate();
            newOrder.CustomerName = QueryCustomerName();
            newOrder.State = QueryState();
            newOrder.ProductType = QueryProductType();
            newOrder.Area = QueryArea();

            //Calculations
            newOrder.TaxRate = taxManager.GetStateTax(newOrder.State).TaxRate;
            newOrder.CostPerSquareFoot = productManager.GetProductType(newOrder.ProductType).CostPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = productManager.GetProductType(newOrder.ProductType).LaborCostPerSquareFoot;
            newOrder._calculateTotal();
            DisplayOrder(newOrder);

            if(YorN($"Are you sure you want to add your order?") == "Y")
            {
                AddOrderResponse response = orderManager.AddOrder(newOrder);
                if (response.Success)
                {
                    DisplayOrder(response.Order);
                    Console.WriteLine("Your order has been added to our files!");
                    Console.WriteLine(response.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An error occured, your order was not saved");
                    Console.WriteLine(response.Message);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your order was not saved");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
