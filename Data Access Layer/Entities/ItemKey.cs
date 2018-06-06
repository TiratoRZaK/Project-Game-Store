using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_Access_Layer.Entities
{
    public class ItemKey
    {
        public int Id { get; set; }
        public int OrdGameId { get; set; }
        public OrdGame OrdGame { get; set; }
        public string Keys { get; set; }
    }
}