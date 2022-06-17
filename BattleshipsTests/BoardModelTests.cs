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
            Destroyer ship = new Destroyer();
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
                Assert.NotNull(slot.Ship);
                Assert.That(slot.Status, Is.EqualTo(ship.Status));
            }
            Assert.That(slots.Count(), Is.EqualTo(ship.Size));
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
                Assert.NotNull(slot.Ship);
                Assert.That(slot.Status, Is.EqualTo(battleship.Status));
            }
            Assert.That(battleshipSlots.Count(), Is.EqualTo(battleship.Size));

            foreach (Slot slot in destroyerSlots)
            {
                Assert.NotNull(slot.Ship);
                Assert.That(slot.Status, Is.EqualTo(destroyer.Status));
            }
            Assert.That(destroyerSlots.Count(), Is.EqualTo(destroyer.Size));
        }
    }
}