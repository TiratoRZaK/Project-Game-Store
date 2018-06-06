using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using System.Web;
using PlaySpace.Models;

namespace PlaySpace.Models
{
    public class GameListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public int CurrentSort { get; set; }
    }
}