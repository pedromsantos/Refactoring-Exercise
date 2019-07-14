using System;

namespace RefactoredChess.Pieces
{
    public class Knight : Piece
    {
        public Knight(Color color)
            : base(color)
        {
        }

        public override bool IsValidMove(Position @from, Position to)
        {
            var columnDiff = Math.Abs(to.Column - @from.Column);
            var rowDiff = Math.Abs(to.Row - @from.Row);
            return columnDiff == 2 && rowDiff == 1 || columnDiff == 1 && rowDiff == 2;
        }
    }
}