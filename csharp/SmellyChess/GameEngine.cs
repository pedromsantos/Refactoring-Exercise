using System;
using SmellyChess.Pieces;

namespace SmellyChess
{
    public class GameEngine
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly ChessBoard _chessBoard;
        private Player _currentPlayer;

        public GameEngine(Player player1, Player player2)
        {
            _chessBoard = new ChessBoard();
            _player1 = player1;
            _player2 = player2;
            ResetBoard();
        }

        private void ResetBoard()
        {
            for (var column = 0; column < 8; column++) 
            {
                if (column == 0) 
                {
                    _chessBoard.Board[7, column].Piece = new LeftRook(Color.WHITE);
                } else if (column == 1) {
                    _chessBoard.Board[7, column].Piece = new LeftKnight(Color.WHITE);
                } else if (column == 2) {
                    _chessBoard.Board[7, column].Piece = new LeftBishop(Color.WHITE);
                } else if (column == 3) {
                    _chessBoard.Board[7, column].Piece = new King(Color.WHITE);
                } else if (column == 4) {
                    _chessBoard.Board[7, column].Piece = new Queen(Color.WHITE);
                } else if (column == 5) {
                    _chessBoard.Board[7, column].Piece = new RightBishop(Color.WHITE);
                } else if (column == 6) {
                    _chessBoard.Board[7, column].Piece = new RightKnight(Color.WHITE);
                } else if (column == 7) {
                    _chessBoard.Board[7, column].Piece = new RightRook(Color.WHITE);
                }
            }
            
            for (var column = 0; column < 8; column++) {
                _chessBoard.Board[6, column].Piece = new Pawn(Color.WHITE);
            }
            
            for (var column = 0; column < 8; column++) {
                if (column == 0) {
                    _chessBoard.Board[0, column].Piece = new LeftRook(Color.BLACK);
                } else if (column == 1) {
                    _chessBoard.Board[0, column].Piece = new LeftKnight(Color.BLACK);
                } else if (column == 2) {
                    _chessBoard.Board[0, column].Piece = new LeftBishop(Color.BLACK);
                } else if (column == 3) {
                    _chessBoard.Board[0, column].Piece = new King(Color.BLACK);
                } else if (column == 4) {
                    _chessBoard.Board[0, column].Piece = new Queen(Color.BLACK);
                } else if (column == 5) {
                    _chessBoard.Board[0, column].Piece = new RightBishop(Color.BLACK);
                } else if (column == 6) {
                    _chessBoard.Board[0, column].Piece = new RightKnight(Color.BLACK);
                } else if (column == 7) {
                    _chessBoard.Board[0, column].Piece = new RightRook(Color.BLACK);
                }
            }
            for (var column = 0; column < 8; column++) {
                _chessBoard.Board[1, column].Piece = new Pawn(Color.BLACK);
            }
            
            _chessBoard.KingDead = false;
        }

        public void Initialize()
        {
            if (_currentPlayer == null || _player1.Color == Color.BLACK) {
                _currentPlayer = _player1;
                _player1.Color = Color.WHITE;
                _player2.Color = Color.BLACK;
            } else {
                _currentPlayer = _player2;
                _player1.Color =Color.BLACK;
                _player2.Color = Color.WHITE;
            }
            
            Console.WriteLine("\nGame initialized");
            Console.WriteLine("Player " + _player1.Name + " has Color " + _player1.Color);
            Console.WriteLine("Player " + _player2.Name + " has Color " + _player2.Color);
            Console.WriteLine("");
            ResetBoard();
            Console.WriteLine(_chessBoard);
        }
        
        public void StartGame()
        {
            while (true) {
                Console.WriteLine("Next move is of " 
                                  + _currentPlayer.Name
                                  + " [" + _currentPlayer.Color 
                                  + "]");
                
                Console.WriteLine("Enter position (row col) of piece to move: ");
                var from = InputPosition();
                Console.WriteLine("Enter destination position: ");
                var to = InputPosition();
                
                var move = new Move(from, to);
                
                if (IsValidMove(move)) {
                    MakeMove(move);
                } else {
                    Console.WriteLine("Invalid move!");
                }
            }
        }

        private Position InputPosition()
        {
            var row = Convert.ToInt32(Console.ReadLine())- 1;
            var col = Convert.ToInt32(Console.ReadLine())- 1;
            return new Position(row, col);
        }

        public bool IsValidMove(Move move)
        {
            return IsPlayerMovingItsOwnColoredPiece(move.From)
                   && _chessBoard.IsValidMove(move.From.Row, move.From.Column,
                       move.To.Row, move.To.Column);
        }

        private bool IsPlayerMovingItsOwnColoredPiece(Position from) {
            return !_chessBoard.IsEmpty(from)
                   && _chessBoard.PieceAt(from).Color == _currentPlayer.Color;
        }
        
        public void MakeMove(Move move)
        {
            _chessBoard.MovePiece(move.From.Row, move.From.Column, move.To.Row,
                move.To.Column);
            Console.WriteLine("");
            Console.WriteLine(_chessBoard);
            if (_chessBoard.IsKingDead()) {
                End();
                Initialize();
            } else {
                _currentPlayer = NextPlayer();
            }
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