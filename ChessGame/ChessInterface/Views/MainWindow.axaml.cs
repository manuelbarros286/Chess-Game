using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using ChessController;
namespace ChessInterface.Views;

public partial class MainWindow : Window
{
    
    private readonly Image[,] pieceImages = new Image[8,8];
    private readonly Rectangle[,] highlights = new Rectangle[8,8]; 
    private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
    

    private GameState gameState;
    private Position selectedPosition = null;
    public MainWindow()
    {
        InitializeComponent();
        InitialiseBoard();

        gameState = new GameState(Player.White, Board.Initial());
        DrawBoard(gameState.Board);
        SetCursor(gameState.CurrentPlayer);
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

                Rectangle highlight = new Rectangle();
                highlights[r, c] = highlight;
                HighlightGrid.Children.Add(highlight);

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

    private Position ToSquarePosition(Point point)
    {
        double squareSize= BoardGrid.Bounds.Width / 8;
        int row = (int)(point.Y / squareSize);
        int column = (int)(point.X / squareSize);
        return new Position(row, column);
    }
    private void BoardGrid_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (IsMenuOnScreen())
        {
            return;
        }
        
        Point point = e.GetPosition(BoardGrid);
        Position position = ToSquarePosition(point);
        if (selectedPosition == null)
        {
            OnFromPositionSelected(position);
        }
        else
        {
            OnToPositionSelected(position);
        }
    }

    private void OnFromPositionSelected(Position position)
    {
        IEnumerable<Move> moves = gameState.LegalMovesForPiece(position);
        if (moves.Any())
        {
            selectedPosition = position;
            CacheMoves(moves);
            ShowHighlights();
        }
    }
    
    private void OnToPositionSelected(Position position)
    {
        selectedPosition = null;
        HideHighlights();
        if (moveCache.TryGetValue(position, out Move move))
        {
            if (move.Type == MoveType.PawnPromotion)
            {
                HandlePromotion(move.FromPosition, move.ToPosition);
            }
            else
            {
                HandleMove(move);
            }
        }
    }
    
    private void HandlePromotion(Position from, Position to)
    {
        pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
        pieceImages[from.Row, from.Column].Source = null;
        
        PromotionMenu promotionMenu = new PromotionMenu(gameState.CurrentPlayer);
        MenuContainer.Content = promotionMenu;
        
        promotionMenu.PieceSelected += type =>
        {
            MenuContainer.Content = null;
            Move promotionMove = new PawnPromotion(from, to, type);
            HandleMove(promotionMove);
        };
    }
    
    private void HandleMove(Move move)
    {
        gameState.MakeMove(move);
        DrawBoard(gameState.Board);
        SetCursor(gameState.CurrentPlayer);
        
        if(gameState.IsGameOver())
        {
            ShowGameOver();
        }
    }
    
    private void CacheMoves(IEnumerable<Move> moves)
    {
        moveCache.Clear();
        foreach (Move move in moves)
        {
            moveCache[move.ToPosition] = move;
        }
    }
    
    private void ShowHighlights()
    {
        Color colour = Color.FromArgb(150, 125, 255, 125);
        foreach (Position to in moveCache.Keys)
        {
            highlights[to.Row, to.Column].Fill = new SolidColorBrush(colour);
        }
    }
    
    private void HideHighlights()
    {
        foreach (Position to in moveCache.Keys)
        {
            highlights[to.Row, to.Column].Fill = Brushes.Transparent;
        }
    }

    private void SetCursor(Player player)
    {
        if(player == Player.White)
        {
            Cursor = MouseCursors.WhiteCursor;
        }
        else
        {
            Cursor = MouseCursors.BlackCursor;
        }
    }

    private bool IsMenuOnScreen()
    {
        return MenuContainer.Content != null;
    }
    
    private void ShowGameOver()
    {
        GameOverMenu gameOverMenu = new GameOverMenu(gameState);
        MenuContainer.Content = gameOverMenu;

        gameOverMenu.OptionSelected += option =>
        {
            if (option == Option.Restart)
            {
                MenuContainer.Content = null;
                RestartGame();
            }
            else
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            }
        };
    }
    
    private void RestartGame()
    {
        selectedPosition = null;
        HideHighlights();
        moveCache.Clear();
        gameState = new GameState(Player.White, Board.Initial());
        DrawBoard(gameState.Board);
        SetCursor(gameState.CurrentPlayer);
    }
    
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && !IsMenuOnScreen())
        {
            ShowPauseMenu();
        }
    }
    
    private void ShowPauseMenu()
    {
        PauseMenu pauseMenu = new PauseMenu();
        MenuContainer.Content = pauseMenu;

        pauseMenu.OptionSelected += option =>
        {
            MenuContainer.Content = null; 
            if (option == Option.Restart)
            {
                RestartGame();
            }
        };
    }
    
    
}