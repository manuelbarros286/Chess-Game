using System;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ChessController;
namespace ChessInterface;

// load pieces from ChessAssets folder
public static class Images
{
    private static readonly Dictionary<PieceType, IImage> whiteSources = new()
    {
        { PieceType.Pawn, LoadImage("/Assets/ChessAssets/PawnW.png") },
        { PieceType.Bishop, LoadImage("/Assets/ChessAssets/BishopW.png") },
        { PieceType.Rook, LoadImage("/Assets/ChessAssets/RookW.png") },
        { PieceType.Knight, LoadImage("/Assets/ChessAssets/KnightW.png") },
        { PieceType.Queen, LoadImage("/Assets/ChessAssets/QueenW.png") },
        { PieceType.King, LoadImage("/Assets/ChessAssets/KingW.png") },
    };

    private static readonly Dictionary<PieceType, IImage> blackSources = new()
    {
        { PieceType.Pawn, LoadImage("/Assets/ChessAssets/PawnB.png") },
        { PieceType.Bishop, LoadImage("/Assets/ChessAssets/BishopB.png") },
        { PieceType.Rook, LoadImage("/Assets/ChessAssets/RookB.png") },
        { PieceType.Knight, LoadImage("/Assets/ChessAssets/KnightB.png") },
        { PieceType.Queen, LoadImage("/Assets/ChessAssets/QueenB.png") },
        { PieceType.King, LoadImage("/Assets/ChessAssets/KingB.png") },
    };
    private static Bitmap LoadImage(string filePath)
    {// look this up in Avalonia documentation
        var uri = new Uri($"avares://ChessInterface{filePath}");
        return new Bitmap(AssetLoader.Open(uri));
    }

    public static IImage GetImage(Player colour, PieceType type)
    {
        return colour switch
        {
            Player.White => whiteSources[type],
            Player.Black => blackSources[type],
            _ => null
        };
    }
    
    public static IImage GetImage(Piece piece)
    {
        if (piece == null)
        {
            return null;
        }
        return GetImage(piece.Colour, piece.Type);
    }
}