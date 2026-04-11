namespace ChessController.Tests.Moves;

public class CastleTests
{
    [Fact]
    public void IsLegal_WhenIntermediateSquareIsAttacked_ReturnsFalse()
    {
        var board = new Board();
        Position kingPos = new Position(0, 4);
        board[kingPos] = new King(Player.White);
        board[new Position(0, 7)] = new Rook(Player.White);
        
        board[new Position(7, 5)] = new Rook(Player.Black);

        var move = new Castle(MoveType.CastleKS, kingPos);

        bool isLegal = move.IsLegal(board);
        
        Assert.False(isLegal);
    }
    
    [Fact]
    public void IsLegal_WhenKingStartsInCheck_ReturnsFalse()
    {
        var board = new Board();
        Position kingPos = new Position(0, 4);
        board[kingPos] = new King(Player.White);
        
        board[new Position(7, 4)] = new Rook(Player.Black);

        var move = new Castle(MoveType.CastleKS, kingPos);
        
        Assert.False(move.IsLegal(board));
    }
    
    [Fact]
    public void Execute_QueenSide_MovesRookToCorrectSquare()
    {
        var board = new Board();
        Position kingPos = new Position(0, 4);
        board[kingPos] = new King(Player.White);
        board[new Position(0, 0)] = new Rook(Player.White);

        var move = new Castle(MoveType.CastleQS, kingPos);

        move.Execute(board);

        // Assert Rook should move to (0, 3) for Queen Side
        Assert.IsType<Rook>(board[new Position(0, 3)]);
        Assert.Null(board[new Position(0, 0)]);
    }


}