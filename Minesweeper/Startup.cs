

using System;

public class Startup
{
    public static void Main(string[] args)
    {
            Console.Clear();
            const int fieldDimension = 10;
            const int mineCount = 10;
            Minefield m = new Minefield(fieldDimension, mineCount);
            m.PreviewMinefield(true);
            Console.ReadKey();
    }
}