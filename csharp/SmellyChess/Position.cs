namespace SmellyChess
{
    public class Position
    {
        private readonly int _x;
        private readonly int _y;

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}