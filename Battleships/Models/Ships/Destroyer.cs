using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Size = 4;
            Status = Status.Destroyer;
        }
    }
}
