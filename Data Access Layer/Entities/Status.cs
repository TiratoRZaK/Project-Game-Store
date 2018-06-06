using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_Access_Layer.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}