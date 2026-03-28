using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ChessInterface;

public static class MouseCursors
{   
    
    public static readonly  Cursor WhiteCursor= LoadCursor("CursorW.cur");
    public static readonly  Cursor BlackCursor= LoadCursor("CursorB.cur");
    // Use a PNG for custom cursors in Avalonia
    private static Cursor LoadCursor(string fileName, int size=24)
    {
        var uri = new Uri($"avares://ChessInterface/Assets/ChessAssets/{fileName}");
        
        // Open the asset stream and create a Bitmap
        using var stream = AssetLoader.Open(uri);
        var bitmap = Bitmap.DecodeToHeight(stream, size, BitmapInterpolationMode.HighQuality);

        return new Cursor(bitmap, new PixelPoint(size/2, size/2));
    }
}