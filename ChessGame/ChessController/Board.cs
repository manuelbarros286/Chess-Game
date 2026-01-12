namespace ChessController;

public class Board
{
    //array to hold the pieces on the board
    private readonly Piece[,] pieces= new Piece[8, 8];
    //provide access through an indexer
    public Piece this[int row, int col]
    {
        get { return pieces[row, col];  }
        set { pieces[row, col] = value; }
    }
    //ability to access pieces using Position object
    public Piece this[Position position]
    {
        get { return this[position.Row, position.Column]; }
        set { this[position.Row, position.Column] = value; }
        
    }

    public static Board Initial()
    {
        Board board = new Board();
        //add all the pieces in initial positions
        board.AddStartPieces();
        return board;
    }

    private void AddStartPieces()
    {
        //initialise each piece position with player colour (Black on top, White on bottom)
        this[0, 0] = new Rook(Player.Black);
        this[0, 1] = new Knight(Player.Black);
        this[0, 2] = new Bishop(Player.Black);
        this[0, 3] = new Queen(Player.Black);
        this[0, 4] = new King(Player.Black);
        this[0, 5] = new Bishop(Player.Black);
        this[0, 6] = new Knight(Player.Black);
        this[0, 7] = new Rook(Player.Black);
        
        for (int i = 0; i < 8; i++)
        {
            this[1, i] = new Pawn(Player.Black);
            this[6, i] = new Pawn(Player.White);
        }
        
        this[7, 0] = new Rook(Player.White);
        this[7, 1] = new Knight(Player.White);
        this[7, 2] = new Bishop(Player.White);
        this[7, 3] = new King(Player.White);
        this[7, 4] = new Queen(Player.White);
        this[7, 5] = new Bishop(Player.White);
        this[7, 6] = new Knight(Player.White);
        this[7, 7] = new Rook(Player.White);
    }
}