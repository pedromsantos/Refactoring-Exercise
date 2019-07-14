using SmellyChess.Pieces;

namespace SmellyChess
{
    public class ChessBoard
    {
        private readonly Cell[,] _board;
        public bool KingDead { get; set; }

        public ChessBoard()
        {
            _board = new Cell[8,8];
            InitBoard();
        }

        public Cell[,] Board => _board;

        private void InitBoard()
        {
            for (var row = 0; row < 8; row++) 
            {
                for (var column = 0; column < 8; column++) 
                {
                    var color = ((row + column) % 2 == 0) ? Color.WHITE : Color.BLACK;
                    _board[row, column] = new Cell(color);
                }
            }
        }

        private bool IsPositionOutOfBounds(Position position) {
                return (position.Row < 0 || position.Row >= 8 || position.Column < 0
                        || position.Column >= 8);
            }
        
        public bool IsEmpty(Position position) {
            return IsPositionOutOfBounds(position) || CellAt(position).IsEmpty();
        }
        
        private Cell CellAt(Position position) {
            return _board[position.Row, position.Column];
        }
        
        public Piece PieceAt(Position position) {
            return (IsPositionOutOfBounds(position) || CellAt(position).IsEmpty()) ? null : CellAt(position).Piece;
        }
        
        public bool IsKingDead() {
            return KingDead;
        }
            
        public bool IsValidMove(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            Position from = new Position(fromRow, fromColumn);
            Position to = new Position(toRow, toColumn);
            
            return !from.Equals(to)
                   && !(IsPositionOutOfBounds(from) || IsPositionOutOfBounds(to))
                   && !IsEmpty(from)
                   && (IsEmpty(to) || PieceAt(from).Color != PieceAt(to).Color)
                   && PieceAt(from).IsValidMove(from, to)
                   && HasNoPieceInPath(from, to)
                   && (!(PieceAt(from) is Pawn) || IsValidPawnMove(from, to));
        }
        
        public void MovePiece(int fromRow, int fromColumn, int toRow, int toColumn) {
            var from = new Position(fromRow, fromColumn);
            var to = new Position(toRow, toColumn);
            
            UpdateIsKingDead(toRow, toColumn);

            if (!CellAt(to).IsEmpty())
            {
                CellAt(to).RemovePiece();
            }
            
            CellAt(to).SetPiece(PieceAt(from));
            CellAt(from).RemovePiece();
        }

        private void UpdateIsKingDead(int row, int column) 
        {
            if (PieceAt(new Position(row, column)) is King) {
                KingDead = true;
            }
        }
        
        private bool IsValidPawnMove(Position @from, Position to)
        {
            return false;
        }

        private bool HasNoPieceInPath(Position @from, Position to)
        {
            if (PieceAt(from) is Knight)
            {
                return true;
            }

            if (!IsStraightLineMove(from, to))
            {
                return false;
            }
            
            var direction = new Direction(CappedCompare(to.Row, from.Row),
                CappedCompare(to.Column, from.Column));
            from = TranslatedPosition(from, direction);
            
            while (!from.Equals(to)) 
            {
                if (!IsEmpty(from))
                {
                    return false;
                }
                
                from = TranslatedPosition(from, direction);
            }
            
            return true;
        }

        private int CappedCompare(int toRow, int fromRow)
        {
            throw new System.NotImplementedException();
        }

        private Position TranslatedPosition(Position @from, Direction direction)
        {
            throw new System.NotImplementedException();
        }

        private bool IsStraightLineMove(Position @from, Position to)
        {
            throw new System.NotImplementedException();
        }
    }
}