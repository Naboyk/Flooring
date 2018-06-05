using SGFlooring.BLL;
using SGFlooring.Models;
using SGFlooring.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring
{
    public class ConsoleIO
    {
        OrderManager orderManager = OrderManagerFactory.Create();
        TaxManager taxManager = TaxManagerFactory.Create();
        ProductManager productManager = ProductManagerFactory.Create();

        public static void DisplayOrder(Order order)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Order Number: {order.OrderNumber} | Order Date: {order.Date}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"State: {order.State}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Material Cost {order.MaterialCost:c}");
            Console.WriteLine($"Labor Cost {order.LaborCost:c}");
            Console.WriteLine($"Tax {order.Tax:c}");
            Console.WriteLine($"Total {order.Total:c}");
        }

        public string QueryProductType()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the product you would like to order");
            string productType = Console.ReadLine();
            while (productManager.GetProductType(productType) == null || productType != productManager.GetProductType(productType).ProductType)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry we do not sell that product.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Here is a list of products we sell:");
                Console.WriteLine(string.Join(" , ", productManager.ListAllProduct().Select(p => p.ProductType)));
                productType = Console.ReadLine();
            }
            return productType;
        }

        public string QueryState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Enter your state:");
            string State = Console.ReadLine();
            while (taxManager.GetStateTax(State) == null || State != taxManager.GetStateTax(State).StateAbbreviation && State != taxManager.GetStateTax(State).StateName)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry we can not send orders to that state.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Here is a list of states within our jurisdiction:");
                Console.WriteLine(string.Join(" , ", taxManager.ListAllStateTax().Select(t => t.StateName)));
                State = Console.ReadLine();
            }
            return State;
        }

        public static string QueryCustomerName()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter a name for the order:");
            string CustomerName = Console.ReadLine();
            while (CustomerName == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter a name for your order.");
                CustomerName = Console.ReadLine();
            }
            return CustomerName;
        }

        public static void SeperatorBar()
        {
            Console.WriteLine("========================");
        }

        public static int QueryOrderNumber()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter your Order Number:");
            string orderNumberInput = Console.ReadLine();
            int orderNumber;
            while (!int.TryParse(orderNumberInput, out orderNumber) || orderNumber < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please input a valid order number greater than 0");
                orderNumberInput = Console.ReadLine();
            }
            return orderNumber;
        }
        public static DateTime QueryDate()
        {
            bool control = true;
            DateTime orderDate = new DateTime();
            while (control)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter the order date (Date Format MM/DD/YYYY):");
                string inputDate = Console.ReadLine();
                try
                {
                    orderDate = DateTime.Parse(inputDate);
                    control = false;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You can not enter nothing or garbage for the date");
                }
            }
            return orderDate;
        }

        public static decimal QueryArea()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the size of the area that will be floored:");
            decimal output;
            while (!decimal.TryParse(Console.ReadLine(), out output) || output < 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Give a valid decimal input.");
            }
            return output;
        }

        public static string YorN(string message)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message + " (Y/N)? ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must enter Y/N.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You must enter Y/N.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }
    }
}

