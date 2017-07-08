using System;
using System.Collections.Generic;

public class Minefield
{
    private readonly Square[,] fieldLayout; // [col,row]
    private readonly int fieldLength;
    private readonly int mineCount;
    private readonly Position position;
    private int fieldsCovered;

    public Minefield(int fieldLength, int mineCount)
    {
        this.fieldLength = fieldLength;
        this.mineCount = mineCount;
        this.fieldLayout = GenerateLayout();
        this.fieldsCovered = fieldLength * fieldLength;
        this.position = new Position(0, 0);
    }

    public bool PlayerLost { get; private set; }
    public bool PlayerWon { get; private set; }
    public int FlagCounter { get; private set; }

    private Square[,] GenerateLayout()
    {
        Square[,] mineField = new Square[this.fieldLength, this.fieldLength];
        var minePositions = RandomiseMinePositions();
        for (int i = 0; i < this.fieldLength; i++)
        {
            for (int j = 0; j < fieldLength; j++)
            {
                mineField[i, j] = new Square(i, j, false);
            }
        }

        foreach (var sqr in mineField)
        {
            sqr.AdjacentPositions = sqr.RemoveInvalidPositions(fieldLength);
        }

        foreach (var mine in minePositions)// add mines to field
        {
            mineField[mine.Row, mine.Column].IsMine = true;
            AddBombToAdjacent(mineField, mine.Row, mine.Column);
        }

        return mineField;
    }

    private IEnumerable<Position> RandomiseMinePositions()
    {
        Random r = new Random();

        HashSet<Position> positionsAdded = new HashSet<Position>();
        while (positionsAdded.Count != this.mineCount)
        {
            int randomRow = r.Next(0, this.fieldLength);
            int randomCol = r.Next(0, this.fieldLength);

            positionsAdded.Add(new Position(randomRow, randomCol));
        }

        return positionsAdded;
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

        if (direction != ' ') this.position.Move(direction, this.fieldLength);

        else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
        {
            var currSquare = this.fieldLayout[this.position.Row, this.position.Column];
            int row = this.position.Row;
            int col = this.position.Column;
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
                        if (currSquare.IsFlagged)
                        {
                            this.ChangeFlagState(currSquare);
                        }
                        this.fieldLayout[row, col].IsHidden = false;
                        this.fieldsCovered--;
                        RecursiveUncover(currSquare);
                    }
                }
            }
            else // flag command
            {
                if (currSquare.IsHidden)
                {
                    this.ChangeFlagState(currSquare);
                }
            }
        }
    }

    private void ChangeFlagState(Square currSquare)
    {
        if (currSquare.IsFlagged)
        {
            this.FlagCounter--;
        }
        else
        {
            this.FlagCounter++;
        }
        currSquare.IsFlagged ^= true; // change value
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
            var nextSquare = this.fieldLayout[position.Row, position.Column];
            if (nextSquare.IsHidden == true)// to prevent stack overflow
            {
                if (nextSquare.IsFlagged)
                {
                    this.ChangeFlagState(nextSquare);
                }
                nextSquare.IsHidden = false;
                this.fieldsCovered--;
                RecursiveUncover(nextSquare);
            }
        }
    }

    public void UpdateGameState()
    {
        this.PlayerWon = this.fieldsCovered == this.mineCount;
    }

    public void Preview()
    {
        int sideLength = this.fieldLength;

        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                char currentChar = '-';
                var currentSquare = this.fieldLayout[i, j];
                bool isPointer = this.position.Row == i && this.position.Column == j;
                bool gameFinished = this.PlayerLost || this.PlayerWon;
                var currentColor = Console.ForegroundColor;
                if (isPointer && !gameFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if ((this.PlayerLost || this.PlayerWon) && currentSquare.IsMine)
                {
                    currentChar = '\u00B7';//middle dot
                }
                else if (!currentSquare.IsHidden || this.PlayerLost)
                {
                    currentChar = (char)(currentSquare.MinesNearby + '0'); // to ascii number
                }
                else if (currentSquare.IsFlagged)
                {
                    currentChar = 'f';
                }

                Console.Write(currentChar);
                if (isPointer)
                {
                    Console.ForegroundColor = currentColor;
                }
            }
            Console.Write('\n');
        }
    }
}