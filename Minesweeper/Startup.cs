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
            PrintControls();

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
        Console.WriteLine();
        Console.WriteLine("Use arrows or WASD for moving the pointer.");
        Console.WriteLine("Enter to open field.");
        Console.WriteLine("Space to flag field.");
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