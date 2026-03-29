using System.Collections.Generic;
using System.Linq;

namespace ChessController;

public class Board
{
    //array to hold the pieces on the board
    private readonly Piece[,] pieces= new Piece[8, 8];

    private readonly Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>
    {
        { Player.White, null },
        { Player.Black, null }
    };
    
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
    
    public Position GetPawnSkipPosition(Player player)
    {
        return pawnSkipPositions[player];
    }
    
    public void SetPawnSkipPosition(Player player, Position position)
    {
        pawnSkipPositions[player] = position;
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
    
    public static bool IsInside(Position position)
    {
        return position.Row >= 0 && position.Row <8 && position.Column >= 0 && position.Column < 8; 
    }
    
    public bool IsEmpty(Position position)
    {
        return this[position] == null;
    }
    
    public IEnumerable<Position> GetAllPiecePositions()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                
                Position position = new Position(row, col);
                if (!IsEmpty(position))
                {
                    yield return position;
                }
            }
        }
    }
    
    public IEnumerable<Position> GetAllPiecePositionsForPlayer(Player player)
    {
        return GetAllPiecePositions().Where(pos => this[pos].Colour == player);
    }
    
    public bool IsInCheck(Player player)
    {
        //find if any of the opponent's pieces can capture the player's king
        return GetAllPiecePositionsForPlayer(player.Opponent()).Any(pos =>
        {
            Piece piece = this[pos];
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }

    public Board Copy()
    {
        Board copy = new Board();
        foreach(Position pos in GetAllPiecePositions())
        {
            copy[pos] = this[pos].Copy();
        }
        return copy;
    }

    public Counting CountPieces()
    {
        Counting counting = new Counting();
        foreach (Position pos in GetAllPiecePositions())
        {
            Piece piece = this[pos];
            counting.Increment(piece.Colour, piece.Type);
        }

        return counting;
    }

    public bool InsufficientMaterial()
    {
        Counting counting = CountPieces();
        return isKingVKing(counting) || IsKingBishopVKing(counting) || IsKingKnightVKing(counting) || IsKingBishopVKingBishop(counting);
    }
    //king vs king scenario
    private static bool isKingVKing(Counting counting)
    {
        return counting.TotalCount == 2;
    }
    
    //king vs king and bishop/knight scenario
    private static bool IsKingBishopVKing(Counting counting)
    {
        return counting.TotalCount == 3 && (counting.White(PieceType.Bishop) == 1 || counting.Black(PieceType.Bishop) == 1);
    }
    
    private static bool IsKingKnightVKing(Counting counting)
    {
        return counting.TotalCount == 3 && (counting.White(PieceType.Knight) == 1 || counting.Black(PieceType.Knight) == 1);
    }

    private bool IsKingBishopVKingBishop(Counting counting)
    {
        if(counting.TotalCount != 4) return false;
        if (counting.White(PieceType.Bishop) != 1 || counting.Black(PieceType.Bishop) != 1) return false;
        
        //check if bishops are on the same colour square, if they are then it's a draw, if not then not a draw
        Position wBishop = FindPiece(Player.White, PieceType.Bishop);
        Position bBishop = FindPiece(Player.Black, PieceType.Bishop);
        
        return wBishop.SquareColour() == bBishop.SquareColour();
    }

    private Position FindPiece(Player colour, PieceType type)
    {
        return GetAllPiecePositionsForPlayer(colour).First(pos => this[pos].Type == type);
    }

}