using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Player
    {

        public Player()
        {
            this.Tabs = new List<Tab>();
        }

        public int OrderBy { get; set; }
        public string PlayerName { get; set; }

        public bool IsMachine { get; set; }
        public bool IsActive { get; set; }

        public List<Tab> Tabs { get; set; }
    }
}
