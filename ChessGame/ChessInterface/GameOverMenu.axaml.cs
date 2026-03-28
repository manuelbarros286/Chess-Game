using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ChessController;

namespace ChessInterface;

public partial class GameOverMenu : UserControl
{

    public event Action<Option>? OptionSelected;

    public GameOverMenu()
    {
        InitializeComponent();
        
    }
    public GameOverMenu(GameState gameState) : this()
    {
        Result result = gameState.Result;
        WinnerText.Text = GetWinnerText(result.Winner);
        ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentPlayer);
    }
    
    private static String GetWinnerText(Player winner)
    {
        return winner switch
        {
            Player.White => "White wins!",
            Player.Black => "Black wins!",
            _ => "It's a draw!"
        };
    }

    private static String PlayerString(Player player)
    {
        return player switch
        {
            Player.White => "White",
            Player.Black => "Black",
            _ => ""
        };
    }

    private static String GetReasonText(GameEndReason reason, Player currentPlayer)
    {
        return reason switch
        {
            GameEndReason.Stalemate => $"Stalemate - {PlayerString(currentPlayer)} has no legal moves",
            GameEndReason.Checkmate => $"Checkmate {PlayerString(currentPlayer)} is in check and has no legal moves",
            GameEndReason.FiftyMoveRule => "Fifty-move rule - no pawn moves or captures in the last 50 moves",
            GameEndReason.ThreefoldRepetition => "Threefold repetition - the same board position has occurred three times",
            _ => ""
        };
    }

    private void Restart_Click(object? sender, RoutedEventArgs e)
    {
        OptionSelected?.Invoke(Option.Restart);
    }

    private void Quit_Click(object? sender, RoutedEventArgs e)
    {
        OptionSelected?.Invoke(Option.Quit);
    }
}