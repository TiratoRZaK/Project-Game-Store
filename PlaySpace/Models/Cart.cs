using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Models
{
    public class Cart
    {
        public static int TotalQuantity=0;
        private List<CartLine> lineCollection = new List<CartLine>();

         public void AddItem(Game game, int quantity)
        {
            TotalQuantity++;
            CartLine line = lineCollection
                .Where(g => g.Game.GameId == game.GameId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Game game)
        {
            foreach( var r in lineCollection)
            {
                if (r.Game.GameId == game.GameId)
                    TotalQuantity = TotalQuantity - r.Quantity;
            }
            lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Game.Price / 100 * (100 - e.Game.Discount) * e.Quantity);
        }

        public decimal CountTotalValue()
        {
            return TotalQuantity;
        }

        public void Clear()
        {
            TotalQuantity = 0;
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        
    }

    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}