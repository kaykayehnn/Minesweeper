using System;
using System.Collections.Generic;

public class Startup
{
    public static void Main(string[] args)
    {
        const int fieldDimension = 10;
        const int mineCount = 10;
        Minefield m = new Minefield(fieldDimension, mineCount);
        m.PreviewMinefield();
    }
}