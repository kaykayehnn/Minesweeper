using System;

public class Startup
{
    public static void Main()
    {
        int fieldDimension = 9;
        int mineCount = 10;

        StartView();

        Minefield board = new Minefield(fieldDimension, mineCount);
        while (board.PlayerWon == board.PlayerLost)
        {
            Console.Clear();
            board.Preview(); // should be false
            PrintControls();
            Console.SetCursorPosition(11, 8);//left bottom corner of board coords at 9x9
            Console.Write($"Flags left: {mineCount - board.FlagCounter}");
            var userInput = Console.ReadKey();

            board.ProcessCommand(userInput);

            board.UpdateGameState();
        }
        
        Delay(1000);
        Console.Clear();
        board.Preview();
        Console.WriteLine();
        if (board.PlayerWon)
        {
            Console.WriteLine("Congratulations! You won.");
        }
        else
        {
            Console.WriteLine("Unlucky! You lost.");
        }

        Console.WriteLine("Thanks for playing!");
        for (int i = 5; i > 0; i--)
        {
            Console.Write($"\rPress any key to leave in {i}");
            Delay(1000);
        }
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