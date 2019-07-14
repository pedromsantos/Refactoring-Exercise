using System.Diagnostics;
using RefactoredChess.Pieces;

namespace RefactoredChess
{
    public class ChessBoard
    {
        private bool _kingDead;

        private Cell[,] Board { get; }

        public ChessBoard()
        {
            Board = new Cell[8, 8];
            InitBoard(); 
        }

        public void Reset()
        {
            for (var column = 0; column < 8; column++)
            {
                Board[7, column].Piece = Piece.CreatePieceFor(column, Color.White);
                Board[6, column].Piece = new Pawn(Color.White);
                
                Board[0, column].Piece = Piece.CreatePieceFor(column, Color.Black);
                Board[1, column].Piece = new Pawn(Color.Black);
            }

            _kingDead = false;
        }

        public Piece PieceAt(Position position)
        {
            return IsPositionOutOfBounds(position) || CellAt(position).IsEmpty() ? null : CellAt(position).Piece;
        }

        public void MovePiece(Move move)
        {
            UpdateIsKingDead(move.To);

            if (!CellAt(move.To).IsEmpty())
            {
                CellAt(move.To).RemovePiece();
            }

            CellAt(move.To).Piece = PieceAt(move.From);
            CellAt(move.From).RemovePiece();
        }

        public bool IsKingDead()
        {
            return _kingDead;
        }

        public bool IsValidMove(Move move, Player player)
        {
            return player.IsMovingItsOwnPiece(PieceAt(move.From)) && IsValidMove(move);
        }

        private void InitBoard()
        {
            for (var row = 0; row < 8; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    Board[row, column] = new Cell();
                }
            }
        }

        private bool IsPositionOutOfBounds(Position position)
        {
            return position.Row < 0 || position.Row >= 8 || position.Column < 0
                   || position.Column >= 8;
        }

        private bool IsEmpty(Position position)
        {
            return IsPositionOutOfBounds(position) || CellAt(position).IsEmpty();
        }

        private Cell CellAt(Position position)
        {
            return Board[position.Row, position.Column];
        }

        private bool IsValidMove(Move move)
        {
            var isMoveOutOfBounds = !(IsPositionOutOfBounds(move.From) || IsPositionOutOfBounds(move.To));
            var canMoveTo = IsEmpty(move.To) || PieceAt(move.From).Color != PieceAt(move.To).Color;
            var isValidMove = PieceAt(move.From).IsValidMove(move.From, move.To);
            var hasNoPieceInPath = HasNoPieceInPath(move);
            var isValidPawnMove = !(PieceAt(move.From) is Pawn) || IsValidPawnMove(move);

            return !move.From.Equals(move.To)
                   && isMoveOutOfBounds
                   && !IsEmpty(move.From)
                   && canMoveTo
                   && isValidMove
                   && hasNoPieceInPath
                   && isValidPawnMove;
        }

        private void UpdateIsKingDead(Position position)
        {
            if (PieceAt(position) is King) _kingDead = true;
        }

        private bool IsValidPawnMove(Move move)
        {
            Debug.Assert(PieceAt(move.From) is Pawn);

            var pawn = (Pawn) PieceAt(move.From);

            var forwardLeft = move.ForwardLeftPosition(pawn.Color);
            var forwardRight = move.ForwardRightPosition(pawn.Color);

            var opponentPieceAtForwardLeft = !IsEmpty(forwardLeft) && PieceAt(forwardLeft).Color != pawn.Color;
            var opponentPieceAtForwardRight = !IsEmpty(forwardRight) && PieceAt(forwardRight).Color != pawn.Color;

            return pawn.IsValidMove(move.From, move.To, opponentPieceAtForwardLeft, opponentPieceAtForwardRight);
        }

        private bool HasNoPieceInPath(Move move)
        {
            if (PieceAt(move.From) is Knight) return true;

            if (!move.IsStraightLineMove()) return false;

            var from = move.Translate();

            while (!from.Equals(move.To))
            {
                if (!IsEmpty(from)) return false;

                from = move.Translate();
            }

            return true;
        }
    }
}