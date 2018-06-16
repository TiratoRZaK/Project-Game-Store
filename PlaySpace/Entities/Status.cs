using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}