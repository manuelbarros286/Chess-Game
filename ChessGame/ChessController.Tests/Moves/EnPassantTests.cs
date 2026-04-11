namespace ChessController.Tests.Moves;

public class EnPassantTests
{
        [Fact]    
        public void EnPassantMove_IsLegal_ReturnsTrue()
        {
            // Arrange
            Board board = new Board();
            Position whitePawnPosition = new Position(4, 1);
            Position blackPawnPosition = new Position(5, 3);
            board[whitePawnPosition] = new Pawn(Player.White);
            board[blackPawnPosition] = new Pawn(Player.Black);
            
            EnPassant enPassantMove = new EnPassant(whitePawnPosition, new Position(5, 2));
    
            // Act
            bool isLegal = enPassantMove.IsLegal(board);
    
            // Assert
            Assert.True(isLegal);
        }
        
        [Fact]    
        public void IsLegal_WhenEnPassantExposesKing_ReturnsFalse()
        {

            Board board = new Board();
    
            // Will move
            Position kingPos = new Position(4, 0); 
            Position whitePawnPos = new Position(4, 3);
            board[kingPos] = new King(Player.White);
            board[whitePawnPos] = new Pawn(Player.White);

            // Piece to be captured
            Position blackPawnPos = new Position(4, 2);
            board[blackPawnPos] = new Pawn(Player.Black);

            // Black Rook that is currently "blocked" by both pawns
            Position enemyRookPos = new Position(4, 7);
            board[enemyRookPos] = new Rook(Player.Black);
            
            EnPassant move = new EnPassant(whitePawnPos, new Position(5, 2));
            
            bool isLegal = move.IsLegal(board);

            // Assert that the Rook has a clear line to the King and is an illegal move
            Assert.False(isLegal); 
        }

}