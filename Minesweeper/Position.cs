public class Position
{
    public Position(int row, int col)
    {
        this.Row = row;
        this.Column = col;
        this.HasChanged = true;
    }

    public int Row { get; private set; }
    public int Column { get; private set; }

    public bool IsValid(int fieldLength)
    {
        bool isValid = this.Row >= 0 && this.Row < fieldLength
                    && this.Column >= 0 && this.Column < fieldLength;
        return isValid;
    }

    public void Move(char direction, int fieldLength)
    {
        this.HasChanged = false;
        switch (direction)
        {
            case 'w':
                if (this.Row != 0)
                {
                    this.Row--;
                    this.HasChanged = true;
                }
                break;
            case 's':
                if (this.Row != fieldLength - 1)
                {
                    this.Row++;
                    this.HasChanged = true;
                }
                break;
            case 'a':
                if (this.Column != 0)
                {
                    this.Column--;
                    this.HasChanged = true;
                }
                break;
            case 'd':
                if (this.Column != fieldLength - 1)
                {
                    this.Column++;
                    this.HasChanged = true;
                }
                break;
        }
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 17 + this.Row;
        hash = hash * 17 + this.Column;
        return hash;
    }
}