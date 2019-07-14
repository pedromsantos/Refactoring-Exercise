namespace RefactoredChess.Pieces
{
    public class Piece
    {
        protected Piece(Color color)
        {
            Color = color;
        }

        public Color Color { get; }

        public virtual bool IsValidMove(Position from, Position to)
        {
            return false;
        }

        public static Piece CreatePieceFor(int column, Color color)
        {
            switch (column)
            {
                case 0:
                    return new Rook(color);
                case 1:
                    return new Knight(color);
                case 2:
                    return new Bishop(color);
                case 3:
                    return new King(color);
                case 4:
                    return new Queen(color);
                case 5:
                    return new Bishop(color);
                case 6:
                    return new Knight(color);
                case 7:
                    return new Rook(color);
                default:
                    return new Pawn(color);
            }
        }
    }
}