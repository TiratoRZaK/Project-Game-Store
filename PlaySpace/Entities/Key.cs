﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Entities
{
    public class Key
    {
        public int Id { get; set; }
        public string Item { get; set; }

        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}