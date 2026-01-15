namespace ChessController;

public class Bishop : Piece
{
    public override PieceType Type => PieceType.Bishop;
    public override Player Colour { get; }
    
    public Bishop(Player colour)
    {
        Colour = colour;
    }
    
    
    public override Piece Copy()
    {
        Bishop copy = new Bishop(Colour);
        copy.HasMoved= HasMoved;
        return copy;
    }
}