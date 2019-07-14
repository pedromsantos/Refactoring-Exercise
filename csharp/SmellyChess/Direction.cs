namespace SmellyChess
{
    internal class Direction
    {
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        
        public Direction(int rowOffset, int columnOffset) {
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
        }
    }
}