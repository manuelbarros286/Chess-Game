namespace ChessController;

public abstract class Move
{
    public abstract MoveType Type { get; }
    public abstract Position FromPosition { get; } 
    public abstract Position ToPosition { get; }
    
    public abstract void Execute(Board board);

    public virtual bool IsLegal(Board board)
    {
        Player currentPlayer = board[FromPosition].Colour;
        Board boardCopy = board.Copy();
        Execute(boardCopy);
        return !boardCopy.IsInCheck(currentPlayer);
    }
}