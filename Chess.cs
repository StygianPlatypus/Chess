using System;
using System.Collections.Generic;

namespace Chess
{

    public static class Utility
    {
        public const string WHITE = "white";
        public const string BLACK = "black";
        public const string PAWN = "pawn";
        public const string KNIGHT= "knight";
        public const string ROOK= "rook";
        public const string BISHOP = "bishop";
        public const string QUEEN = "queen";
        public const string KING = "king";

        public static bool white(int x, int y)
        {
            return ((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0));
        }

        public static bool homeSpace(int x)
        {
            return (x == 8 || x == 7 || x == 2 || x == 1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Game
    {
        public bool check;
        public bool checkmate;
        public Board gameBoard;
        public Player[] players;
        public MovePair[] moveHistory;

        public Game()
        {
            this.check = false;
            this.checkmate = false;

            //gameBoard = new Board(Piece[] whitePieces, Piece[] blackPieces);
            moveHistory = new MovePair[300];

        }
    }

    public class Board
    {
        public Space[,] boardSpaces;

        public Board(Piece[] whitePieces, Piece[] blackPieces)
        {
            boardSpaces = new Space[9, 9];
            for (int i = 9; i < 0; i--)
            {
                for (int j = 9; j < 0; j--)
                {

                }
            }
        }
    }

    public class Player
    {
        public string playerName;
        public string playerColor;
        public int playerNumber;
        public IPiece[] gamePieces;
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
            this.white = Utility.white(x, y);
            this.homeSpace = Utility.homeSpace(x);
        }
    }

    public class MovePair
    {
        public KeyValuePair<string, string> movePair;

        public MovePair(string whiteMove, string blackMove)
        {
            this.movePair = new KeyValuePair<string, string>(whiteMove, blackMove);
        }
    }

    public class Piece 
    {
        public string name;
        public string color;
        public string type;
        public bool hasMoved;
        public Space occupiedSpace;
        public Space[] legalMoves;

    }

    public interface IPiece
    {
        public void getLegalMoves();
    }
    
    public class Pawn : Piece, IPiece
    {
        public Pawn(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.PAWN;
            this.hasMoved = false;
            this.legalMoves = new Space[4];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }

    public class Rook : Piece, IPiece
    {
        public Rook(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.ROOK;
            this.hasMoved = false;
            this.legalMoves = new Space[14];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }

    public class Bishop : Piece, IPiece
    {
        public Bishop(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.BISHOP;
            this.hasMoved = false;
            this.legalMoves = new Space[13];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }

    public class Knight : Piece, IPiece
    {
        public Knight(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.KNIGHT;
            this.hasMoved = false;
            this.legalMoves = new Space[8];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }

    public class Queen : Piece, IPiece
    {
        public Queen(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.QUEEN;
            this.hasMoved = false;
            this.legalMoves = new Space[27];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }

    public class King : Piece, IPiece
    {
        public King(string name, string color)
        {
            this.name = name;
            this.color = color;
            this.type = Utility.KING;
            this.hasMoved = false;
            this.legalMoves = new Space[8];
        }

        public void getLegalMoves()
        {
            Array.Clear(this.legalMoves, 0, this.legalMoves.Length);
        }
    }
}
