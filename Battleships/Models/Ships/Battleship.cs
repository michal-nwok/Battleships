using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Size = 5;
            Status = Status.Battleship;
        }
    }
}
