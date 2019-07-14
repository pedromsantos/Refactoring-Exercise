using System;

namespace RefactoredChess.Pieces
{
    public class Queen : Piece
    {
        public Queen(Color color)
            : base(color)
        {
        }

        public override bool IsValidMove(Position @from, Position to)
        {
            return Math.Abs(@from.Row - to.Row) == Math.Abs(@from.Column - to.Column)
                   || @from.Row == to.Row || @from.Column == to.Column;
        }
    }
}