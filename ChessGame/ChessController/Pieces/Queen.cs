namespace ChessController;

public class Queen : Piece
{
    public override PieceType Type => PieceType.Queen;
    public override Player Colour { get; }
    
    public Queen(Player colour)
    {
        Colour = colour;
    }
    
    
    public override Piece Copy()
    {
        Queen copy = new Queen(Colour);
        copy.HasMoved= HasMoved;
        return copy;
    }
}