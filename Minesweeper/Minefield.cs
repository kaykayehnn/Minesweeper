using System;
using System.Collections.Generic;

public class Minefield
{
    public Square[,] FieldLayout { get; set; } // [col,row]
    public int FieldLength { get; set; }
    public int MineCount { get; set; }
    public bool GameFinished { get; set; }
    public int FieldsCovered { get; set; }
    public bool UserWon { get; set; }

    public Minefield(int fieldLength, int mineCount)
    {
        this.FieldLength = fieldLength;
        this.MineCount = mineCount;
        this.FieldLayout = GenerateLayout(this.FieldLength, this.MineCount);
        this.FieldsCovered = fieldLength * fieldLength; // bools are false by default
    }

    private Square[,] GenerateLayout(int fieldLength, int mineCount)
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
            sqr.AdjacentPositions = sqr.RemoveInvalidPositions(fieldLength);
        }

        foreach (var mine in minePositions)// add mines to field
        {
            mineField[mine.Row, mine.Column].IsMine = true;
            AddBombToAdjacent(mineField, mine.Row, mine.Column);
        }

        return mineField;
    }

    private Position[] RandomiseMinePositions(int fieldLength, int mineCount)
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
    } // needs improvement, last lines rarely get mines

    private void AddBombToAdjacent(Square[,] mineField, int row, int col)
    {
        var adjacentPositions = mineField[row, col].AdjacentPositions;
        foreach (var position in adjacentPositions)
        {
            mineField[position.Row, position.Column].MinesNearby++;
        }
    }

    private void RecursiveUncover(Square currSquare)
    {
        if (currSquare.MinesNearby != 0)
        {
            return;
        }
        var positions = currSquare.AdjacentPositions;
        foreach (var position in positions)
        {
            var nextSquare = this.FieldLayout[position.Row, position.Column];
            if (nextSquare.IsHidden == true)// to prevent stack overflow
            {
                nextSquare.IsHidden = false;
                this.FieldsCovered--;
                RecursiveUncover(nextSquare);
            }
        }
    }

    private bool IsMine(int row, int col)
    {
        return this.FieldLayout[row, col].IsMine;
    }

    public void Preview(bool showMines = false)
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
                char currentChar = '-';
                var currentSquare = this.FieldLayout[i - 1, j - 2];
                if (showMines && currentSquare.IsMine)
                {
                    currentChar = 'o';
                }
                else if (!currentSquare.IsHidden)
                {
                    currentChar = (char)(currentSquare.MinesNearby + 48); // to ascii number
                }
                else if (currentSquare.IsFlagged)
                {
                    currentChar = 'f';
                }

                mineField[i][j] = currentChar;
            }
        }

        foreach (char[] item in mineField)
        {
            Console.WriteLine(new string(item));
        }
    }

    public void AggregateCommand(string command, int row, int col)
    {
        var currSquare = this.FieldLayout[row, col];
        if (command.Equals("open"))
        {
            if (currSquare.IsMine)
            {
                this.GameFinished = true;
            }
            else if (currSquare.MinesNearby == 0)
            {
                RecursiveUncover(currSquare);
            }
            else // more than 1 mine nearby
            {
                this.FieldLayout[row, col].IsHidden = false;
                this.FieldsCovered--;
            }
        }
        else if (command.Equals("flag"))
        {
            currSquare.IsFlagged ^= true; // change value
        }

        if (this.FieldsCovered == this.MineCount)
        {
            this.GameFinished = true;
        }
    }

    public void DetermineOutcome()
    {
        this.UserWon = FieldsCovered == MineCount;
    }
    
}