using System;
using System.Diagnostics;
using SmellyChess.Pieces;

namespace SmellyChess
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            Board = new Cell[8, 8];
            InitBoard();
        }

        public bool KingDead { get; set; }

        public Cell[,] Board { get; }

        private void InitBoard()
        {
            for (var row = 0; row < 8; row++)
            for (var column = 0; column < 8; column++)
            {
                var color = (row + column) % 2 == 0 ? Color.WHITE : Color.BLACK;
                Board[row, column] = new Cell(color);
            }
        }

        private bool IsPositionOutOfBounds(Position position)
        {
            return position.Row < 0 || position.Row >= 8 || position.Column < 0
                   || position.Column >= 8;
        }

        public bool IsEmpty(Position position)
        {
            return IsPositionOutOfBounds(position) || CellAt(position).IsEmpty();
        }

        private Cell CellAt(Position position)
        {
            return Board[position.Row, position.Column];
        }

        public Piece PieceAt(Position position)
        {
            return IsPositionOutOfBounds(position) || CellAt(position).IsEmpty() ? null : CellAt(position).Piece;
        }

        public bool IsKingDead()
        {
            return KingDead;
        }

        public bool IsValidMove(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            var from = new Position(fromRow, fromColumn);
            var to = new Position(toRow, toColumn);

            var isMoveOutOfBounds = !(IsPositionOutOfBounds(from) || IsPositionOutOfBounds(to));
            var canMoveTo = IsEmpty(to) || PieceAt(from).Color != PieceAt(to).Color;
            var isValidMove = PieceAt(from).IsValidMove(from, to);
            var hasNoPieceInPath = HasNoPieceInPath(from, to);
            var isPawn = !(PieceAt(from) is Pawn);
            var isValidPawnMove = isPawn || IsValidPawnMove(from, to);

            return !from.Equals(to)
                   && isMoveOutOfBounds
                   && !IsEmpty(from)
                   && canMoveTo
                   && isValidMove
                   && hasNoPieceInPath
                   && isValidPawnMove;
        }

        public void MovePiece(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            var from = new Position(fromRow, fromColumn);
            var to = new Position(toRow, toColumn);

            UpdateIsKingDead(toRow, toColumn);

            if (!CellAt(to).IsEmpty()) CellAt(to).RemovePiece();

            CellAt(to).Piece = PieceAt(from);
            CellAt(from).RemovePiece();
        }

        private void UpdateIsKingDead(int row, int column)
        {
            if (PieceAt(new Position(row, column)) is King) KingDead = true;
        }

        private bool IsValidPawnMove(Position from, Position to)
        {
            Debug.Assert(PieceAt(from) is Pawn);

            var pawn = (Pawn) PieceAt(from);
            var pawnColor = pawn.Color;
            var forwardRow = from.Row + (pawnColor == Color.BLACK ? 1 : -1);
            var forwardLeft = new Position(forwardRow, from.Column + (pawnColor == Color.WHITE ? -1 : 1));
            var forwardRight = new Position(forwardRow, from.Column + (pawnColor == Color.WHITE ? 1 : -1));

            var opponentPieceAtForwardLeft = !IsEmpty(forwardLeft) && PieceAt(forwardLeft).Color != pawnColor;
            var opponentPieceAtForwardRight = !IsEmpty(forwardRight) && PieceAt(forwardRight).Color != pawnColor;
            var atInitialPosition = from.Row == (pawnColor == Color.BLACK ? 1 : 6);

            return pawn.IsValidMoveGivenContext(from, to, atInitialPosition, opponentPieceAtForwardLeft,
                opponentPieceAtForwardRight);
        }

        private bool HasNoPieceInPath(Position from, Position to)
        {
            if (PieceAt(from) is Knight) return true;

            if (!IsStraightLineMove(from, to)) return false;

            var direction = new Direction(CappedCompare(to.Row, from.Row),
                CappedCompare(to.Column, from.Column));
            from = TranslatedPosition(from, direction);

            while (!from.Equals(to))
            {
                if (!IsEmpty(from)) return false;

                from = TranslatedPosition(from, direction);
            }

            return true;
        }

        private int CappedCompare(int x, int y)
        {
            return Math.Max(-1, Math.Min(1, x.CompareTo(y)));
        }

        private Position TranslatedPosition(Position from, Direction direction)
        {
            return new Position(from.Row + direction.RowOffset, from.Column + direction.ColumnOffset);
        }

        private bool IsStraightLineMove(Position from, Position to)
        {
            return Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column)
                   || from.Row == to.Row || from.Column == to.Column;
        }
    }
}