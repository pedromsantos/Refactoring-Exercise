using System;

namespace RefactoredChess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Color color)
            : base(color)
        {
        }

        public override bool IsValidMove(Position from, Position to)
        {
            return Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column);
        }
    }
}