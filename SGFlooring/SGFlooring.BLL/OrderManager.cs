using SGFlooring.Data;
using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class OrderManager
    {
        private IOrderRepository _ordersRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _ordersRepository = orderRepository;
        }

        public OrderResponse DisplayOrder(int orderNumber, DateTime orderDate)
        {
            OrderResponse response = new OrderResponse();

            response.Order = _ordersRepository.LoadOrder(orderNumber, orderDate);
            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"{orderNumber} is not a valid order";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public AddOrderResponse AddOrder(Order orderToAdd)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.Success = _ordersRepository.AddOrder(orderToAdd);
            if (!response.Success)
            {
                response.Message = $"Order was not added.";
                RemoveOrder(orderToAdd.OrderNumber, orderToAdd.Date);
            }
            else
            {
                response.Order = orderToAdd;
                response.Message = $"Order Successfully added.";
            }
            return response;
        }

        public EditOrderResponse EditOrder(Order oldOrder, Order newOrder, DateTime orderDate, int orderNumber)
        {
            EditOrderResponse response = new EditOrderResponse();
            
            response.NewOrder = newOrder;
            response.Success = _ordersRepository.EditOrder(oldOrder, newOrder, orderDate, orderNumber);
            if (!response.Success)
            {
                response.Message = $"Your order was not successfully edited, try again later.";
            }
            else
            {
                response.OldOrder = RemoveOrder(oldOrder.OrderNumber,oldOrder.Date).Order;
                response.NewOrder = AddOrder(newOrder).Order;
                response.Message = $"Your order was successfuly edited.";
            }
            return response;
        }

        public RemoveOrderResponse RemoveOrder(int orderNumber, DateTime orderDateRemove)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();

            response.Order = DisplayOrder(orderNumber, orderDateRemove).Order;
            response.Success = _ordersRepository.RemoveOrder(orderNumber, orderDateRemove);
            if (!response.Success)
            {
                response.Message = $"Your order was not successfully removed, try again later.";
            }
            else
            {
                response.Message = $"Your order was successfuly removed.";
            }
            return response;
        }
    }
}
