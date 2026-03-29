using System.Collections.Generic;
using System.Linq;

namespace ChessController;

public class GameState
{
    //class UI will interact with
    
    //store board configuration
    public Board Board { get; }
    public Player CurrentPlayer { get; private set; }
    public Result Result { get; private set; } = null;

    public GameState(Player player, Board board)
    {
        CurrentPlayer = player;
        Board = board;
    }

    public IEnumerable<Move> LegalMovesForPiece(Position pos)
    {
        if (Board.IsEmpty(pos) || Board[pos].Colour != CurrentPlayer)
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
        Board.SetPawnSkipPosition(CurrentPlayer, null);
        move.Execute(Board);
        CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
        CheckForGameEnd();
    }
    
    public IEnumerable<Move> AllLegalMovesForPlayer(Player player)
    {
        IEnumerable<Move> moveCandidates = Board.GetAllPiecePositionsForPlayer(player).SelectMany(pos =>
        {
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        });
        return moveCandidates.Where(move => move.IsLegal(Board)); 
    }
    
    private void CheckForGameEnd()
    {
        if (!AllLegalMovesForPlayer(CurrentPlayer).Any())
        {
            if (Board.IsInCheck(CurrentPlayer))
            {
                Result = Result.Win(CurrentPlayer.Opponent());
            }
            else
            {
                Result = Result.Draw(GameEndReason.Stalemate);
            }
        } else if (Board.InsufficientMaterial())
        {
            Result = Result.Draw(GameEndReason.InsufficientMaterial);
        }
    }
    
    public bool IsGameOver()
    {
        return Result != null;
    }
}