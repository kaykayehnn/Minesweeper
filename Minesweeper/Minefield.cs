using System;
using System.Collections.Generic;

public class Minefield
{
    public Square[,] FieldLayout { get; } // [col,row]
    public int FieldLength { get; }
    public int MineCount { get; }
    public bool PlayerLost { get; private set; }
    public int FieldsCovered { get; private set; }
    public bool UserWon { get; private set; }
    public Position Pointer { get; private set; }

    public Minefield(int fieldLength, int mineCount)
    {
        this.FieldLength = fieldLength;
        this.MineCount = mineCount;
        this.FieldLayout = GenerateLayout(this.FieldLength, this.MineCount);
        this.FieldsCovered = fieldLength * fieldLength; // bools are false by default
        this.Pointer = new Position(0, 0);
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
        HashSet<KeyValuePair<int, int>> positionsAdded = new HashSet<KeyValuePair<int, int>>();
        while (minesPositioned != mineCount)
        {
            int randomRow = r.Next(0, fieldLength);
            int randomCol = r.Next(0, fieldLength);
            var pair = new KeyValuePair<int, int>(randomRow, randomCol);
            if (!positionsAdded.Contains(pair))
            {
                minePositions[minesPositioned] = new Position(randomRow, randomCol);
                positionsAdded.Add(pair);
                minesPositioned++;
            }
        }

        return minePositions;
    }

    private void AddBombToAdjacent(Square[,] mineField, int row, int col)
    {
        var adjacentPositions = mineField[row, col].AdjacentPositions;
        foreach (var position in adjacentPositions)
        {
            mineField[position.Row, position.Column].MinesNearby++;
        }
    }

    public void ProcessCommand(ConsoleKeyInfo key)
    {
        char direction = ' ';
        if (key.KeyChar == 'w' || key.KeyChar == 'W' || key.Key == ConsoleKey.UpArrow) direction = 'w';
        else if (key.KeyChar == 's' || key.KeyChar == 'S' || key.Key == ConsoleKey.DownArrow) direction = 's';
        else if (key.KeyChar == 'a' || key.KeyChar == 'A' || key.Key == ConsoleKey.LeftArrow) direction = 'a';
        else if (key.KeyChar == 'd' || key.KeyChar == 'D' || key.Key == ConsoleKey.RightArrow) direction = 'd';

        if (direction != ' ') Pointer.Move(direction, this.FieldLength);

        else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
        {
            var currSquare = this.FieldLayout[Pointer.Row, Pointer.Column];
            int row = this.Pointer.Row;
            int col = this.Pointer.Column;
            if (key.Key == ConsoleKey.Enter)
            {
                if (currSquare.IsMine)
                {
                    this.PlayerLost = true;
                }
                else
                {
                    if (currSquare.IsHidden)
                    {
                        this.FieldLayout[row, col].IsHidden = false;
                        this.FieldsCovered--;
                        RecursiveUncover(currSquare);
                    }
                }
            }
            else
            {
                currSquare.IsFlagged ^= true; // change value
            }
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

    public void Preview(bool showMines)
    {
        int sideLength = this.FieldLength;
        char[][] mineField = new char[sideLength][];//one more line for indexing at top
        mineField[0] = new char[sideLength];

        for (int i = 0; i < sideLength; i++)
        {
            mineField[i] = new char[sideLength];

            for (int j = 0; j < sideLength; j++)
            {
                char currentChar = '-';
                var currentSquare = this.FieldLayout[i, j];
                bool isPointer = this.Pointer.Row == i && this.Pointer.Column == j;
                if ((showMines || this.UserWon) && currentSquare.IsMine)
                {
                    currentChar = '\u00B7';//middle dot
                }
                else if (isPointer)
                {
                    currentChar = '\u25AF';
                }

                else if (!currentSquare.IsHidden)
                {
                    currentChar = (char)(currentSquare.MinesNearby + '0'); // to ascii number
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

    public bool PlayerWon()
    {
        return this.MineCount == this.FieldsCovered;
    }
}