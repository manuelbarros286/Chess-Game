using Avalonia.Controls;
using ChessController;
namespace ChessInterface.Views;

public partial class MainWindow : Window
{
    
    private readonly Image[,] pieceImages = new Image[8,8];

    private GameState gameState;

    public MainWindow()
    {
        InitializeComponent();
        InitialiseBoard();

        gameState = new GameState(Player.White, Board.Initial());
    }
    
    private void InitialiseBoard()
    {   //iterate through rows and columns and create Image squares
        for (int r=0; r<8; r++)
        {
            for (int c = 0; c < 8; c++)
            { 
                Image image = new Image();
                pieceImages[r, c] = image;
                PieceGrid.Children.Add(image);

            }
        }
    }

    private void DrawBoard(Board board)
    {
        for(int r=0; r<8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Piece piece = board[r, c];
                pieceImages[r, c].Source = Images.GetImage(piece);
            }
        }
    }
}