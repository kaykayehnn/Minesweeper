using System;

public class Minefield
{
    public int[][] FieldLayout { get; set; }
    public int FieldLength { get; set; }
    public Mine[] Mines { get; set; }
    public int MineCount { get; set; }

    public Minefield(int fieldLength, int mineCount)
    {
        this.FieldLength = fieldLength;
        this.MineCount = mineCount;
        this.Mines = Minefield.RandomiseMinePositions(this.FieldLayout, mineCount);
        this.FieldLayout = Utilities.InitMatrix(fieldLength, this.Mines);
    }
    
    private static Mine[] RandomiseMinePositions(int[][] fieldLayout, int mineCount)
    {
        Random r = new Random();
        int minesPositioned = 0;
        int rows = fieldLayout.Length;
        int cols = rows;
        int totalPositions = rows * cols;
        Mine[] minePositions = new Mine[mineCount];
        for (int i = 0; i < rows && minesPositioned != mineCount; i++)
        {
            for (int j = 0; j < cols && minesPositioned != mineCount; j++)
            {
                int positionsLeft = totalPositions - (i * rows + j);
                double percentageForPlacement = (mineCount - minesPositioned) * 100.0 / positionsLeft ;
                if (r.Next(0, positionsLeft) < percentageForPlacement)
                {
                    minePositions[minesPositioned] = new Mine(i, j);
                    minesPositioned++;
                }
            }
        }

        return minePositions;
    }

    public void PreviewMinefield(bool showMines = false)
    {
        char[][] output = Utilities.CharJaggedArr(this.FieldLength);

        if (showMines)
        {
            foreach (var mine in this.Mines)
            {
                output[mine.Row][mine.Column] = 'o';
            }
        }
        foreach (char[] item in output)
        {
            Console.WriteLine(new string(item));
        }
    }
}