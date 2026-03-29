using System;

namespace ChessController;

public class PawnPromotion : Move
{
    public override MoveType Type => MoveType.PawnPromotion;
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    
    private readonly PieceType promotionType;

    public PawnPromotion(Position from, Position to, PieceType promotionType)
    {
        FromPosition = from;
        ToPosition = to;
        this.promotionType = promotionType;
        
        
    }
    
    private Piece CreatePromotionPiece(Player colour)
    {
        return promotionType switch
        {
            PieceType.Queen => new Queen(colour),
            PieceType.Rook => new Rook(colour),
            PieceType.Bishop => new Bishop(colour),
            PieceType.Knight => new Knight(colour),
            _ => throw new ArgumentException("Invalid promotion type")
        };
    }

    public override bool Execute(Board board)
    {
        Piece pawn = board[FromPosition];
        board[FromPosition] = null;
        Piece promotionPiece = CreatePromotionPiece(pawn.Colour);
        promotionPiece.HasMoved = true;
        board[ToPosition] = promotionPiece;
        
        //for the fifty-move rule
        return true;
    }
}