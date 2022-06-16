using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public enum Status
    {
        [Description("~")]
        Empty = 1,
        [Description("B")]
        Battleship = 2,
        [Description("D")]
        Destroyer = 3,
        [Description("X")]
        Shotdown = 4,
        [Description("M")]
        Missed = 5,
    }
}
