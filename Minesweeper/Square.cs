
public class Square
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsBomb { get; set; }
    public bool IsUncovered { get; set; }
    public int MinesNearby { get; set; }

    public Square(int row, int col, bool isBomb)
    {
        this.Row = row;
        this.Column = col;
        this.IsBomb = isBomb;
        this.IsUncovered = false;
        this.MinesNearby = 0;
    }
}