using System.IO;

namespace ToyRobotSimulation
{
    // Define an enumeration for the directions
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    // Define a class for the ToyRobot
    public class ToyRobot
    {
        //Limits of the table
        public const int TABLE_X = 5;

        public const int TABLE_Y = 5;

        // Properties to store the current position and direction of the robot
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        // Constructor to initialize the robot at the default position and direction
        public ToyRobot()
        {
            X = 0;
            Y = 0;
            Facing = Direction.NORTH;
        }

        // Method to check if the placement of the robot would be valid
        public static bool CheckValidPlacement(int x, int y)
        {
            return (x >= 0 && x < TABLE_X && y >= 0 && y < TABLE_Y);
        }

        // Method to place the robot at a specified position and direction
        public void Place(int x, int y, Direction facing)
        {
            // Check if the specified position is within the bounds of the tabletop
            if (CheckValidPlacement(x, y))
            {
                // Set the position and direction of the robot
                X = x;
                Y = y;
                Facing = facing;
            }
        }

        // Method to move the robot one unit forward in the direction it is currently facing
        public void Move()
        {
            // Move the robot based on its current direction, while ensuring it stays within the bounds
            switch (Facing)
            {
                case Direction.NORTH:
                    if (Y < TABLE_Y - 1)
                        Y++;
                    break;
                case Direction.EAST:
                    if (X < TABLE_X - 1)
                        X++;
                    break;
                case Direction.SOUTH:
                    if (Y > 0)
                        Y--;
                    break;
                case Direction.WEST:
                    if (X > 0)
                        X--;
                    break;
            }
        }

        // Method to rotate the robot 90 degrees to the left without changing its position
        public void Left()
        {
            Facing = (Direction)(((int)Facing + 3) % 4);
        }

        // Method to rotate the robot 90 degrees to the right without changing its position
        public void Right()
        {
            Facing = (Direction)(((int)Facing + 1) % 4);
        }

        // Method to report the current position and direction of the robot
        public void Report()
        {
            // Output the current position and direction of the robot
            Console.WriteLine($"Output: {X},{Y},{Facing}");
        }
    }

    // Main program class
    class Program
    {
        // Entry point of the program
        static void Main()
        {
            // Create a new instance of the ToyRobot class
            ToyRobot robot = new();

            string? input;
            // Keep reading input commands until the user exits
            do
            {
                // Read a line of input from the console
                input = Console.ReadLine()?.Trim();
                if (input != null)
                {
                    // Split the input line into parts based on whitespace
                    string[] parts = input.Split(' ');
                    // Check the command type and perform the corresponding action
                    switch (parts[0])
                    {
                        case "PLACE":
                            // If the command is PLACE, extract the position and direction parameters and place the robot - only if we have a second part
                            if (parts.Length == 2)
                            {
                                string[] placeParams = parts[1].Split(',');
                                if (placeParams.Length == 3)
                                {
                                    bool successX = int.TryParse(placeParams[0], out int x);
                                    bool successY = int.TryParse(placeParams[1], out int y);
                                    bool successF = Enum.TryParse<Direction>(placeParams[2], out Direction facing);
                                    if (successX && successY && successF)
                                        robot.Place(x, y, facing);
                                }
                            }
                            break;
                        case "MOVE":
                            // If the command is MOVE, move the robot one unit forward
                            robot.Move();
                            break;
                        case "LEFT":
                            // If the command is LEFT, rotate the robot 90 degrees to the left
                            robot.Left();
                            break;
                        case "RIGHT":
                            // If the command is RIGHT, rotate the robot 90 degrees to the right
                            robot.Right();
                            break;
                        case "REPORT":
                            // If the command is REPORT, output the current position and direction of the robot
                            robot.Report();
                            break;
                        default:
                            Console.WriteLine("Unknown Command!");
                            break;
                    }
                }
            } while (input != null); // Continue the loop until the user exits

            // For reading from command file, the code would look like below:
            //try
            //{
            //    // Read all lines from the file
            //    string[] commands = File.ReadAllLines(filePath);

            //    // Process each command from the file
            //    foreach (string command in commands)
            //    {
            //        // Split the command into parts based on whitespace
            //        string[] parts = command.Split(' ');
            //        switch (parts[0])
            //        {
            //            case "PLACE":
            //                // If the command is PLACE, extract the position and direction parameters and place the robot - only if we have a second part
            //                if (parts.Length == 2)
            //                {
            //                    string[] placeParams = parts[1].Split(',');
            //                    if (placeParams.Length == 3)
            //                    {
            //                        bool successX = int.TryParse(placeParams[0], out int x);
            //                        bool successY = int.TryParse(placeParams[1], out int y);
            //                        bool successF = Enum.TryParse<Direction>(placeParams[2], out Direction facing);
            //                        if (successX && successY && successF)
            //                            robot.Place(x, y, facing);
            //                    }
            //                }
            //                break;
            //            case "MOVE":
            //                // If the command is MOVE, move the robot one unit forward
            //                robot.Move();
            //                break;
            //            case "LEFT":
            //                // If the command is LEFT, rotate the robot 90 degrees to the left
            //                robot.Left();
            //                break;
            //            case "RIGHT":
            //                // If the command is RIGHT, rotate the robot 90 degrees to the right
            //                robot.Right();
            //                break;
            //            case "REPORT":
            //                // If the command is REPORT, output the current position and direction of the robot
            //                robot.Report();
            //                break;
            //            default:
            //                Console.WriteLine("Unknown Command!");
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle any errors that occur during file reading or command execution
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
        }
    }
}
