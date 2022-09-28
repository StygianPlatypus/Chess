using System;
using System.Collections.Generic;

namespace Chess
{
    class Program
    {
        static void Main()
        {
            Game newGame = new Game();
            Board newBoard = newGame.gameBoard;

            do
            {
                Utility.printBoard(newBoard);
                Console.WriteLine("Enter in the next move");
                SpacePair move = Play.parseInput(newBoard, Console.ReadLine());
                //Board gameBoard, Space newSpace, Piece piece, Player player
                Piece movedPiece = move.spacePair.Key.occupyingPiece;
                Play.reassignSpaceAndPiece(newBoard, move.spacePair.Value, movedPiece, newGame.player1);
                Console.Clear();
            } while (0 == 0);
        }
    }

    public static class Utility
    {
        public const string WHITE = "white";
        public const string BLACK = "black";
        public const string PAWN = "pawn";
        public const string KNIGHT = "knight";
        public const string ROOK = "rook";
        public const string BISHOP = "bishop";
        public const string QUEEN = "queen";
        public const string KING = "king";
        public static readonly char[] LETTERS = {'*','a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
		public static readonly char[] NUMBERS = {'*', '8', '7', '6', '5', '4', '3', '2', '1'};

        public static bool white(int x, int y)
        {
            return ((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0));
        }

        public static bool homeSpace(int x)
        {
            return (x == 8 || x == 1);
        }
		
		public static List<Piece> createOrderedPieceSet(string color)
		{
			List<Piece> pieces = new List<Piece>();
			string name;
			for (int i = 1; i <= 8; i++)
			{
				name = color + " pawn " + i.ToString();
				pieces.Add(new Pawn(name, color));
			}
			name = color + " rook 1";
			pieces.Add(new Rook(name, color));
			name = color + " knight 1";
			pieces.Add(new Knight(name, color));
			name = color + " bishop 1";
			pieces.Add(new Bishop(name, color));
			name = color + " queen";
			pieces.Add(new Queen(name, color));
			name = color + " king";
			pieces.Add(new King(name, color));
			name = color + " bishop 2";
			pieces.Add(new Bishop(name, color));
			name = color + " knight 2";
			pieces.Add(new Knight(name, color));
			name = color + " rook 2";
			pieces.Add(new Rook(name, color));
			
			return pieces;
		}
		
		public static void setupPiecesOnBoard(Board gameBoard, Player player1, Player player2)
		{
            List<Piece> playerPieces = null;
            int pieceIndex = 0;
            for (int i = 1; i <= 4; i++)
			{
				int row = -1;
				switch (i)
				{
					case 1:
						row = 7;
						playerPieces =  player1.gamePieces;
                        break;
					case 2:
						row = 8;
                        break;
					case 3:
						row = 2;
						playerPieces = player2.gamePieces;
                        break;
					case 4:
						row = 1;
                        break;
				}
				for (int j = 1; j <= 8; j++)
				{
					gameBoard.boardSpaces[row,j].occupyingPiece = playerPieces[pieceIndex];
                    playerPieces[pieceIndex].occupiedSpace = gameBoard.boardSpaces[row, j];
                    pieceIndex = (pieceIndex >= 15) ? 0 : pieceIndex += 1;
				}
			}
		}

        public static void printBoard(Board gameBoard)
        {
            for (int row = 1; row < 9; row++)
            {
                for (int column = 1; column < 9; column++)
                {
                    Space thisSpace = gameBoard.boardSpaces[row, column];
                    if (thisSpace.occupyingPiece != null)
                    {
                        Console.WriteLine(thisSpace.label + ": " + thisSpace.occupyingPiece.name);
                    }
                    else
                    {
                        Console.WriteLine(thisSpace.label);
                    }
                }
            }
        }
    }

    public static class Play
    {
        public static SpacePair parseInput(Board gameBoard, string move)
        {
            Char[] coordinates = move.ToCharArray();
            int priorY = Array.IndexOf(Utility.LETTERS, coordinates[0]);
            int priorX = Array.IndexOf(Utility.NUMBERS, coordinates[1]);
            int nextY = Array.IndexOf(Utility.LETTERS, coordinates[2]);
            int nextX = Array.IndexOf(Utility.NUMBERS, coordinates[3]);
            Space previousSpace = gameBoard.boardSpaces[priorX, priorY];
            Space nextSpace = gameBoard.boardSpaces[nextX, nextY];
            return new SpacePair(previousSpace, nextSpace);
        }
        public static void reassignSpaceAndPiece(Board gameBoard, Space newSpace, Piece piece, Player player)
        {
            int priorX = piece.occupiedSpace.x;
            int priorY = piece.occupiedSpace.y;
            int x = newSpace.x;
            int y = newSpace.y;
            int pieceIndex = player.gamePieces.IndexOf(piece);
            gameBoard.boardSpaces[x, y].occupyingPiece = player.gamePieces[pieceIndex];
            player.gamePieces[pieceIndex].occupiedSpace = gameBoard.boardSpaces[x, y];
            gameBoard.boardSpaces[priorX, priorY].occupyingPiece = null;
        }
    }

    public class Game
    {
        public bool check;
        public bool checkmate;
        public Board gameBoard;
        public Player player1;
		public Player player2;
        public List<MovePair> moveHistory;

        public Game()
        {
            check = false;
            checkmate = false;
            moveHistory = new List<MovePair>();
			player1 = new Player("Player 1", 1);
			player2 = new Player("Player 2", 2);
			gameBoard = new Board();
			Utility.setupPiecesOnBoard(gameBoard, player1, player2);
        }
    }

    public class Board
    {
        public Space[,] boardSpaces;

        public Board()
        {
            boardSpaces = new Space[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
					boardSpaces[row, column] = new Space(row, column);
                }
            }
        }
    }

    public class Player
    {
        public string playerName;
        public string playerColor;
        public int playerNumber;
        public List<Piece> gamePieces;

        public Player(string playerName, int playerNumber)
        {
            this.playerName = playerName;
            this.playerNumber = playerNumber;
            playerColor = (playerNumber == 1) ? Utility.WHITE : Utility.BLACK;
            gamePieces = Utility.createOrderedPieceSet(playerColor);
        }
    }

    public class Space
    {
        public int x;
        public int y;
        public string label;
        public bool white;
        public bool homeSpace;
        public Piece occupyingPiece;

        public Space(int x, int y)
        {
            this.x = x;
            this.y = y;
            if (x == 0)
            {
                label = Char.ToString(Utility.LETTERS[y]);
            }
            if (y == 0)
            {
                label = Char.ToString(Utility.NUMBERS[x]);
            }
            if (x != 0 && y != 0)
            {
                label = Char.ToString(Utility.LETTERS[y]) + Char.ToString(Utility.NUMBERS[x]);
            }
            if (x != 0 || y != 0)
            {
                white = Utility.white(x, y);
                homeSpace = Utility.homeSpace(x);
            }
        }
    }

    public class SpacePair
    {
        public KeyValuePair<Space, Space> spacePair;

        public SpacePair(Space currentSpace, Space nextSpace)
        {
            spacePair = new KeyValuePair<Space, Space>(currentSpace, nextSpace);
        }
    }

    public class MovePair
    {
        public KeyValuePair<string, string> movePair;

        public MovePair(string whiteMove, string blackMove)
        {
            movePair = new KeyValuePair<string, string>(whiteMove, blackMove);
        }
    }

    public abstract class Piece
    {
        public string name;
        public string color;
        public string type;
        public bool hasMoved;
        public Space occupiedSpace;
        public Space[] legalMoves;

        public abstract void getLegalMoves();
    }

    public class Pawn : Piece
    {
        public Pawn(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.PAWN;
            hasMoved = false;
            legalMoves = new Space[4];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }

    public class Rook : Piece
    {
        public Rook(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.ROOK;
            hasMoved = false;
            legalMoves = new Space[14];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
            Console.WriteLine("a rook moves.");
        }
    }

    public class Bishop : Piece
    {
        public Bishop(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.BISHOP;
            hasMoved = false;
            legalMoves = new Space[13];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }

    public class Knight : Piece
    {
        public Knight(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.KNIGHT;
            hasMoved = false;
            legalMoves = new Space[8];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }

    public class Queen : Piece
    {
        public Queen(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.QUEEN;
            hasMoved = false;
            legalMoves = new Space[27];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }

    public class King : Piece
    {
        public King(string name, string color)
        {
            this.name = name;
            this.color = color;
            type = Utility.KING;
            hasMoved = false;
            legalMoves = new Space[8];
        }

        public override void getLegalMoves()
        {
            Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }
}
