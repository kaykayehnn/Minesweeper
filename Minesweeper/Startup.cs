using System;
using System.Threading.Tasks;

public class Startup
{
    public static void Main()
    {
        int fieldDimension = 9;
        int mineCount = 10;

        StartView();
        Console.CursorVisible = false;
        bool playerIsAddicted = true;
        while (playerIsAddicted)
        {
            Minefield board = new Minefield(fieldDimension, mineCount);
            while (board.PlayerWon == board.PlayerLost)
            {
                if (board.NeedsUpdate)
                {
                    Console.Clear();
                    board.Preview(true); // show cursor after move
                    PrintControls();
                    board.PrintFlags();
                }
                else
                {
                    Console.Write(" ");
                }

                Console.SetCursorPosition(30,8);
                Task<ConsoleKeyInfo> readCommand = Task.Run(() => ReadUserInput());
                int timePassed = 0;
                while (!readCommand.IsCompleted)
                {
                    timePassed += 1;
                    Delay(1);
                    if (timePassed % 500 == 0)
                    {
                        Console.Clear();
                        board.Blink(timePassed % 1000 == 0);
                        PrintControls();
                        board.PrintFlags();
                    }
                }
                var userInput = readCommand.Result;
                ClearUserInput(board.MineCount);
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

            Console.WriteLine("\nTry again?");

            char input = (char)Console.Read();
            playerIsAddicted = input == 'y' || input == 'Y' /* || true */;
        }

        Console.WriteLine("Thanks for playing!");
        Delay(1000);
        for (int i = 5; i > 0; i--)
        {
            Console.Write($"\rLeaving in {i}");
            Delay(1000);
        }
    }

    private static void ClearUserInput(int mineCount)
    {
        Console.SetCursorPosition(30, 8);
        Console.Write(" ");
    }

    private static ConsoleKeyInfo ReadUserInput()
    {
        return Console.ReadKey();
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
        Console.SetCursorPosition(11, 8);//left bottom corner of board coords at 9x9
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