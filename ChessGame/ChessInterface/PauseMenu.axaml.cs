using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ChessInterface;

public partial class PauseMenu : UserControl
{
    
    public event Action<Option>? OptionSelected;
    public PauseMenu()
    {
        InitializeComponent();
    }
    
    private void Continue_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OptionSelected?.Invoke(Option.Continue);
    }
    
    private void Restart_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OptionSelected?.Invoke(Option.Restart);
    }
}