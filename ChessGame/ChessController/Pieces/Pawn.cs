namespace ChessController;

//inherit from Piece class
public class Pawn : Piece
{
    public override PieceType Type => PieceType.Pawn;
    public override Player Colour { get; }

    private readonly Direction forward;

    public Pawn(Player colour)
    {
        Colour = colour;
        forward = colour == Player.White ? Direction.North : Direction.South;
    }

    public override Piece Copy()
    {
        Pawn copy = new Pawn(Colour);
        copy.HasMoved = HasMoved;
        return copy;
    }
    
    private static bool CanMoveTo(Position pos, Board board)
    {
        return Board.isInside(pos) && board.isEmpty(pos);
    }
    
    //define pawn being able to capture diagonally
    private bool CanCaptureAt(Position pos, Board board)
    {
        if (!Board.isInside(pos) || board.isEmpty(pos)) return false;
        return board[pos].Colour != Colour;
    }
    
    private IEnumerable<Move> ForwardMoves(Position from, Board board)
    {
        Position oneMovePosition = from + forward;
        if (CanMoveTo(oneMovePosition, board))
        {
            yield return new NormalMove(from, oneMovePosition);
            //enable two square move if the pawn hasn't moved yet and is empty
            Position twoMovesPosition = oneMovePosition + forward;
            if (!HasMoved && CanMoveTo(twoMovesPosition, board))
            {
                yield return new NormalMove(from, twoMovesPosition);
            }
        }
    }
    
    private IEnumerable<Move> DiagonalMoves(Position from, Board board)
    {
        //en passant capture positions depend on the pawn's colour
        foreach( Direction dir in new Direction[] { Direction.East, Direction.West })
        {
            Position to = from + forward + dir;
            if (CanCaptureAt(to, board))
            {
                yield return new NormalMove(from, to);
            }
        }
    }
    
    // private IEnumerable<Move> CaptureMoves(Position from, Board board)
    // {
    //     //capture directions depend on the pawn's colour
    //     Direction[] captureDirs = forward == Direction.North
    //         ? new[] { Direction.NorthWest, Direction.NorthEast }
    //         : new[] { Direction.SouthWest, Direction.SouthEast };
    //
    //     foreach (Direction dir in captureDirs)
    //     {
    //         Position capturePos = from + dir;
    //         if (CanCaptureAt(capturePos, board))
    //         {
    //             yield return new NormalMove(from, capturePos);
    //         }
    //     }
    // }
    
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return ForwardMoves(from, board)
            .Concat(DiagonalMoves(from, board));
            // .Concat(CaptureMoves(from, board));
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return DiagonalMoves(from, board).Any(move =>
        {
            Piece piece= board[move.ToPosition];
            return piece != null && piece.Type == PieceType.King;
        });
    }
}