using NUnit.Framework;
using toy_robot;

namespace Tests
{
    public class InvalidCommandTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InvalidCommandThrowsException()
        {
            var toyRobot = new ToyRobot();
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("THIS IS NOT A COMMAND"));
        }

        [Test]
        public void CommandBeforePlaceThrowsException()
        {
            var toyRobot = new ToyRobot();
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("MOVE"));
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("LEFT"));
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("RIGHT"));
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("REPORT"));
        }

        [Test]
        public void PlaceOffTableThrowsException()
        {
            var toyRobot = new ToyRobot();
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("PLACE 10,10,N"));
        }

        [Test]
        public void MoveOffTableIsPrevented()
        {
            var toyRobot = new ToyRobot();

            // Check the top of the table
            toyRobot.Command("PLACE 0,0,N");
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("MOVE"));

            // Check the bottom of the table
            toyRobot.Command("PLACE 0,4,S");
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("MOVE"));

            // Check the left of the table
            toyRobot.Command("PLACE 0,0,W");
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("MOVE"));

            // Check the right of the table
            toyRobot.Command("PLACE 4,0,E");
            Assert.Throws<InvalidCommandException>(() => toyRobot.Command("MOVE"));
        }
    }
}