namespace RefactoredChess
{
    public class Move
    {
        public Move(Position from, Position to)
        {
            From = from;
            To = to;
        }

        public Position From { get; }
        public Position To { get; }
        
        public bool IsStraightLineMove()
        {
            return From.IsStraightLineMove(To);
        }

        public Position Translate()
        {
            return From.TranslatedPosition(From).TranslatedPosition(To);
        }

        public Position ForwardLeftPosition(Color color)
        {
            return new Position(ForwardRow(color), From.Column + (color == Color.White ? -1 : 1));
        }

        public Position ForwardRightPosition(Color color)
        {
            return new Position(ForwardRow(color), From.Column + (color == Color.White ? 1 : -1));
        }

        private int ForwardRow(Color color)
        {
            return From.Row + (color == Color.Black ? 1 : -1);
        }
    }
}