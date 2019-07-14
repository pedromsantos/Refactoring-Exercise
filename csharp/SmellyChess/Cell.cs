using SmellyChess.Pieces;

namespace SmellyChess
{
    public class Cell
    {
        private readonly Color _color;
        private Piece _piece;

        public Cell(Color color)
        {
            _color = color;
        }

        public Piece Piece { get; set; }
        
        public bool IsEmpty()
        {
            return _piece == null;
        }
        
        public void RemovePiece()
        {
            _piece = null;
        }
        
        public void SetPiece(Piece piece)
        {
            _piece = piece;
        }
    }
}