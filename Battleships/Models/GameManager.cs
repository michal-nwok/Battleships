using Battleships.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class GameManager
    {
        public bool IsPlaying { get; set; }
        public Board Board { get; set; } = new ();
        public List<string> Messages { get; set; } = new();
        public List<Ship> Ships = new();

        public void startGame()
        {
            IsPlaying = true;
            Ships.Clear();

            Ships.Add(new Battleship());
            Ships.Add(new Destroyer());
            Ships.Add(new Destroyer());

            foreach (Ship ship in Ships)
            {

                Board.PutShipInRandomPlace(ship);

            }
            Board.PrintBoard();
            
            while(IsPlaying)
            {
                Board.PrintBoard();
                foreach (var message in Messages)
                {
                    Console.WriteLine(message);
                }

                Messages.Clear();
                var coordinates = getCoordinatesFromInput();

                if(coordinates.Count() <= 0)
                {

                    continue;

                }

                var slot = Board.GameBoard[coordinates[1], coordinates[0]];

                checkSlot(slot);
                
                if(Ships.Count <= 0)
                {

                    Messages.Add("You've won");
                    IsPlaying = false;

                }
                
            }
        }


        private void checkSlot(Slot slot)
        {
            if (slot.wasChecked)
            {

                Messages.Add("Slot was already checked");

            }
            else
            {
                if (slot.hasShip)
                {
                    Messages.Add($"You've hit {slot.Ship.Name}");

                    slot.Ship.Hits++;
                    slot.Status = Status.Shotdown;
                    if (slot.Ship.IsDead)
                    {

                        Messages.Add($"You've successfully shotdown {slot.Ship.Name}");
                        Ships.Remove(Ships.Where(x => x == slot.Ship).First());

                    }
                }
                else
                {

                    Messages.Add("You've missed");
                    slot.Status = Status.Missed;

                }
            }
        }

        private List<int> getCoordinatesFromInput()
        {
            List<int> coordinates = new();

            Console.Write("Put coords: ");
            var input = Console.ReadLine();

            

            if (string.IsNullOrEmpty(input))
            {

                Messages.Add("Wrong format of coordinates. Try again.");
                return coordinates;

            }

            var rowChar = input.First();
            var columnString = input.Remove(0, 1).ToString();

            if(!Char.IsLetter(rowChar) || !int.TryParse(columnString, out int n))
            {
                Messages.Add("Wrong format of coordinates. Try again.");
                return coordinates;
            }

            var row = (int)input.First() % 32 - 1;
            var column = int.Parse(columnString) - 1;
            
            if((row + 1) > Board.Height || (column + 1) > Board.Width)
            {

                Messages.Add("Coordinates out of bounds. Try again.");
                return coordinates;

            }

            coordinates.Add(row);
            coordinates.Add(column);

            return coordinates;
        }
    }
}
