﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Tab item { get; set; }
    }
}
