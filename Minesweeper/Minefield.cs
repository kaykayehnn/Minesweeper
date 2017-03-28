using System;
using System.Collections.Generic;

public class Minefield
{
    public Square[,] FieldLayout { get; set; }
    public int FieldLength { get; set; }
    public bool[,] MinePositions { get; set; }
    public int MineCount { get; set; }

    public Minefield(int fieldLength, int mineCount)
    {
        this.FieldLength = fieldLength;
        this.MineCount = mineCount;
        this.MinePositions = Minefield.RandomiseMinePositions(fieldLength, mineCount);
        this.FieldLayout = Minefield.InitMatrix(fieldLength, this.MinePositions);
    }

    private static bool[,] RandomiseMinePositions(int fieldLength, int mineCount)
    {
        Random r = new Random();
        int minesPositioned = 0;
        int rows = fieldLength;
        int totalPositions = rows * rows;
        bool[,] minePositions = new bool[fieldLength, fieldLength];
        for (int i = 0; i < rows && minesPositioned != mineCount; i++)
        {
            for (int j = 0; j < rows && minesPositioned != mineCount; j++)
            {
                int positionsLeft = totalPositions - (i * rows + j);
                double percentageForPlacement = (mineCount - minesPositioned) * 100.0 / positionsLeft;
                if (r.Next(0, positionsLeft) < percentageForPlacement)
                {
                    minePositions[j, i] = true;
                    minesPositioned++;
                }
            }
        }

        return minePositions;
    }

    private static Square[,] InitMatrix(int fieldLength, bool[,] minePositions)
    {
        Square[,] matrix = new Square[fieldLength, fieldLength];

        for (int i = 0; i < fieldLength; i++)
        {
            for (int j = 0; j < fieldLength; j++)
            {
                bool isBomb = minePositions[i, j];
                Square sq = new Square(j, i, isBomb);
                matrix[i, j] = sq;
            }
        }

        return matrix;
    }

    public void PreviewMinefield(bool showMines = false)
    {
        int sideLength = this.FieldLength;
        int lastIndex = sideLength + 2;
        char[][] matrix = new char[sideLength + 1][];//one more line for indexing at top
        matrix[0] = new char[lastIndex];
        for (int i = 2; i < lastIndex; i++)// indexing at top, starts at two because of side indexing
        {
            matrix[0][i] = (char)(i + 46);
        }

        for (int i = 1; i < sideLength; i++)
        {
            matrix[i] = new char[lastIndex];
            matrix[i][0] = (char)(i + 47);//indexing on the left
            matrix[i][1] = ' ';
            for (int j = 2; j < lastIndex; j++)
            {
                char currentSquare = '-';
                if (showMines && this.MinePositions[i - 1, j - 2])
                {
                    currentSquare = 'o';
                }

                matrix[i][j] = currentSquare;
            }
        }


        foreach (char[] item in matrix)
        {
            Console.WriteLine(new string(item));
        }
    }
}