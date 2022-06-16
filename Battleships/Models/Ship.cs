using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int Hits { get; set; }
        public Status Status { get; set; }
        public bool IsDead
        {
            get { return Hits >= Size; }
        }
    }
}
