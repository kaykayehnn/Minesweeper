using System;

public class Startup
{
    public static void Main(string[] args)
    {
        int fieldDimension = 9;
        int mineCount = 10;

        StartView();

        Minefield board = new Minefield(fieldDimension, mineCount);
        while (board.PlayerWon() == board.PlayerLost)
        {
            Console.Clear();
            board.Preview(false); // should be false
            PrintControls();

            var userInput = Console.ReadKey();

            board.ProcessCommand(userInput);
        }
        
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
        Delay(5000);
        Console.WriteLine("Press any key to leave.");
        Console.ReadKey();
    }

    private static void StartView()
    {
        Console.Clear();
        PrintGreeting();
        Delay(2000);
    }

    private static void PrintControls()
    {
        Console.WriteLine();
        Console.WriteLine("Use arrows or WASD for moving the pointer.");
        Console.WriteLine("Enter to open field.");
        Console.WriteLine("Space to flag field.");
    }

    private static void PrintGreeting()
    {
        Console.WriteLine("Welcome to Minesweeper.");
        Delay(1500);
        Console.WriteLine("Good luck.");
    }

    private static void Delay(int millis)
    {
        System.Threading.Thread.Sleep(millis);
    }
}