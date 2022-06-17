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
        public void Check_Slot_On_Slot_That_Was_Already_Checked()
        {
            Slot slot = new(0, 0)
            {
                Status = Battleships.Status.Missed
            };

            var messages = _board.CheckSlot(slot);
            Assert.Multiple(() =>
            {
                Assert.That(messages, Has.Count.EqualTo(1));
                Assert.That(messages[0], Is.EqualTo("Slot was already checked"));
            });
        }
    }
}