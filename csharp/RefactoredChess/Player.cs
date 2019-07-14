using RefactoredChess.Pieces;

namespace RefactoredChess
{
    public class Player
    {
        private int _gamesWon;

        public Player(string name)
        {
            _gamesWon = 0;
        }

        public Color Color { get; set; }
        public string Name { get; private set; }

        public void Increase()
        {
            _gamesWon++;
        }

        public bool IsMovingItsOwnPiece(Piece piece) 
        {
            return piece.Color == Color;
        }
    }
}