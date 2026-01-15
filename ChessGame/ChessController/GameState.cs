namespace ChessController;

public class GameState
{
    //class UI will interact with
    
    //store board configuration
    public Board Board { get; }
    public Player CurrentPlayer { get; }

    public GameState(Player player, Board board)
    {
        CurrentPlayer = player;
        Board = board;
    }
}