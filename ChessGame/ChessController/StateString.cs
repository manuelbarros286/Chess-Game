using System.Text;

namespace ChessController;

public class StateString
{
    private readonly StringBuilder sb = new StringBuilder();

    public StateString(Player currentPlayer, Board board)
    {
        AddPiecePlacementData(board);
        sb.Append(' ');
        
        AddCurrentPlayer(currentPlayer);
        sb.Append(' ');
        
        AddCastlingRights(board);
        sb.Append(' ');
        
        AddEnPassant(board, currentPlayer);
    }

    public override string ToString()
    {
        return sb.ToString();
    }

    private static char PieceChar(Piece piece)
    {
        char c = piece.Type switch
        {
            PieceType.Pawn => 'P',
            PieceType.Knight => 'N',
            PieceType.Bishop => 'B',
            PieceType.Rook => 'R',
            PieceType.Queen => 'Q',
            PieceType.King => 'K',
            _ => throw new ArgumentException("Invalid piece type")
        };
        
        if (piece.Colour == Player.Black)
        {
            c = char.ToLower(c);
        }
        
        return c;
    }

    private void AddRowData(Board board, int row)
    {
        int empty = 0;
        for (int c = 0; c < 8; c++)
        {
            if (board[row, c] == null)
            {
                empty++;
                continue;
            }
            if(empty > 0)
            {
                sb.Append(empty);
                empty = 0;
            }
            
            sb.Append(PieceChar(board[row, c]));
        }

        if (empty > 0)
        {
            sb.Append(empty);
        }
    }

    private void AddPiecePlacementData(Board board)
    {
        for (int r = 0; r < 8; r++)
        {
            if (r != 0)
            {
                sb.Append('/');
            }

            AddRowData(board, r);
        }
    }

    private void AddCurrentPlayer(Player currentPlayer)
    {
        if (currentPlayer == Player.White)
        {
            sb.Append('w');
        }
        else
        {
            sb.Append('b');
        }
    }
    
    private void AddCastlingRights(Board board)
    {
        bool CastleWKS = board.CastleRightKS(Player.White);
        bool CastleWQS = board.CastleRightQS(Player.White);
        bool CastleBKS = board.CastleRightKS(Player.Black);
        bool CastleBQS = board.CastleRightQS(Player.Black);

        if (!(CastleWKS || CastleWQS || CastleBKS || CastleBQS))
        {
            sb.Append('-');
            return;
        }

        if (CastleWKS)
        {
            sb.Append('K');
        }
        if(CastleWQS)
        {
            sb.Append('Q');
        }
        if(CastleBKS)
        {
            sb.Append('k');
        }
        if(CastleBQS)
        {
            sb.Append('q');
        }
    }

    private void AddEnPassant(Board board, Player currentPlayer)
    {
        if(!board.CanCaptureEnPassant(currentPlayer))
        {
            sb.Append('-');
            return;
        }
        
        Position position = board.GetPawnSkipPosition(currentPlayer.Opponent());
        char file = (char)('a' + position.Column);
        int rank = 8- position.Row;
        sb.Append(file);
        sb.Append(rank);
    }
}