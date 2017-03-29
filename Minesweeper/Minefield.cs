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
        this.FieldLayout = Minefield.GenerateLayout(fieldLength, mineCount);
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

    private static Square[,] GenerateLayout(int fieldLength, int mineCount)
    {
        Square[,] mineField = new Square[fieldLength, fieldLength];
        Position[] minePositions = RandomiseMinePositions(fieldLength, mineCount);
        for (int i = 0; i < fieldLength; i++)
        {
            for (int j = 0; j < fieldLength; j++)
            {
                mineField[i, j] = new Square(i, j, false);
            }
        }

        foreach (var sqr in mineField)
        {
            var positions = sqr.AdjacentPositions;
            sqr.AdjacentPositions = Minefield.RemoveInvalidPositions(positions, fieldLength);
        }

        foreach (var mine in minePositions)// add mines to field
        {
            mineField[mine.Row, mine.Column].IsBomb = true;
            AddBombToAdjacent(mineField, mine.Row, mine.Column);
        }

        return mineField;
    }

    private static void AddBombToAdjacent(Square[,] mineField, int row, int col)
    {
        var adjacentPositions = mineField[row, col].AdjacentPositions;
        foreach (var position in adjacentPositions)
        {
            mineField[position.Row, position.Column].MinesNearby++;
        }
    }

    private static Position[] RemoveInvalidPositions(Position[] adjacentPositions, int fieldLength)
    {
        List<Position> validPositions = new List<Position>();
        foreach (var position in adjacentPositions)
        {
            if (position.IsValid(fieldLength))
            {
                validPositions.Add(position);
            }
        }

        var validPosArray = validPositions.ToArray();
        return validPosArray;
    }

    public void PreviewMinefield(bool showMines = false)
    {
        int sideLength = this.FieldLength;
        int lastIndex = sideLength + 2;
        char[][] mineField = new char[sideLength + 1][];//one more line for indexing at top
        mineField[0] = new char[lastIndex];
        for (int i = 2; i < lastIndex; i++)// indexing at top, starts at two because of side indexing
        {
            mineField[0][i] = (char)(i + 46);
        }

        for (int i = 1; i < sideLength + 1; i++)
        {
            mineField[i] = new char[lastIndex];
            mineField[i][0] = (char)(i + 47);//indexing on the left
            mineField[i][1] = ' ';

            for (int j = 2; j < lastIndex; j++)
            {
                char currentSquare = '-';
                if (showMines && this.FieldLayout[i - 1, j - 2].IsBomb)
                {
                    currentSquare = 'o';
                }

                mineField[i][j] = currentSquare;
            }
        }

        foreach (char[] item in mineField)
        {
            Console.WriteLine(new string(item));
        }
    }
}