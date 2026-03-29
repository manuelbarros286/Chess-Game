namespace ChessController;

public class DoublePawn : Move
{
    public override MoveType Type => MoveType.DoublePawn;
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    private readonly Position skippedPosition;
    
    public DoublePawn(Position from, Position to)
    {
        FromPosition = from;
        ToPosition = to;
        skippedPosition = new Position((from.Row+ to.Row) / 2, from.Column);
        
    }

    public override void Execute(Board board)
    {
        Player player = board[FromPosition].Colour;
        board.SetPawnSkipPosition(player, skippedPosition);
        new NormalMove(FromPosition, ToPosition).Execute(board);
    }
}