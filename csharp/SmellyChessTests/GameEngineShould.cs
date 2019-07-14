using SmellyChess;
using SmellyChess.Pieces;
using Xunit;

namespace SmellyChessTests
{
    public class GameEngineShould
    {
        public GameEngineShould()
        {
            var player1 = new Player("A");
            var player2 = new Player("B");
            _gameEngine = new GameEngine(player1, player2);
            _gameEngine.Initialize();
        }

        private readonly GameEngine _gameEngine;

        private bool IsValidMoveHelper(Position from, Position to)
        {
            return _gameEngine.IsValidMove(new Move(from, to));
        }

        private void MakeMoveHelper(Position from, Position to)
        {
            _gameEngine.MakeMove(new Move(from, to));
        }

        [Fact]
        public void DoNoMovementOfPieceForFirstPlayer()
        {
            var fromPosition = new Position(6, 0);
            var toPosition = new Position(6, 0);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void DoNoMovementOfPieceForSecondPlayer()
        {
            var fromPosition = new Position(1, 0);
            var toPosition = new Position(1, 0);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void MoveOfKingForSecondPlayer()
        {
            var fromPosition = new Position(0, 1);
            var toPosition = new Position(2, 0);
            MakeMoveHelper(fromPosition, toPosition);

            var chessBoard = _gameEngine.Board;
            Assert.True(chessBoard.PieceAt(new Position(0, 1)) == null);
            Assert.True(chessBoard.PieceAt(new Position(2, 0)) is Knight);
        }

        [Fact]
        public void MovePawnForFirstPlayer()
        {
            var fromPosition = new Position(6, 6);
            var toPosition = new Position(4, 6);

            MakeMoveHelper(fromPosition, toPosition);

            var chessBoard = _gameEngine.Board;
            Assert.True(chessBoard.PieceAt(new Position(6, 6)) == null);
            Assert.True(chessBoard.PieceAt(new Position(4, 6)) is Pawn);
        }

        [Fact]
        public void NotAllowSecondPlayerToPlayIfNotInTurn()
        {
            var fromPosition = new Position(0, 1);
            var toPosition = new Position(2, 0);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void NotValidateInvalidMoveBecauseThereIsAPieceOnPathForFirstPlayer()
        {
            var fromPosition = new Position(7, 7);
            var toPosition = new Position(5, 7);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void NotValidateInvalidMoveBecauseToIsNotEmptyForFirstPlayer()
        {
            var fromPosition = new Position(7, 7);
            var toPosition = new Position(6, 1);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void NotValidateInvalidMoveOfPawnForFirstPlayer()
        {
            var fromPosition = new Position(6, 0);
            var toPosition = new Position(5, 1);

            Assert.False(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void ValidateMoveOfKingForSecondPlayer()
        {
            _gameEngine.MakeMove(new Move(new Position(6, 1), new Position(5, 1)));

            var fromPosition = new Position(0, 1);
            var toPosition = new Position(2, 0);

            Assert.True(IsValidMoveHelper(fromPosition, toPosition));
        }

        [Fact]
        public void ValidateMoveOfPawnForFirstPlayer()
        {
            var fromPosition = new Position(6, 1);
            var toPosition = new Position(5, 1);

            Assert.True(IsValidMoveHelper(fromPosition, toPosition));
        }
    }
}