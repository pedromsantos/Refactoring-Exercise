using System;

namespace RefactoredChess
{
    public class GameEngine
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private Player _currentPlayer;

        public ChessBoard Board { get; }

        public GameEngine(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;

            Board = new ChessBoard();
            Board.Reset();
            
            if (_currentPlayer == null || _player1.Color == Color.Black)
            {
                _currentPlayer = _player1;
                _player1.Color = Color.White;
                _player2.Color = Color.Black;
            }
            else
            {
                _currentPlayer = _player2;
                _player1.Color = Color.Black;
                _player2.Color = Color.White;
            }
        }

        public void Initialize()
        {
            Console.WriteLine("\nGame initialized");
            Console.WriteLine("Player " + _player1.Name + " has Color " + _player1.Color);
            Console.WriteLine("Player " + _player2.Name + " has Color " + _player2.Color);
            Console.WriteLine("");
            
            Board.Reset();
            
            Console.WriteLine(Board);
        }

        public void MakeMove(Move move)
        {
            Board.MovePiece(move);
            
            Console.WriteLine("");
            Console.WriteLine(Board);
            
            if (Board.IsKingDead())
            {
                End();
                Initialize();
            }
            else
            {
                _currentPlayer = NextPlayer();
            }
        }

        public bool IsValidMove(Move move)
        {
            return Board.IsValidMove(move, _currentPlayer);
        }

        private void End()
        {
            Console.WriteLine("Game Ended");

            var winner = _currentPlayer;
            winner.Increase();

            Console.WriteLine("WINNER - " + winner + "\n\n");
        }

        private Player NextPlayer()
        {
            return _player1 == _currentPlayer ? _player2 : _player1;
        }
    }
}