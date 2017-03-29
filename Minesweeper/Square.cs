
using System;
using System.Collections.Generic;

public class Square
{
    public bool IsBomb { get; set; }
    public bool IsUncovered { get; set; }
    public int MinesNearby { get; set; }
    public Position[] AdjacentPositions { get; set; }
    public Square(int row, int col, bool isBomb)
    {
        this.IsBomb = isBomb;
        this.IsUncovered = false;
        this.MinesNearby = 0;
        this.AdjacentPositions = GetAdjacentPositions(row, col);
    }

    private Position[] GetAdjacentPositions(int row, int col)
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