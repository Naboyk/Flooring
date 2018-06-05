using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class OrdersTestRepository : IOrderRepository
    {
        private static Order _order = new Order
        {
            OrderNumber = 0,
            Date = new DateTime(06/02/2018),
            CustomerName = "Test Customer",
            State = "Ohio",
            TaxRate = 6.25M,
            ProductType = "Test Product",
            Area = 10.00M,
            CostPerSquareFoot = 20.00M,
            LaborCostPerSquareFoot = 20.00M,
            MaterialCost = 20.00M,
            LaborCost = 25.00M,
            Tax = 25.00M,
            Total = 100.00M,
        };

        public bool AddOrder(Order order)
        {
            _order = order;
            return true;
        }

        public Order LoadOrder(int OrderNumber, DateTime orderDate)
        {
            if (_order.OrderNumber == OrderNumber)
            {
                return _order;
            }
            else
            {
                return null;
            }
        }

        public bool EditOrder(Order oldOrder, Order newOrder, DateTime orderDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOrder(int OrderNumber, DateTime orderDate)
        {
            return true;
        }

        
    }
}
