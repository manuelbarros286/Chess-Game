namespace ChessController;

//inherit from Piece class
public class Pawn : Piece
{
    public override PieceType Type => PieceType.Pawn;
    public override Player Colour { get; }

    public Pawn(Player colour)
    {
        Colour = colour;
        
    }

    public override Piece Copy()
    {
        Pawn copy = new Pawn(Colour);
        copy.HasMoved = HasMoved;
        return copy;
    }
    
}