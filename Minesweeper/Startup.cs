using System;

public class Startup
{
    public static void Main(string[] args)
    {
        int fieldDimension = 9;
        int mineCount = 10;

        //StartView();

        Minefield board = new Minefield(fieldDimension, mineCount);
        while (!board.GameFinished)
        {
            Console.Clear();
            board.Preview(true); // should be false

            var userInput = Console.ReadKey(); 

            board.ProcessCommand(userInput);
            
        }

        board.DetermineOutcome();
        Delay(1000);
        Console.Clear();
        board.Preview(true);
        Console.WriteLine();
        if (board.UserWon)
        {
            Console.WriteLine("Congratulations! You won.");
        }
        else
        {
            Console.WriteLine("Unlucky! You lost.");
        }
        Console.WriteLine("Thanks for playing!");
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
        Console.Clear();
        PrintGreeting();
        Delay(2000);
        PrintControls();
        Delay(5000);
    }

    private static void PrintControls(bool help = false)
    {
        Console.WriteLine("Input syntax:");
        Delay(750);
        Console.WriteLine($"open {{row}} {{col}} {Environment.NewLine}flag {{row}} {{col}}");
        Delay(1500);
        if (!help)
        {
            Console.WriteLine("Good Luck.");
        }
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