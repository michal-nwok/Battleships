using Battleships.Helpers;
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

        public void StartMatch()
        {
            Board = new Board();
            IsPlaying = true;

            Board.InitializeShips();
            
            while(IsPlaying)
            {
                Board.PrintBoard();

                AlertBroker.PrintAlerts();
                AlertBroker.ClearAlerts();

                var coordinates = GetCoordinatesFromInput();

                if(coordinates.Count <= 0)
                {

                    continue;

                }

                var slot = Board.Grid[coordinates[1], coordinates[0]];

                Board.CheckSlot(slot);
                
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

        private List<int> GetCoordinatesFromInput()
        {
            List<int> coordinates = new();

            Console.Write("Put coords: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {

                AlertBroker.AddAlert("Wrong format of coordinates. Try again.");
                return coordinates;

            }

            var rowChar = input.First();
            var columnString = input.Remove(0, 1).ToString();

            if(!Char.IsLetter(rowChar) || !int.TryParse(columnString, out int n))
            {

                AlertBroker.AddAlert("Wrong format of coordinates. Try again.");
                return coordinates;

            }

            var row = (int)input.First() % 32 - 1;
            var column = int.Parse(columnString) - 1;
            
            if((row + 1) > Board.Height || (column + 1) > Board.Width)
            {

                AlertBroker.AddAlert("Coordinates out of bounds. Try again.");
                return coordinates;

            }

            coordinates.Add(row);
            coordinates.Add(column);

            return coordinates;
        }
    }
}
