using System;

public class Startup
{
    public static void Main(string[] args)
    {
        const int fieldDimension = 9;
        const int mineCount = 10;

        //StartView();

        Minefield board = new Minefield(fieldDimension, mineCount);
        while (!board.GameFinished)
        {
            Console.Clear();
            board.Preview(true); // should be false

            string[] userInput = Console.ReadLine().Split();
            string command = userInput[0].Trim();

            bool invalidCoords = !int.TryParse(userInput[1].Trim(), out int row);// returns true if success
            invalidCoords |= !int.TryParse(userInput[2].Trim(), out int col);
            bool invalidInput = !(command.Equals("open") || command.Equals("flag"));
            invalidInput |= invalidCoords;
            if (invalidInput)
            {
                Console.WriteLine("Please enter valid command.");
                continue;
            }
            board.AggregateCommand(command, row, col);
        }
    }

    private static void StartView()
    {
        //Console.Clear();
        //PrintGreeting();
        //Delay(2000);
        //PrintRules();
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

    private static void PrintRules()
    {
        Console.WriteLine("The rules are simple:");
        Delay(1000);
        Console.WriteLine("Kill or be killed.");
        Delay(1000);
        Console.WriteLine("Understood?");
        if (Console.Read() != 'y')
        {
            Console.WriteLine("Too bad.");
        }
    }

    private static void Delay(int millis)
    {
        System.Threading.Thread.Sleep(millis);
    }
}