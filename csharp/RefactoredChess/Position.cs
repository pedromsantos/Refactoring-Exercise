using System;

namespace RefactoredChess
{
    public class Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        private bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }

        public Position TranslatedPosition(Position to)
        {
            var direction = CreateDirection(to);
            return new Position(Row + direction.RowOffset, Column + direction.ColumnOffset);
        }

        public bool IsStraightLineMove(Position to)
        {
            return Math.Abs(Row - to.Row) == Math.Abs(Column - to.Column)
                   || Row == to.Row || Column == to.Column;
        }

        public Position IncrementRow()
        {
            return new Position(Row + 1, Column);
        }

        public Position DecrementRow()
        {
            return new Position(Row - 1, Column);
        }
        
        public Position IncrementColumn()
        {
            return new Position(Row, Column + 1);
        }

        public Position DecrementDecrement()
        {
            return new Position(Row, Column - 1);
        }
        
        private Direction CreateDirection(Position to)
        {
            return new Direction(RowOffset(to), ColumnOffset(to));
        }

        private int ColumnOffset(Position to)
        {
            return Math.Max(-1, Math.Min(1, to.Column.CompareTo(Column)));
        }

        private int RowOffset(Position to)
        {
            return Math.Max(-1, Math.Min(1, to.Row.CompareTo(Row)));
        }
    }
}