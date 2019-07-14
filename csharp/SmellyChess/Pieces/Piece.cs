using System;

namespace SmellyChess.Pieces
{
    public class Piece
    {
        public Color Color { get; }
        private char Type { get; }


        protected Piece(Color color, char type)
        {
            Color = color;
            Type = type;
        }

        public bool IsValidMove(Position from, Position to)
        {
            switch (Type) {
                case 'b':
                    return Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column);
                case 'r':
                    return from.Row == to.Row || from.Column == to.Column;
                case 'k':
                    int columnDiff = Math.Abs(to.Column - from.Column);
                    int rowDiff = Math.Abs(to.Row - from.Row);
                    return (columnDiff == 2 && rowDiff == 1) || (columnDiff == 1 && rowDiff == 2);
                case 'q':
                    return Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column)
                           || from.Row == to.Row || from.Column == to.Column;
                case 'K':
                    return (Math.Abs(from.Row - to.Row) == 1) && (Math.Abs(from.Column - to.Column) == 1);
                default:
                    return false;
            }
        }
    }
}