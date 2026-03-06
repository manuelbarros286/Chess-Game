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
        return piece.GetMoves(pos, Board);
    }
    
    public void MakeMove(Move move)
    {
        move.Execute(Board);
        CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
    }
}