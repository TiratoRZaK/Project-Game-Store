using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySpace.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(ShippingDetails shippingDetails, Order order);
    }
}
