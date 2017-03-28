using System;

public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int col)
    {
        this.Row = row;
        this.Column = col;
    }

    internal static bool IsValid(Position position, int fieldLength)
    {
        bool isValid = position.Row > 0 && position.Row < fieldLength
                    && position.Column > 0 && position.Column < fieldLength;
        return isValid;
    }
}