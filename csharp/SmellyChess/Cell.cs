using SmellyChess.Pieces;

namespace SmellyChess
{
    public class Cell
    {
        private readonly Color _color;

        public Cell(Color color)
        {
            _color = color;
        }

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