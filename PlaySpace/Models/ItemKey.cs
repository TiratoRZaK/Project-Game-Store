using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class ItemKey
    {
        public int Id { get; set; }
        public int OrdGameId { get; set; }
        public OrdGame OrdGame { get; set; }
        public string Keys { get; set; }
    }
}