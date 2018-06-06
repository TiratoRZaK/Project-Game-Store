using Data_Access_Layer.Entities;
using Business_Logic_Layer.Models;


namespace Business_Logic_Layer.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(ShippingDetails shippingDetails, Order order);
    }
}

