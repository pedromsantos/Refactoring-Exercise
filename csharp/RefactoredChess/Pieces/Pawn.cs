using System;

namespace RefactoredChess.Pieces
{
    public class Pawn : Piece
    {
        private readonly int _initialRow;
            
        public Pawn(Color color)
            : base(color)
        {
            _initialRow = color == Color.White ? 0 : 7;
        }

        public override bool IsValidMove(Position from, Position to)
        {
            var columnsMoved = Math.Abs(to.Column - from.Column);
            var rowsMoved = Math.Abs(to.Row - from.Row);
            var isForwardMove = IsForwardMove(from, to);
            var moveOneRow = columnsMoved <= 1 && rowsMoved == 1;
            var moveTwoRows = columnsMoved == 0 && rowsMoved == 2;
            return isForwardMove
                   && (moveOneRow || moveTwoRows);
        }

        private bool IsForwardMove(Position from, Position to)
        {
            switch (Color)
            {
                case Color.White:
                    return to.Row < from.Row;
                case Color.Black:
                    return to.Row > from.Row;
                default:
                    return false;
            }
        }

        public bool IsValidMove(Position from,
            Position to,
            bool opponentPieceAtForwardLeft,
            bool opponentPieceAtForwardRight)
        {
            return IsForwardMove(from, to)
                   && IsTakingAllowedNumberOfForwardSteps(from, to)
                   && IsTakingAllowedNumberOfForwardSteps(from, to, opponentPieceAtForwardLeft,
                       opponentPieceAtForwardRight);
        }

        private bool IsTakingAllowedNumberOfForwardSteps(Position from, Position to) 
        {
            var rowsAbsDiff = Math.Abs(to.Row - from.Row);
            return rowsAbsDiff > 0 && rowsAbsDiff <= (from.Row == _initialRow ? 2 : 1);
        }

        private bool IsTakingAllowedNumberOfForwardSteps(Position from,
            Position to,
            bool opponentPieceAtForwardLeft,
            bool opponentPieceAtForwardRight)
        {
            var columnsDiff = to.Column - from.Column;

            if (columnsDiff == -1)
                return opponentPieceAtForwardLeft && Color == Color.White
                       || opponentPieceAtForwardRight && Color == Color.Black;

            if (columnsDiff == 1)
                return opponentPieceAtForwardRight && Color == Color.White
                       || opponentPieceAtForwardLeft && Color == Color.Black;

            return columnsDiff == 0;
        }
    }
}