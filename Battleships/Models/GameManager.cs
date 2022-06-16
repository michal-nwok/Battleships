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
        public Board Board { get; set; }
        public List<string> Messages { get; set; } = new();

        public void StartMatch()
        {
            Board = new Board();
            IsPlaying = true;

            InitializeShips();
            
            Board.PrintBoard();
            
            while(IsPlaying)
            {
                Board.PrintBoard();
                foreach (var message in Messages)
                {
                    Console.WriteLine(message);
                }
                Messages.Clear();

                var coordinates = GetCoordinatesFromInput();

                if(coordinates.Count <= 0)
                {

                    continue;

                }

                var slot = Board.GameBoard[coordinates[1], coordinates[0]];

                CheckSlot(slot);
                
                if(Board.Ships.Count <= 0)
                {

                    FinishMatch();

                }
                
            }
        }

        private void FinishMatch()
        {
            IsPlaying = false;
            Board.PrintBoard();
            Console.WriteLine("You've won. Press Enter to restart game or ESC to quit.");
            var input = Console.ReadKey();
            
            switch(input.Key)
            {
                case ConsoleKey.Enter:
                    StartMatch();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    FinishMatch();
                    break;
            }
            
        }

        private void CheckSlot(Slot slot)
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
                    slot.Status = slot.Ship.Status;
                    if (slot.Ship.IsDead)
                    {

                        Messages.Add($"You've successfully shotdown {slot.Ship.Name}");
                        Board.Ships.Remove(Board.Ships.Where(x => x == slot.Ship).First());
                        SetSlotsOfShipAsShotdown(slot.Ship);
                    }
                }
                else
                {

                    Messages.Add("You've missed");
                    slot.Status = Status.Missed;

                }
            }
        }

        private void SetSlotsOfShipAsShotdown(Ship ship)
        {
            for(int i = 0; i < Board.Height; i++)
            {
                for(int j = 0; j < Board.Width; j++)
                {
                    if (Board.GameBoard[i, j].Ship == ship)
                    {
                        Board.GameBoard[i, j].Status = Status.Shotdown;
                    }
                }
            }
        }

        private void InitializeShips()
        {
            Board.Ships.Add(new Battleship());
            Board.Ships.Add(new Destroyer());
            Board.Ships.Add(new Destroyer());

            foreach (Ship ship in Board.Ships)
            {

                Board.PutShipInRandomPlace(ship);

            }
        }

        private List<int> GetCoordinatesFromInput()
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
