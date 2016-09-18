using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Board
    {
        public List<Tab> Tabs { get; set; }
        public List<Position> Items { get; set; }

        int lastY = 0;
        int lastX = 0;

        public int UpValue { get; set; }
        public int DownValue { get; set; }


        public Board()
        {
            Init();
        }

        void Init()
        {
            this.Tabs = new List<Tab>();
            this.Items = new List<Position>();
            this.lastX = 0;
            this.lastY = 0;
            this.UpValue = 0;
            this.DownValue = 0;
        }



    }
}
