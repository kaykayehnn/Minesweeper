using System.Collections.Generic;

public class Square
{
    public Square(int row, int col, bool isBomb)
    {
        this.IsMine = isBomb;
        this.IsHidden = true;
        this.MinesNearby = 0;
        this.AdjacentPositions = GetAdjacentPositions(row, col);
        this.IsFlagged = false;
    }

    public bool IsMine { get; set; }
    public bool IsHidden { get; set; }
    public int MinesNearby { get; set; }
    public Position[] AdjacentPositions { get; set; }
    public bool IsFlagged { get; set; }
    
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