namespace RefactoredChess.Pieces
{
    public class Rook : Piece
    {
        public Rook(Color color)
            : base(color)
        {
        }

        public override bool IsValidMove(Position @from, Position to)
        {
            return @from.Row == to.Row || @from.Column == to.Column;
        }
    }
}