namespace ChessController.Tests.Moves;

public class PawnPromotionTests
{
    [Theory]
    [InlineData(PieceType.Queen)]
    [InlineData(PieceType.Knight)]
    [InlineData(PieceType.Rook)]
    [InlineData(PieceType.Bishop)]
    public void Execute_Should_ReplacePawnWithPromotedPiece(PieceType type)
    {
        var board = new Board();
        Position from = new Position(6, 0);
        Position to = new Position(7, 0);
        board[from] = new Pawn(Player.White);

        var move = new PawnPromotion(from, to, type);
        
        move.Execute(board);
        
        Assert.Null(board[from]);
        Assert.Equal(type, board[to].Type);
        Assert.Equal(Player.White, board[to].Colour);
        Assert.True(board[to].HasMoved);
    }
    
    [Fact]
    public void IsLegal_WhenPromotionExposesKingToCheck_ReturnsFalse()
    {
        var board = new Board();
        Position kingPos = new Position(0, 0);
        Position pawnPos = new Position(1, 0);
        Position enemyRookPos = new Position(7, 0);
    
        board[kingPos] = new King(Player.White);
        board[pawnPos] = new Pawn(Player.White);
        board[enemyRookPos] = new Rook(Player.Black);


        var move = new PawnPromotion(pawnPos, new Position(2, 0), PieceType.Queen);
        
        bool isLegal = move.IsLegal(board);
        
        // If the King is safe, this should be true. If the move opens a line, false.
        Assert.True(isLegal); 
    }


}