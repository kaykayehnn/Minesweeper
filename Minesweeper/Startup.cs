using System;

public class Startup
{
    public static void Main(string[] args)
    {
        const int fieldDimension = 10;
        const int mineCount = 10;

        Console.Clear();
        PrintGreeting();
        PrintRules();
        Console.Clear();

        Minefield m = new Minefield(fieldDimension, mineCount);
        m.PreviewMinefield(true);
        Console.ReadKey();
    }

    private static void PrintGreeting()
    {
        Console.WriteLine("Welcome to Minesweeper.");
    }

    private static void PrintRules()
    {
        Console.WriteLine("The rules are simple:");
        Console.WriteLine("Kill or be killed.");
        Console.WriteLine("Understood?");
        Console.WriteLine("y/n");
        while (Console.ReadLine().Trim().ToLower() != "y") ;

    }


}