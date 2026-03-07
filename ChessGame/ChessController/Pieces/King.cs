namespace ChessController;

public class King : Piece
{
    public override PieceType Type => PieceType.King;
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
    
    private IEnumerable<Position> MovePositions(Position from, Board board)
    {
        foreach (Direction dir in dirs)
        {
            Position to = from + dir;
            if (Board.isInside(to) && (board.isEmpty(to) || board[to].Colour != Colour))
            {
                yield return to;
            }
        }
    }

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        foreach(Position to in MovePositions(from, board))
        {
            yield return new NormalMove(from, to);
        }
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(to =>
        {
            Piece piece = board[to];
            return piece != null && piece.Type == PieceType.King;
        });
    }
}