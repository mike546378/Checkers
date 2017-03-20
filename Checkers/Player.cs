﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    //Class handles a instance of a player, AIPlayer and RemotePlayer both extend from this class
    class Player
    {


        private checkerPiece.Team team;
        private PlayerType playerType;
        private BoardManager manager;

        public enum PlayerType
        {
            Human = 1,
            AI = 2,
            Remote = 3,
        }


        //Initilization through constructor
        public Player(checkerPiece.Team t, BoardManager m)
        {
            team = t;
            playerType = PlayerType.Human;
            manager = m;
            highlightAvaliablePieces(false);
        }


        //Highlights all avaliable moves
        public void highlightAvaliablePieces(bool jumpOnly)
        {
            List<checkerPiece> avaliableMoves = new List<checkerPiece>();       //Pieces with avaliable regular moves
            List<checkerPiece> avaliableJumpMoves = new List<checkerPiece>();   //Pieces with avaliable jump moves

            //Retrieve all pieces with avaliable moves and jumpMoves
            foreach (checkerPiece piece in manager.getAllPieces(team)) {
                if (piece.getMoves().Count > 0)
                    avaliableMoves.Add(piece);
                if (piece.getJumpMoves().Count > 0)
                    avaliableJumpMoves.Add(piece);
            }

            //Highlight any piece with a jump move avaliable
            if (avaliableJumpMoves.Count > 0 && !jumpOnly)
            {
                foreach (checkerPiece piece in avaliableJumpMoves)
                    piece.highlight();
                return;
            }

            //Highlight any piece with an avaliable move
            foreach (checkerPiece piece in avaliableMoves)
                piece.highlight();

        }

        //Attempts to move a piece to a given location
        public Boolean move(checkerPiece piece, BoardTile location)
        {
            if (piece.getTeam() != team || location == null)
                return false;
            piece.move(location);
            return true;
        }

        //Initiates players turn
        public void startTurn()
        {

        }

        //Ends players turn
        public void endTurn()
        {

        }
    }
}
