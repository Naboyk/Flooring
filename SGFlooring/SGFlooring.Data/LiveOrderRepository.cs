using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class LiveOrderRepository : IOrderRepository
    {
        private string _filepath;

        public LiveOrderRepository(string filePath)
        {
            _filepath = filePath;
        }

        private List<Order> LoadAllOrders(DateTime dateTime)
        {
            string filePathPlusDate = _filepath + "Orders_" + dateTime.ToString("MMddyyyy") + ".txt";
            List<Order> orderList = new List<Order>();

            if (!File.Exists(filePathPlusDate))
            {
                return orderList;
            }
            else
            {
                using (StreamReader sr = new StreamReader(filePathPlusDate))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total")
                        {
                            continue;
                        }
                        Order order = new Order();
                        try
                        {
                            string[] colums = line.Split(',');

                            order.OrderNumber = int.Parse(colums[0]);
                            order.Date = dateTime;
                            order.CustomerName = colums[1].Replace('~', ',');
                            order.State = colums[2];
                            order.TaxRate = decimal.Parse(colums[3]);
                            order.ProductType = colums[4];
                            order.Area = decimal.Parse(colums[5]);
                            order.CostPerSquareFoot = decimal.Parse(colums[6]);
                            order.LaborCostPerSquareFoot = decimal.Parse(colums[7]);
                            order.MaterialCost = decimal.Parse(colums[8]);
                            order.LaborCost = decimal.Parse(colums[9]);
                            order.Tax = decimal.Parse(colums[10]);
                            order.Total = decimal.Parse(colums[11]);

                            orderList.Add(order);


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occured while writing the list of Orders.");
                        }
                    }
                }
                return orderList;
            }
        }

        public bool AddOrder(Order order)
        {
            string filePathPlusDate = _filepath + "Orders_" + order.Date.ToString("MMddyyyy") + ".txt";
            bool FileHeader = true;
            List<Order> listOfOrders = LoadAllOrders(order.Date);
            try
            {
                if (listOfOrders.Count() > 0)
                {
                    int newOrderNumber = listOfOrders.Select(o => o.OrderNumber).Max() + 1;
                    order.OrderNumber = newOrderNumber;
                }
                else
                {
                    order.OrderNumber = 0;
                }
                listOfOrders.Add(order);
                if (File.Exists(filePathPlusDate))
                {
                    File.Delete(filePathPlusDate);
                }
                File.Create(filePathPlusDate).Close();

                using (StreamWriter sw = new StreamWriter(filePathPlusDate))
                {
                    foreach (Order o in listOfOrders)
                    {
                        if (FileHeader)
                        {
                            sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                            FileHeader = false;
                        }
                        sw.WriteLine($"{o.OrderNumber},{o.CustomerName.Replace(',', '~')},{o.State},{o.TaxRate},{o.ProductType},{o.Area},{o.CostPerSquareFoot:F02},{o.LaborCostPerSquareFoot:F02},{o.MaterialCost:F02},{o.LaborCost:F02},{o.Tax:F02},{o.Total:F02}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an issue writing out your order - Data may have been lost");
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public Order LoadOrder(int OrderNumber, DateTime orderDate)
        {
            return LoadAllOrders(orderDate).Where(o => o.OrderNumber == OrderNumber && o.Date == orderDate).FirstOrDefault();
        }
        public bool EditOrder(Order oldOrder, Order newOrder, DateTime orderDate, int orderNumber)
        {
            List<Order> loadOrders = LoadAllOrders(orderDate);
            oldOrder = loadOrders.Where(o => o.OrderNumber == orderNumber && o.Date == orderDate).FirstOrDefault();
            newOrder.OrderNumber = oldOrder.OrderNumber;
            newOrder.Date = oldOrder.Date;
            RemoveOrder(oldOrder.OrderNumber, oldOrder.Date);
            AddOrder(newOrder);
            return true;
        }

        public bool RemoveOrder(int orderNumber, DateTime orderDate)
        {
            string filePathPlusDate = _filepath + "Orders_" + orderDate.Date.ToString("MMddyyyy") + ".txt";
            bool flag = true;
            bool FileHeader = true;
            while (flag)
            {
                List<Order> loadOrders = LoadAllOrders(orderDate);
                Order orderToDelete = loadOrders.Where(o => o.OrderNumber == orderNumber && o.Date == orderDate).FirstOrDefault();

                try
                {
                    if (File.Exists(filePathPlusDate))
                    {
                        //Order orderToDelete = LoadOrder(orderNumber, orderDate);
                        loadOrders.Remove(orderToDelete);
                        
                        using (StreamWriter sw = new StreamWriter(filePathPlusDate))
                        {
                            foreach (Order o in loadOrders)
                            {
                                if (FileHeader)
                                {
                                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                                    FileHeader = false;
                                }
                                sw.WriteLine($"{o.OrderNumber},{o.CustomerName.Replace(',', '~')},{o.State},{o.TaxRate},{o.ProductType},{o.Area},{o.CostPerSquareFoot:F02},{o.LaborCostPerSquareFoot:F02},{o.MaterialCost:F02},{o.LaborCost:F02},{o.Tax:F02},{o.Total:F02}");
                            }
                        }
                        flag = false;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("There was an issue deleting your order. The order was not deleted.");
                }

                if (loadOrders.Count == 0)
                {
                    File.Delete(filePathPlusDate);
                    Console.WriteLine("There Are No More Orders In this File So The File Was Deleted.");
                    flag = false;
                }
                
            }
            return true;
        }
    }
}




