using Xunit;
using SmellyChess;

namespace ChessGameTests
{
    public class GameEngineShould 
    {
        private readonly GameEngine _gameEngine;

        private bool IsValidMoveHelper(Position from, Position to)
        {
            return _gameEngine.IsValidMove(new Move(from, to));
        }

        private void MakeMoveHelper(Position from, Position to)
        {
            _gameEngine.MakeMove(new Move(from, to));
        }
        
        public GameEngineShould()
        {
            var player1 = new Player("A");
            var player2 = new Player("B");
            _gameEngine = new GameEngine(player1, player2);
            _gameEngine.Initialize();     
        }
        
        [Fact]
        public void DoNoMovementOfPieceForFirstPlayer()
        {
            var fromPosition = new Position(6, 0);
            var toPosition = new Position(6, 0);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }
    }
}