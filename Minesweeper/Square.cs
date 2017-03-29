using System.Collections.Generic;

public class Square
{
    public bool IsMine { get; set; }
    public bool IsUncovered { get; set; }
    public int MinesNearby { get; set; }
    public Position[] AdjacentPositions { get; set; }
    public bool IsFlagged { get; set; }

    public Square(int row, int col, bool isBomb)
    {
        this.IsMine = isBomb;
        this.IsUncovered = false;
        this.MinesNearby = 0;
        this.AdjacentPositions = GetAdjacentPositions(row, col);
        this.IsFlagged = false;
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

    public Position[] RemoveInvalidPositions(int fieldLength)
    {
        var adjacentPositions = this.AdjacentPositions;
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
}