using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Board
    {
        public List<Tab> Tabs { get; set; }

        public Board()
        {
            this.Tabs = new List<Tab>();
        }

    }
}
