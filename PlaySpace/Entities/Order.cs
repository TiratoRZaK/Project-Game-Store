using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime? DataPay { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AllPrice { get; set; }
        ICollection<OrdGame> OrdGames { get; set; }
    }
}