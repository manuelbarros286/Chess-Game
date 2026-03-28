using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ChessController;

namespace ChessInterface;

public partial class PromotionMenu : UserControl
{
    public event Action<PieceType> PieceSelected;
    
    public PromotionMenu(Player player)
    {
        InitializeComponent();
        QueenImage.Source = Images.GetImage(player, PieceType.Queen);
        RookImage.Source = Images.GetImage(player, PieceType.Rook);
        BishopImage.Source = Images.GetImage(player, PieceType.Bishop);
        KnightImage.Source = Images.GetImage(player, PieceType.Knight);
    }
    
    private void QueenImage_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Queen);
    }
    
    private void RookImage_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Rook);
    }
    
    private void BishopImage_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Bishop);
    }
    
    private void KnightImage_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Knight);
    }
    
}