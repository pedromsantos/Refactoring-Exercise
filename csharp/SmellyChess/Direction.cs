namespace SmellyChess
{
    internal class Direction
    {
        public Direction(int rowOffset, int columnOffset)
        {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
        }

        public int RowOffset { get; }
        public int ColumnOffset { get; }
    }
}