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

    public bool IsValid(int fieldLength)
    {
        bool isValid = this.Row >= 0 && this.Row < fieldLength
                    && this.Column >= 0 && this.Column < fieldLength;
        return isValid;
    }

    public void Move(char direction, int fieldLength)
    {
        switch (direction)
        {
            case 'w':
                if (this.Row != 0) this.Row--;
                break;
            case 's':
                if (this.Row != fieldLength - 1) this.Row++;
                break;
            case 'a':
                if (this.Column != 0) this.Column--;
                break;
            case 'd':
                if (this.Column != fieldLength - 1) this.Column++;
                break;
            default:
                break;
        }
    }
}