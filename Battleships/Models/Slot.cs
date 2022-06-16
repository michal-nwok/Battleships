using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class Slot
    {
        public Status Status { get; set; }
        public Position Position { get; set; }
        public Ship? Ship { get; set; }

        public Slot(int row, int column)
        {
            Position = new Position(row, column);
            Status = Status.Empty;
        }

        public string PrintStatus
        {
            get
            {
                return ((DescriptionAttribute)Status.GetType().GetField(Status.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).ElementAt(0)).Description;
            }
        }

        public bool hasShip
        {
            get
            {
                return Ship != null;
            }
        }

        public bool wasChecked
        {
            get
            {
                return Status == Status.Missed || Status == Status.Shotdown;
            }
        }
    }
}
