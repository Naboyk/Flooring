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
    public class EditOrderWorkflow : ConsoleIO
    {
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            OrderManager orderManager = OrderManagerFactory.Create();
            TaxManager taxManager = TaxManagerFactory.Create();
            ProductManager productManager = ProductManagerFactory.Create();


            Order newOrder = new Order();

            Console.Clear();
            Console.WriteLine("Edit Order");
            SeperatorBar();

            //Query Access To Order To Edit
            int orderNumber = QueryOrderNumber();
            DateTime orderDate = QueryDate();
            Order oldOrder = orderManager.DisplayOrder(orderNumber, orderDate).Order;
            DisplayOrder(oldOrder);

            //define edit in models
            if (YorN($"Are you sure you want to edit your order?") == "Y")
            {
                try
                {
                    OrderResponse oldOrderResponse = orderManager.DisplayOrder(orderNumber, orderDate);

                    if (oldOrderResponse.Success)
                    {
                        bool flag = true;
                        while (flag)
                        {
                            Console.WriteLine("Edit the Order Customer Name or press enter to keep the Old Order's Customer Name the same.");
                            if (Console.ReadLine() != "")
                            {
                                newOrder.CustomerName = QueryCustomerName();
                            }
                            else
                            {
                                newOrder.CustomerName = oldOrder.CustomerName;
                            }
                            Console.WriteLine("Edit the Order State or press enter to keep the Old Order's State the same.");
                            if (Console.ReadLine() != "")
                            {
                                newOrder.State = QueryState();
                            }
                            else
                            {
                                newOrder.State = oldOrder.State;
                            }
                            Console.WriteLine("Edit the Order Product Type or press enter to keep the Old Order's Product Type the same.");
                            if (Console.ReadLine() != "")
                            {
                                newOrder.ProductType = QueryProductType();
                            }
                            else
                            {
                                newOrder.ProductType = oldOrder.ProductType;
                            }
                            Console.WriteLine("Edit the Order Area or press enter to keep the Old Order's Area the same.");
                            if (Console.ReadLine() != "")
                            {
                                newOrder.Area = QueryArea();
                            }
                            else
                            {
                                newOrder.Area = oldOrder.Area;
                            }
                            newOrder.TaxRate = taxManager.GetStateTax(newOrder.State).TaxRate;
                            newOrder.CostPerSquareFoot = productManager.GetProductType(newOrder.ProductType).CostPerSquareFoot;
                            newOrder.LaborCostPerSquareFoot = productManager.GetProductType(newOrder.ProductType).LaborCostPerSquareFoot;
                            newOrder._calculateTotal();

                            flag = false;
                        }

                        EditOrderResponse response = orderManager.EditOrder(oldOrder, newOrder, orderDate, orderNumber);
                        if (response.Success)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            DisplayOrder(response.NewOrder);
                            Console.WriteLine("Your order has been successfully edited.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("An error occured, youe old order should still be in the database");
                            Console.WriteLine(response.Message);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your order was not saved");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There was an issue with your input, redirecting you back to the menu");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occured");
            }
        }
    }
}
