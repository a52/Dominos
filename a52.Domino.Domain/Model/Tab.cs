using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Tab
    {
        public int Up_Value { get; set; }
        public int Down_Value { get; set; }
        public int Position { get; set; }

        public bool IsOnBoard { get; set; }
        public bool IsAssigned { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Up_Value, Down_Value);
        }

    }
}
