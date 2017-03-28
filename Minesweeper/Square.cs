
using System;

public class Square
{
    public bool IsBomb { get; set; }
    public bool IsUncovered { get; set; }
    public int MinesNearby { get; set; }

    public Square(bool isBomb)
    {
        this.IsBomb = isBomb;
        this.IsUncovered = false;
        this.MinesNearby = 0;
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