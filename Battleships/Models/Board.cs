using Battleships.Helpers;
using Battleships.Models.Ships;
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

        public Slot[,] Grid { get; set; }
        public List<Ship> Ships { get; set; } = new ();

        public Board() 
        {
            Grid = new Slot[Height, Width];
            for(int i = 0; i < Width; i++)
            {

                for(int j = 0; j < Height; j++)
                {

                    Grid[i, j] = new Slot(i, j);

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

                    ConsoleHelper.SetConsoleColorById((int)Grid[x, y].Status);
                    Console.Write($"[{Grid[x , y].PrintStatus}]");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine("\n");
            }
        }

        public void PutShipInRandomPlace(Ship ship)
        {
            var rand = new Random();
            int startingColumn = rand.Next(0, Height);
            int startingRow = rand.Next(0, Width);
            int orientation = rand.Next(1, 3);

            List<Slot> slots = new ();
            if (orientation % 2  != 0)
            {
                if((startingColumn + 1) - ship.Size >= 0)
                {

                    for(int i = 0; i < ship.Size; i++)
                    {

                        slots.Add(Grid[startingRow, startingColumn - i]);

                    }

                }
                else
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(Grid[(startingRow), startingColumn + i]);

                    }

                }
                
            } else
            {

                if ((startingRow + 1) - ship.Size >= 0)
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(Grid[(startingRow - i), startingColumn]);

                    }

                }
                else
                {

                    for (var i = 0; i < ship.Size; i++)
                    {

                        slots.Add(Grid[(startingRow + i), startingColumn]);

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

        public void InitializeShips()
        {
            Ships.Add(new Battleship());
            Ships.Add(new Destroyer());
            Ships.Add(new Destroyer());

            foreach (Ship ship in Ships)
            {

                PutShipInRandomPlace(ship);

            }
        }

        public void CheckSlot(Slot slot)
        {
            if (slot.wasChecked)
            {

                AlertBroker.AddAlert("Slot was already checked");

            }
            else
            {
                if (slot.hasShip)
                {

                    AlertBroker.AddAlert($"You've hit {slot.Ship.Name}");

                    slot.Ship.Hits++;
                    slot.Status = slot.Ship.Status;
                    if (slot.Ship.IsDead)
                    {

                        AlertBroker.AddAlert($"You've successfully shotdown {slot.Ship.Name}");
                        Ships.Remove(Ships.Where(x => x == slot.Ship).First());
                        SetSlotsOfShipAsShotdown(slot.Ship);

                    }
                }
                else
                {

                    AlertBroker.AddAlert("You've missed");
                    slot.Status = Status.Missed;

                }
            }
        }

        private void SetSlotsOfShipAsShotdown(Ship ship)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Grid[i, j].Ship == ship)
                    {
                        Grid[i, j].Status = Status.Shotdown;
                    }
                }
            }
        }

        private static bool SlotsForShipOccupied(List<Slot> slots)
        {
            foreach (var slot in slots)
            {

                if(slot.hasShip)
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

            }
        }
    }
}