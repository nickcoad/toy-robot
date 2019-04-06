using System;
using System.Linq;

namespace toy_robot
{
    class Program
    {
        static void Main(string[] args)
        {
            var toyRobot = new ToyRobot();

            string command = null;
            while (command != "EXIT")
            {
                Console.Write("Give the robot a command (PLACE, MOVE, LEFT, RIGHT, REPORT): ");
                command = Console.ReadLine();

                try
                {
                    var output = toyRobot.Command(command);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {output}");
                    Console.ResetColor();
                }
                catch (InvalidCommandException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"A problem was encountered: {e.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}
