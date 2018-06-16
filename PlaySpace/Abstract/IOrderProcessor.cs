using PlaySpace.Entities;
using PlaySpace.Models;


namespace PlaySpace.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(ShippingDetails shippingDetails, Order order);
    }
}

