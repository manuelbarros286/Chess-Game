namespace ChessController;

public class Counting
{
    //count how many pieces of the opponent the player has captured
    private readonly Dictionary<PieceType, int> whiteCount = new Dictionary<PieceType, int>();
    private readonly Dictionary<PieceType, int> blackCount = new Dictionary<PieceType, int>();
    
    public int TotalCount { get; private set; }

    public Counting()
    {
        foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
        {
            whiteCount[type] = 0;
            blackCount[type] = 0;
        }
    }
    
    public void Increment(Player colour, PieceType type)
    {
        if (colour == Player.White)
        {
            whiteCount[type]++;
        }
        else
        {
            blackCount[type]++;
        }
        TotalCount++;
    }

    public int White(PieceType type)
    {
        return whiteCount[type];
    }
    
    public int Black(PieceType type)
    {
        return blackCount[type];
    }
}