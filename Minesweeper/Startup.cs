using System;

public class Startup
{
    public static void Main(string[] args)
    {
        const int fieldDimension = 4;
        const int mineCount = 2;

        //StartView();

        Minefield board = new Minefield(fieldDimension, mineCount);
        while (!board.GameFinished)
        {
            Console.Clear();
            board.Preview(true); // should be false

            string[] userInput = Console.ReadLine().Split();


            bool validInput = ValidateInput(userInput);
            if (validInput)
            {
                string command = userInput[0].Trim();
                int row = int.Parse(userInput[1]);
                int col = int.Parse(userInput[2]);
                board.AggregateCommand(command, row, col);
            }
            else
            {
                Console.WriteLine("Please enter valid command.");
                continue;
            }
        }
        board.DetermineOutcome();
        Delay(2000);
        Console.Clear();
        if (board.UserWon)
        {
            Console.WriteLine("Congratulations! Thanks for playing.");
        }
        else
        {
            Console.WriteLine("Unlucky!");
        }
    }

    private static bool ValidateInput(string[] userInput)
    {
        if (userInput.Length != 3)
        {
            return false;
        }

        string command = userInput[0];
        bool invalidCoords = !int.TryParse(userInput[1].Trim(), out int row) || !int.TryParse(userInput[2].Trim(), out int col);// returns true if success
        bool validInput = (command.Equals("open") || command.Equals("flag")) ^ invalidCoords;

        return validInput;
    }

    private static void StartView()
    {
        //Console.Clear();
        //PrintGreeting();
        //Delay(2000);
        PrintControls();
        Delay(2000);
    }

    private static void PrintControls()
    {
        Console.WriteLine("Input syntax:");
        Delay(750);
        Console.WriteLine($"open {{row}} {{col}} {Environment.NewLine}flag {{row}} {{col}}");
        Delay(1500);
        Console.WriteLine("Good Luck.");
    }

    private static void PrintGreeting()
    {
        Console.WriteLine("Welcome to Minesweeper.");
    }
    
    private static void Delay(int millis)
    {
        System.Threading.Thread.Sleep(millis);
    }
}