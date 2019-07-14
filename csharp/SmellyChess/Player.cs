namespace SmellyChess
{
    public class Player
    {
        private readonly string _name;
        private int _gamesWon;

        public Player(string name)
        {
            _name = name;
            _gamesWon = 0;
        }

        public Color Color { get; set; }
        public string Name { get; private set; }

        public void Increase()
        {
            _gamesWon++;
        }
    }
}