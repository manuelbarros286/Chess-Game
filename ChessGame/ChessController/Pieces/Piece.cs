namespace ChessController;
// abstract since it a base class for all pieces
public abstract class Piece
{
    public abstract PieceType Type { get; }
    public abstract Player Colour { get; }
    public bool HasMoved { get; set; } = false;
    public abstract Piece Copy();
    
    public abstract IEnumerable<Move> GetMoves(Position from, Board board);

    protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)
    {
        for (Position pos = from + dir; Board.isInside(pos); pos += dir)
        {
            if (board.isEmpty(pos))
            {
                yield return pos;
                continue;
            }
            
            Piece piece = board[pos];
            if (piece.Colour != Colour)
            {
                yield return pos;
            }

            yield break;
        }
        
    }
    //helper for getting move positions in multiple directions
    protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, IEnumerable<Direction> dirs)
    {
        return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
    }

    public virtual bool CanCaptureOpponentKing(Position from, Board board)
    {
        return GetMoves(from, board).Any(move =>
        {
            Piece piece = board[move.ToPosition];
            return piece != null && piece.Type == PieceType.King;
        });    
    }
}