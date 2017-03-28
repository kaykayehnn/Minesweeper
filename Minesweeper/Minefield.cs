using System;
using System.Collections.Generic;

public class Minefield
{
    public Square[,] FieldLayout { get; set; } // [col,row]
    public int FieldLength { get; set; }
    public Position[] MinePositions { get; set; }
    public int MineCount { get; set; }

    public Minefield(int fieldLength, int mineCount)
    {
        this.FieldLength = fieldLength;
        this.MineCount = mineCount;
        this.MinePositions = Minefield.RandomiseMinePositions(fieldLength, mineCount);
        this.FieldLayout = Minefield.InitMatrix(fieldLength, this.MinePositions);
    }

    private static Position[] RandomiseMinePositions(int fieldLength, int mineCount)
    {
        Random r = new Random();
        int minesPositioned = 0;
        int rows = fieldLength;
        int totalPositions = rows * rows;
        Position[] minePositions = new Position[mineCount];
        for (int i = 0; i < rows && minesPositioned != mineCount; i++)
        {
            for (int j = 0; j < rows && minesPositioned != mineCount; j++)
            {
                int positionsLeft = totalPositions - (i * rows + j);
                double percentageForPlacement = (mineCount - minesPositioned) * 100.0 / positionsLeft;
                if (r.Next(0, positionsLeft) < percentageForPlacement)
                {
                    Position currentPos = new Position(i, j);
                    minePositions[minesPositioned] = currentPos;
                    minesPositioned++;
                }
            }
        }

        return minePositions;
    }

    private static Square[,] InitMatrix(int fieldLength, Position[] minePositions)
    {
        Square[,] matrix = new Square[fieldLength, fieldLength];
        for (int i = 0; i < fieldLength; i++)
        {
            for (int j = 0; j < fieldLength; j++)
            {
                matrix[i, j] = new Square(false);
            }
        }

        foreach (var mine in minePositions)
        {
            matrix[mine.Column, mine.Row].IsBomb = true;
        }

        for (int i = 0; i < fieldLength; i++)
        {
            for (int j = 0; j < fieldLength; j++)
            {
                if (matrix[i, j].IsBomb)
                {
                    AddBombToAdjacent(matrix, j, i);
                }
            }
        }
        return matrix;
    }

    private static void AddBombToAdjacent(Square[,] matrix, int row, int col)
    {
        int fieldLength = matrix.GetLength(0);
        if (col != 0)
        {
            matrix[col - 1, row].MinesNearby++;
            if (row != 0)
            {
                matrix[col - 1, row - 1].MinesNearby++;
                matrix[col, row - 1].MinesNearby++;
            }
            if (row != fieldLength - 1)
            {
                matrix[col - 1, row + 1].MinesNearby++;
                matrix[col, row + 1].MinesNearby++;
            }
        }
        if (col != fieldLength - 1)
        {
            matrix[col + 1, row].MinesNearby++;
            if (row != 0)
            {
                matrix[col + 1, row - 1].MinesNearby++;
            }
            if (row != fieldLength - 1)
            {
                matrix[col + 1, row + 1].MinesNearby++;
            }
        }
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

        for (int i = 1; i < sideLength + 1; i++)
        {
            matrix[i] = new char[lastIndex];
            matrix[i][0] = (char)(i + 47);//indexing on the left
            matrix[i][1] = ' ';

            for (int j = 2; j < lastIndex; j++)
            {
                char currentSquare = '-';
                if (showMines && this.FieldLayout[i - 1, j - 2].IsBomb)
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