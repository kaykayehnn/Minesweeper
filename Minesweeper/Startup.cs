using System;
public class Startup
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            const int fieldDimension = 10;
            const int mineCount = 10;
            Minefield m = new Minefield(fieldDimension, mineCount);
            m.PreviewMinefield(true);
        }
    }
}