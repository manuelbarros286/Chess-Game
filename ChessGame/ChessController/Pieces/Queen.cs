namespace ChessController;

public class Queen : Piece
{
    public override PieceType Type => PieceType.Queen;
    public override Player Colour { get; }
    
    private static readonly Direction[] dirs = new Direction[]
    {
        Direction.North,
        Direction.NorthEast,
        Direction.East,
        Direction.SouthEast,
        Direction.South,
        Direction.SouthWest,
        Direction.West,
        Direction.NorthWest
    };
    
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
    
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
    }
}