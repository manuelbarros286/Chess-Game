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

    private static bool IsUnmovedRook(Position position, Board board)
    {
        if (board.IsEmpty(position))
        {
            return false;
        }
        Piece piece = board[position];
        return piece.Type == PieceType.Rook && !piece.HasMoved;
    }

    private static bool AllEmpty(IEnumerable<Position> positions, Board board)
    {
        return positions.All(pos => board.IsEmpty(pos));
    }
    
    private bool CanCastleKingside(Position from, Board board)
    {
        if(HasMoved) return false;
        
        Position rookPosition = new Position(from.Row, 7);
        Position[] betweenPositions = new Position[]{ new Position(from.Row, 5) , new Position(from.Row, 6) };
        return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
    }
    
    private bool CanCastleQueenside(Position from, Board board)
    {
        if(HasMoved) return false;
        
        Position rookPosition = new Position(from.Row, 0);
        Position[] betweenPositions = new Position[]{ new Position(from.Row, 1) , new Position(from.Row, 2), new Position(from.Row, 3) };
        return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
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
            if (Board.IsInside(to) && (board.IsEmpty(to) || board[to].Colour != Colour))
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
        
        if (CanCastleQueenside(from, board))
        {
            yield return new Castle(MoveType.CastleQS, from);
        }
        if (CanCastleKingside(from, board))
        {
            yield return new Castle(MoveType.CastleKS, from);
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