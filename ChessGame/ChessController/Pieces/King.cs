namespace ChessController;

public class King : Piece
{
    public override PieceType Type => PieceType.King;
    public override Player Colour { get; }
    
    public King(Player colour)
    {
        Colour = colour;
    }
    
    
    public override Piece Copy()
    {
        King copy = new King(Colour);
        copy.HasMoved= HasMoved;
        return copy;
    }
}