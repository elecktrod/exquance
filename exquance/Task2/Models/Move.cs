using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.Models
{
    public class Move
    {
        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }
        public int y { get; set; }
    }
}
