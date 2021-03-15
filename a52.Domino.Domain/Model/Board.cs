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
        int index = 0;

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
            this.index = 0;
        }

        public void AddTab(Tab tab, Direction direction)
        {
            var p = new Position();
            p.item = tab;
            p.x = this.lastX;
            p.y = this.lastY;
            p.Index = this.index + 1;

            /// value for not items in the board
            if (Tabs.Count == 0)
            {
                p.x += 1;
                p.y -= 1;
                this.Tabs.Add(tab);
                this.UpValue = tab.Up_Value;
                this.DownValue = tab.Down_Value;
                this.Items.Add(p);

            }
            /// value for item in the board
            else
            {
                if (direction == Direction.Up)
                {
                    p.x += 1;
                    if (this.UpValue == tab.Up_Value)
                        this.UpValue = tab.Down_Value;
                    else if (this.UpValue == tab.Down_Value)
                        this.UpValue = tab.Up_Value;
                    else throw new Exception("Tab does not match");

                }
                else
                {
                    p.y -= 1;
                    if (this.DownValue == tab.Up_Value)
                        this.DownValue = tab.Down_Value;
                    else if (this.DownValue == tab.Down_Value)
                        this.DownValue = tab.Up_Value;
                    else throw new Exception("Tab does not match");
                }

                this.Tabs.Add(tab);
                this.UpValue = tab.Up_Value;
                this.DownValue = tab.Down_Value;
                this.Items.Add(p);

            }

            this.lastY = p.y;
            this.lastX = p.x;
            this.index = p.Index;
        }

    }
    public enum Direction
    {
        Auto = 0,
        Up = 1,
        Down = 2
    }
}
