using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class OrdGame
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<ItemKey> ItemKeys { get; set; }
    }
}