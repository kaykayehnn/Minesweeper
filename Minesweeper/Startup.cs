using System;

public class Startup
{
    public static void Main(string[] args)
    {
        const int fieldDimension = 10;
        const int mineCount = 10;

        StartView();

        Minefield m = new Minefield(fieldDimension, mineCount);
        m.PreviewMinefield(true);
        Console.ReadKey();
    }

    private static void StartView()
    {
        Console.Clear();
        PrintGreeting();
        Delay(2000);
        PrintRules();
        Delay(2000);
        PrintControls();
        Delay(2000);
        Console.Clear();
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
        if(Console.Read() != 'y')
        {
            Console.WriteLine("Too bad.");
        }
    }

    private static void Delay(int millis)
    {
        var startTime = DateTime.Now;
        var endTime = startTime.AddMilliseconds(millis);
        while (startTime < endTime) startTime = DateTime.Now;        
    }
}