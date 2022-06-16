using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class Board
    {
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;

        public Slot[,] GameBoard { get; set; }

        public Board() 
        {
            GameBoard = new Slot[Height, Width];
            for(int i = 0; i < Width; i++)
            {

                for(int j = 0; j < Height; j++)
                {

                    GameBoard[i, j] = new Slot(i, j);

                }

            }
        }

        public Board(int width, int height)
            :base()
        {
            Width = width;
            Height = height;
        }

        public void PrintBoard()
        {
            Console.Clear();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Console.Write("\t");
            for (int z = 0; z < Width; z++)
            {

                Console.Write($"|{letters[z]}|");

            }
            Console.WriteLine("\n");

            for (int x = 0; x < Height; x++)
            {

                Console.Write($"|{x+1}|\t");
                for (var y = 0; y < Width; y++)
                {

                    setConsoleTextColor((int)GameBoard[x, y].Status);
                    Console.Write($"[{GameBoard[x , y].PrintStatus}]");

                }
                Console.WriteLine("\n");
            }
        }

        public void PutShipInRandomPlace(Ship ship)
        {
            var rand = new Random();
            int startingColumn = rand.Next(0, 10);
            int startingRow = rand.Next(0, 10);
            int orientation = rand.Next(1, 3);

            List<Slot> slots = new ();
            if (orientation % 2  != 0)
            {
                if((startingColumn + 1) - ship.Size >= 0)
                {

                    for(int i = 0; i < ship.Size; i++)
                    {

                        slots.Add(GameBoard[startingRow, startingColumn - i]);

                    }

                }else
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(GameBoard[(startingRow), startingColumn + i]);

                    }

                }
                
            } else
            {

                if ((startingRow + 1) - ship.Size >= 0)
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(GameBoard[(startingRow - i), startingColumn]);

                    }

                }
                else if ((startingRow) - ship.Size <= 0)
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(GameBoard[(startingRow + i), startingColumn]);

                    }

                }

            }

            if (SlotsForShipOccupied(slots))
            {

                PutShipInRandomPlace(ship);

            }
            else
            {

                AddShipToSlots(slots, ship);

            }
        }


        private void setConsoleTextColor(int id)
        {
            switch(id)
            {
                case 1:
                case 2:
                case 3:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        private static bool SlotsForShipOccupied(List<Slot> slots)
        {
            foreach (var slot in slots)
            {

                if(slot.Status != Status.Empty)
                {

                    return true;

                }

            }

            return false;
        }

        private static void AddShipToSlots(List<Slot> slots, Ship ship)
        {
            foreach(var slot in slots)
            {

                slot.Ship = ship;
                slot.Status = ship.Status;

            }
        }
    }
}