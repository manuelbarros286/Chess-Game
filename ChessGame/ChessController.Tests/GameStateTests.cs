namespace ChessController.Tests;

public class GameStateTests
{
    [Fact]
    public void MakeMove_WhenCheckmateIsDelivered_EndsGameWithCorrectWinner()
    {

        var board = new Board();
        board[new Position(7, 7)] = new King(Player.Black); 
        board[new Position(6, 6)] = new Queen(Player.White); 
        board[new Position(5, 6)] = new Rook(Player.White); 

        var gameState = new GameState(Player.White, board);
        var finalMove = new NormalMove(new Position(6, 6), new Position(7, 6)); 

        gameState.MakeMove(finalMove);

        // Assert
        Assert.True(gameState.IsGameOver());
        Assert.Equal(Player.White, gameState.Result.Winner);
    }
    
    [Fact]
    public void CheckForGameEnd_WhenStalemate_SetsResultToDraw()
    {
        var board = new Board();

        board[new Position(7, 7)] = new King(Player.Black);
        board[new Position(5, 4)] = new Queen(Player.White); 
        board[new Position(3, 3)] = new King(Player.White);
        board[new Position(0, 0)] = new Pawn(Player.White);
        
        var gameState = new GameState(Player.White, board);
        var move = new NormalMove(new Position(5, 4), new Position(6, 5)); 
        gameState.MakeMove(move); 
        
        Assert.True(gameState.IsGameOver());
        Assert.Equal(GameEndReason.Stalemate, gameState.Result.Reason);
    }
    
    [Fact]
    public void MakeMove_WhenSamePositionOccursThrice_DeclaresDraw()
    {
        var board = new Board();
        board[new Position(0, 0)] = new King(Player.White);
        board[new Position(7, 7)] = new King(Player.Black);
        var gameState = new GameState(Player.White, board);
        
        var moveRight = new NormalMove(new Position(0, 0), new Position(0, 1));
        var moveLeft = new NormalMove(new Position(0, 1), new Position(0, 0));
        
        gameState.MakeMove(moveRight); 
        // Dummy move for black
        gameState.MakeMove(new NormalMove(new Position(7, 7), new Position(7, 6))); 
        gameState.MakeMove(moveLeft); 
    }

    [Fact]
    public void MakeMove_WhenThreefoldRepetitionOccurs_SetsResultToDraw()
    {
        var board = new Board();

        board[new Position(0, 0)] = new King(Player.White);
        board[new Position(7, 7)] = new King(Player.Black);
        board[new Position(1, 1)] = new Pawn(Player.White);
        board[new Position(6, 6)] = new Pawn(Player.Black);
        var state = new GameState(Player.White, board);

        // Positions to toggle between
        var whiteMove1 = new NormalMove(new Position(0, 0), new Position(0, 1));
        var whiteMove2 = new NormalMove(new Position(0, 1), new Position(0, 0));
        var blackMove1 = new NormalMove(new Position(7, 7), new Position(7, 6));
        var blackMove2 = new NormalMove(new Position(7, 6), new Position(7, 7));
        
        state.MakeMove(whiteMove1); state.MakeMove(blackMove1);
        state.MakeMove(whiteMove2); state.MakeMove(blackMove2);

        state.MakeMove(whiteMove1); state.MakeMove(blackMove1);
        state.MakeMove(whiteMove2); state.MakeMove(blackMove2);
        
        Assert.True(state.IsGameOver());
        Assert.Equal(GameEndReason.ThreefoldRepetition, state.Result.Reason);
    }


}