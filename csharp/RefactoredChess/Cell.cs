using RefactoredChess.Pieces;

namespace RefactoredChess
{
    public class Cell
    {
        public Piece Piece { get; set; }

        public bool IsEmpty()
        {
            return Piece == null;
        }

        public void RemovePiece()
        {
            Piece = null;
        }
    }
}