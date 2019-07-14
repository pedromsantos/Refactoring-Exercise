namespace SmellyChess
{
    internal class Direction
    {
        public int RowOffset { get; }
        public int ColumnOffset { get; }

        public Direction(int rowOffset, int columnOffset) {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
        }

    }
}