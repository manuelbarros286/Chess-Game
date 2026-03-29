namespace ChessController;

public class EnPassant : Move
{
    public override MoveType Type => MoveType.EnPassant;
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    private readonly Position capturedPawnPosition;
    
    public EnPassant(Position from, Position to)
    {
        FromPosition = from;
        ToPosition = to;
        capturedPawnPosition = new Position(from.Row, to.Column);
    }
    
    public override void Execute(Board board)
    {
        new NormalMove(FromPosition, ToPosition).Execute(board);
        board[capturedPawnPosition] = null;
    }
}