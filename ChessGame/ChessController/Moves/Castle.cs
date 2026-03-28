namespace ChessController;

public class Castle : Move
{
    public override MoveType Type { get; }
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    
    private readonly Direction kingDirection;
    private readonly Position rookFromPosition;
    private readonly Position rookToPosition;


    public Castle(MoveType type, Position kingPos)
    {
        Type = type;
        FromPosition = kingPos;
        if (type == MoveType.CastleKS)
        {
            kingDirection = Direction.East;
            ToPosition = new Position(kingPos.Row, 6);
            rookFromPosition = new Position(kingPos.Row, 7);
            rookToPosition = new Position(kingPos.Row, 5);
        }
        
        if (type == MoveType.CastleQS)
        {
            kingDirection = Direction.West;
            ToPosition = new Position(kingPos.Row, 2);
            rookFromPosition = new Position(kingPos.Row, 0);
            rookToPosition = new Position(kingPos.Row, 3);
        }
    }
    
    public override void Execute(Board board)
    {
        new NormalMove(FromPosition, ToPosition).Execute(board);
        new NormalMove(rookFromPosition, rookToPosition).Execute(board);

    }

    public override bool IsLegal(Board board)
    {
        Player player = board[FromPosition].Colour;
        if (board.IsInCheck(player))
        {
            return false;
        }
        Board boardCopy = board.Copy();
        Position kingPositionInCopy = FromPosition;
        
        for(int i=0; i<2; i++)
        {
            kingPositionInCopy += kingDirection;
            new NormalMove(FromPosition, kingPositionInCopy).Execute(boardCopy);
            if (boardCopy.IsInCheck(player))
            {
                return false;
            }
        }
        return true;
    }
}