using System;

namespace SmellyChess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Color color) 
            : base(color, 'p')
        {
        }
        
        public override bool IsValidMove(Position from, Position to)
        {
            var columnsMoved = Math.Abs(to.Column - from.Column);
            var rowsMoved = Math.Abs(to.Row - from.Row);
            var isForwardMove = IsForwardMove(@from, to);
            var moveOneRow = columnsMoved <= 1 && rowsMoved == 1;
            var moveTwoRows = columnsMoved == 0 && rowsMoved == 2;
            return isForwardMove
                   && (moveOneRow || moveTwoRows);
        }

        private bool IsForwardMove(Position from, Position to)
        {
            switch (Color) 
            {
                case Color.WHITE:
                    return to.Row < from.Row;
                case Color.BLACK:
                    return to.Row > from.Row;
                default:
                    return false; 
            }
        }

        public bool IsValidMoveGivenContext(Position from,
                                               Position to,
                                               bool atInitialPosition,
                                               bool opponentPieceAtForwardLeft,
                                               bool opponentPieceAtForwardRight)
        {
            return IsForwardMove(from, to)
                   && IsTakingAllowedNumberOfForwardSteps(from, to, atInitialPosition)
                   && IsTakingAllowedNumberOfForwardSteps(from, to, opponentPieceAtForwardLeft, opponentPieceAtForwardRight);
        }

        private bool IsTakingAllowedNumberOfForwardSteps(Position from, Position to, bool atInitialPosition)
        {
            var rowsAbsDiff = Math.Abs(to.Row - from.Row);
            return rowsAbsDiff > 0 && (rowsAbsDiff <= (atInitialPosition ? 2 : 1));
        }

        private bool IsTakingAllowedNumberOfForwardSteps(Position from,
                                                             Position to,
                                                             bool opponentPieceAtForwardLeft,
                                                             bool opponentPieceAtForwardRight)
        {
            var columnsDiff = to.Column - from.Column;
            
            if (columnsDiff == -1)
            {
                return (opponentPieceAtForwardLeft && Color == Color.WHITE)
                       || (opponentPieceAtForwardRight && Color == Color.BLACK);
                
            }
            
            if (columnsDiff == 1) 
            {
                return (opponentPieceAtForwardRight && Color == Color.WHITE)
                       || (opponentPieceAtForwardLeft && Color == Color.BLACK);
            }
            
            return columnsDiff == 0;
        }
    }
}