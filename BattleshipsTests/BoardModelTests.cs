using Battleships.Models;
using Battleships.Models.Ships;

namespace BattleshipsTests
{
    public class BoardModelTests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board();
        }

        [Test]
        public void Put_Ship_In_Random_Place()
        {
            Destroyer ship = new ();
            List<Slot> slots = new ();


            _board.PutShipInRandomPlace(ship);
            for(int i = 0; i < _board.Height; i++)
            {
                for(int j = 0; j < _board.Width; j++)
                {
                    if (_board.Grid[i, j].hasShip)
                    {
                        slots.Add(_board.Grid[i, j]);
                    }
                }
            }


            foreach(Slot slot in slots)
            {
                Assert.That(slot.Ship, Is.Not.Null);
            }
            Assert.That(slots, Has.Count.EqualTo(ship.Size));
        }

        [Test]
        public void Put_Two_Ships_In_Random_Places()
        {
            Destroyer destroyer = new ();
            Battleship battleship = new ();
            List<Slot> battleshipSlots = new ();
            List<Slot> destroyerSlots = new();


            _board.PutShipInRandomPlace(destroyer);
            _board.PutShipInRandomPlace(battleship);
            for (int i = 0; i < _board.Height; i++)
            {
                for (int j = 0; j < _board.Width; j++)
                {
                    if (_board.Grid[i, j].hasShip && _board.Grid[i, j].Ship.Status == Battleships.Status.Battleship)
                    {
                        battleshipSlots.Add(_board.Grid[i, j]);
                    }
                    if (_board.Grid[i, j].hasShip && _board.Grid[i, j].Ship.Status == Battleships.Status.Destroyer)
                    {
                        destroyerSlots.Add(_board.Grid[i, j]);
                    }
                }
            }


            foreach (Slot slot in battleshipSlots)
            {
                Assert.That(slot.Ship, Is.Not.Null);
            }
            Assert.That(battleshipSlots, Has.Count.EqualTo(battleship.Size));

            foreach (Slot slot in destroyerSlots)
            {
                Assert.That(slot.Ship, Is.Not.Null);
            }
            Assert.That(destroyerSlots, Has.Count.EqualTo(destroyer.Size));
        }

        [Test]
        public void Check_Slot_That_Was_Already_Checked()
        {
            var status = Battleships.Status.Missed;
            Slot slot = new(0, 0)
            {
                Status = status
            };

            Assert.That(slot.Status, Is.EqualTo(status));
        }

        [Test]
        public void Check_Slot_That_Was_Not_Checked_And_Has_Ship_Not_Sinked()
        {
            Battleship ship = new();
            _board.Ships.Add(ship);
            Slot slot = new(0, 0)
            {
                Ship = ship
            };

            _board.CheckSlot(slot);

            Assert.Multiple(() =>
            {
                Assert.That(slot.Ship.Hits, Is.EqualTo(1));
                Assert.That(slot.Status, Is.EqualTo(ship.Status));
                Assert.That(ship.IsDead, Is.False);
                Assert.That(_board.Ships, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void Check_Slot_That_Was_Not_Checked_And_Has_Ship_Sinked()
        {
            Battleship ship = new()
            {
                Hits = 4,
            };
            _board.Ships.Add(ship);
            Slot slot = new(0, 0)
            {
                Ship = ship
            };

            _board.CheckSlot(slot);

            Assert.Multiple(() =>
            {
                Assert.That(slot.Ship.Hits, Is.EqualTo(5));
                Assert.That(slot.Status, Is.EqualTo(ship.Status));
                Assert.That(ship.IsDead, Is.True);
                Assert.That(_board.Ships, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void Set_Slots_As_Shotdown_For_Battleship()
        {
            int shotdownSlots = 0;
            Battleship ship = new();
            _board.Grid[0, 0].Ship = ship;
            _board.Grid[0, 1].Ship = ship;
            _board.Grid[0, 2].Ship = ship;
            _board.Grid[0, 3].Ship = ship;
            _board.Grid[0, 4].Ship = ship;

            _board.SetSlotsOfShipAsShotdown(ship);
            for(int i = 0; i < _board.Height; i++)
            {

                for(int j = 0; j < _board.Width; j++)
                {

                    if (_board.Grid[i, j].Status == Battleships.Status.Shotdown)
                    {

                        shotdownSlots++;

                    }

                }

            }

            Assert.That(shotdownSlots, Is.EqualTo(5));
        }

        [Test]
        public void Set_Slots_As_Shotdown_For_Destroyer()
        {
            int shotdownSlots = 0;
            Destroyer ship = new();
            _board.Grid[0, 0].Ship = ship;
            _board.Grid[0, 1].Ship = ship;
            _board.Grid[0, 2].Ship = ship;
            _board.Grid[0, 3].Ship = ship;

            _board.SetSlotsOfShipAsShotdown(ship);
            for (int i = 0; i < _board.Height; i++)
            {

                for (int j = 0; j < _board.Width; j++)
                {

                    if (_board.Grid[i, j].Status == Battleships.Status.Shotdown)
                    {

                        shotdownSlots++;

                    }

                }

            }

            Assert.That(shotdownSlots, Is.EqualTo(4));
        }
    }
}