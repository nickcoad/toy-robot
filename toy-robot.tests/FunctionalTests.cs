using NUnit.Framework;
using toy_robot;

namespace Tests
{
    public class FunctionalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MovesInCorrectDirection()
        {
            var toyRobot = new ToyRobot();
            toyRobot.Command("PLACE 0,1,N");
            toyRobot.Command("MOVE");
            Assert.AreEqual(toyRobot.X, 0);
            Assert.AreEqual(toyRobot.Y, 0);

            toyRobot.Command("PLACE 0,0,S");
            toyRobot.Command("MOVE");
            Assert.AreEqual(toyRobot.X, 0);
            Assert.AreEqual(toyRobot.Y, 1);

            toyRobot.Command("PLACE 0,0,E");
            toyRobot.Command("MOVE");
            Assert.AreEqual(toyRobot.X, 1);
            Assert.AreEqual(toyRobot.Y, 0);

            toyRobot.Command("PLACE 1,0,W");
            toyRobot.Command("MOVE");
            Assert.AreEqual(toyRobot.X, 0);
            Assert.AreEqual(toyRobot.Y, 0);
        }

        [Test]
        public void TurnsLeftCorrectly()
        {
            var toyRobot = new ToyRobot();
            toyRobot.Command("PLACE 0,0,N");
            toyRobot.Command("LEFT");
            Assert.AreEqual(toyRobot.Direction, Direction.West);

            toyRobot.Command("LEFT");
            Assert.AreEqual(toyRobot.Direction, Direction.South);

            toyRobot.Command("LEFT");
            Assert.AreEqual(toyRobot.Direction, Direction.East);

            toyRobot.Command("LEFT");
            Assert.AreEqual(toyRobot.Direction, Direction.North);
        }

        [Test]
        public void TurnsRightCorrectly()
        {
            var toyRobot = new ToyRobot();
            toyRobot.Command("PLACE 0,0,N");
            toyRobot.Command("RIGHT");
            Assert.AreEqual(toyRobot.Direction, Direction.East);

            toyRobot.Command("RIGHT");
            Assert.AreEqual(toyRobot.Direction, Direction.South);

            toyRobot.Command("RIGHT");
            Assert.AreEqual(toyRobot.Direction, Direction.West);

            toyRobot.Command("RIGHT");
            Assert.AreEqual(toyRobot.Direction, Direction.North);
        }

        [Test]
        public void PlacesCorrectly()
        {
            var toyRobot = new ToyRobot();
            toyRobot.Command("PLACE 1,2,N");
            Assert.AreEqual(toyRobot.X, 1);
            Assert.AreEqual(toyRobot.Y, 2);
            Assert.AreEqual(toyRobot.Direction, Direction.North);

            toyRobot.Command("PLACE 3,2,S");
            Assert.AreEqual(toyRobot.X, 3);
            Assert.AreEqual(toyRobot.Y, 2);
            Assert.AreEqual(toyRobot.Direction, Direction.South);

            toyRobot.Command("PLACE 2,1,E");
            Assert.AreEqual(toyRobot.X, 2);
            Assert.AreEqual(toyRobot.Y, 1);
            Assert.AreEqual(toyRobot.Direction, Direction.East);

            toyRobot.Command("PLACE 0,1,W");
            Assert.AreEqual(toyRobot.X, 0);
            Assert.AreEqual(toyRobot.Y, 1);
            Assert.AreEqual(toyRobot.Direction, Direction.West);
        }
    }
}