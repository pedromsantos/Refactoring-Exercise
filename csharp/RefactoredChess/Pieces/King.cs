using System;

namespace RefactoredChess.Pieces
{
    internal class King : Piece
    {
        public King(Color color)
            : base(color)
        {
        }
        
        public override bool IsValidMove(Position from, Position to)
        {
            return Math.Abs(@from.Row - to.Row) == 1 && Math.Abs(@from.Column - to.Column) == 1;
        }
    }
}