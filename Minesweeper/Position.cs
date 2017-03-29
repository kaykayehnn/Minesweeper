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
}