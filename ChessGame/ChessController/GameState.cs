namespace ChessController;

public class GameState
{
    //class UI will interact with
    
    //store board configuration
    public Board Board { get; }
    public Player CurrentPlayer { get; private set; }

    public GameState(Player player, Board board)
    {
        CurrentPlayer = player;
        Board = board;
    }

    public IEnumerable<Move> LegalMovesForPiece(Position pos)
    {
        if (Board.isEmpty(pos) || Board[pos].Colour != CurrentPlayer)
        {
            return Enumerable.Empty<Move>();
        }
        
        Piece piece = Board[pos];
        //passing piece position to GetMoves to determine legal moves for that piece
        IEnumerable<Move> moveCandidates= piece.GetMoves(pos, Board);
        //only return moves that are legal (won't put own king in check)
        return moveCandidates.Where(move => move.IsLegal(Board));
    }
    
    public void MakeMove(Move move)
    {
        move.Execute(Board);
        CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
    }
}