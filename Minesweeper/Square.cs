
using System;

public class Square
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsBomb { get; set; }
    public bool IsUncovered { get; set; }
    public int MinesNearby { get; set; }
    public Position[] PositionsAround { get; set; }

    public Square(int row, int col, bool isBomb)
    {
        this.Row = row;
        this.Column = col;
        this.IsBomb = isBomb;
        this.IsUncovered = false;
        this.MinesNearby = 0;
        this.PositionsAround = AdjacentPositions(row, col);
    }

    private Position[] AdjacentPositions(int row, int col)
    {
        Position[] adjecentPos = new Position[8] {
            new Position(row-1,col-1),
            new Position(row-1,col),
            new Position(row-1,col+1),
            new Position(row,col-1),
            new Position(row,col+1),
            new Position(row+1,col-1),
            new Position(row+1,col),
            new Position(row+1,col+1)
        };
        return adjecentPos;
    }
}