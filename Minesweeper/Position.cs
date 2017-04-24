public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool HasChanged { get; set; }
    public Position(int row, int col)
    {
        this.Row = row;
        this.Column = col;
        this.HasChanged = true;
    }

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
            default:
                break;
        }
    }
}