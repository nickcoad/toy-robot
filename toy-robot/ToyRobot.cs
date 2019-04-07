using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace toy_robot
{
    public class ToyRobot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Direction { get; private set; }

        private bool IsPlaced { get; set; }
        private const int MaxX = 4;
        private const int MaxY = 4;
        private readonly List<string> ValidCommands = new List<string>
        {
            "PLACE",
            "MOVE",
            "LEFT",
            "RIGHT",
            "REPORT",
            "EXIT"
        };

        /// <summary>
        /// Parses the input string for a valid command and executes it.
        /// </summary>
        /// <returns>A confirmation message from the robot.</returns>
        public string Command(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new InvalidCommandException("Invalid command!");

            // PLACE is a special case
            if (command.StartsWith("PLACE"))
            {
                var regex = new Regex("^PLACE ([\\d]+),([\\d]+),([NSEW])$");
                if (!regex.IsMatch(command))
                {
                    throw new InvalidCommandException("Invalid format for PLACE command!");
                }

                var matches = regex.Matches(command);

                // We should have exactly one match, if we have more, something has gone terribly wrong.
                if (matches.Count != 1)
                {
                    throw new Exception($"Unexpected condition: {matches.Count} PLACE matches encountered.");
                }

                var match = matches.Single();
                var xCoord = match.Groups[1].Value;
                var yCoord = match.Groups[2].Value;
                var direction = match.Groups[3].Value;

                // We know from the Regex test that these parses are safe.
                var output = Place(int.Parse(xCoord), int.Parse(yCoord), ParseDirection(direction));
                return output;
            }
            else if (ValidCommands.Contains(command))
            {
                // These commands can only be issued if the robot has been placed on the table.
                if (!IsPlaced)
                    throw new InvalidCommandException($"Unable to process command: {command}. Please PLACE the robot first.");

                string output = null;

                switch (command)
                {
                    case "MOVE":
                        output = Move();
                        break;
                    case "LEFT":
                        output = TurnLeft();
                        break;
                    case "RIGHT":
                        output = TurnRight();
                        break;
                    case "REPORT":
                        output = Report();
                        break;
                    case "EXIT":
                        output = "Goodbye!";
                        break;
                    default:
                        // The Contains check should prevent this scenario, but just in case...
                        throw new InvalidCommandException($"Invalid command: {command}.");
                }
                return output;
            }
            else
            {
                throw new InvalidCommandException($"Invalid command: {command}.");
            }
        }

        /// <summary>
        /// Moves the robot 1 unit forward in its current direction.
        /// </summary>
        /// <returns>A confirmation message from the robot.</returns>
        private string Move()
        {
            bool offTableAttempted = false;
            switch (Direction)
            {
                case Direction.North:
                    if (Y == 0)
                        offTableAttempted = true;
                    else
                        Y--;
                    break;
                case Direction.South:
                    if (Y == MaxY)
                        offTableAttempted = true;
                    else
                        Y++;
                    break;
                case Direction.East:
                    if (X == MaxX)
                        offTableAttempted = true;
                    else
                        X++;
                    break;
                case Direction.West:
                    if (X == 0)
                        offTableAttempted = true;
                    else
                        X--;
                    break;
                default:
                    throw new Exception("Unsupported direction.");
            }

            var output = string.Empty;
            if (offTableAttempted)
            {
                throw new InvalidCommandException("You tried to move me off the table! Rude!");
            }
            else
            {
                output = $"Okay! I moved {Direction}.";
            }

            return output;
        }

        /// <summary>
        /// Places the robot onto the table at the specified coordinates,
        /// facing the specified direction.
        /// </summary>
        /// <returns>A confirmation message from the robot.</returns>
        private string Place(int x, int y, Direction direction)
        {
            if (x < 0 || x > MaxX || y < 0 || y > MaxY)
            {
                throw new InvalidCommandException("You can't place me there, that's madness!");
            }

            X = x;
            Y = y;
            Direction = direction;
            IsPlaced = true;

            return $"Thanks! I've been placed at X: {x}, Y: {y}, facing {direction}.";
        }

        /// <summary>
        /// Turns the robot 90º to the left.
        /// </summary>
        /// <returns>A confirmation message from the robot.</returns>
        private string TurnLeft()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.West;
                    break;
                case Direction.South:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.North;
                    break;
                case Direction.West:
                    Direction = Direction.South;
                    break;
                default:
                    throw new Exception("Unsupported direction.");
            }

            var output = $"Turned left! I'm now facing {Direction}.";
            return output;
        }

        /// <summary>
        /// Turns the robot 90º to the right.
        /// </summary>
        /// <returns>A confirmation message from the robot.</returns>
        private string TurnRight()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
                default:
                    throw new Exception("Unsupported direction.");
            }

            var output = $"Turned right! I'm now facing {Direction}.";
            return output;
        }

        /// <summary>
        /// Reports the current position of the robot and its direction.
        /// </summary>
        /// <returns>A message from the robot indicating its position and direction.</returns>
        private string Report()
        {
            return $"I'm sitting at X: {X}, Y: {Y}, facing {Direction} and feeling great. Thanks for asking!";
        }

        /// <summary>
        /// Parses a valid Direction enum value from a string.
        /// </summary>
        /// <param name="input">The direction input by the user, expected to be N, S, E or W.</param>
        /// <returns>A valid Direction value corresponding to the string entered.</returns>
        private Direction ParseDirection(string input)
        {
            Direction direction;
            switch (input)
            {
                case "N":
                    direction = Direction.North;
                    break;
                case "S":
                    direction = Direction.South;
                    break;
                case "E":
                    direction = Direction.East;
                    break;
                case "W":
                    direction = Direction.West;
                    break;
                default:
                    throw new Exception("Unsupported direction.");
            }

            return direction;
        }
    }
}