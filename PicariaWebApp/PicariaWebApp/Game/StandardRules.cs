﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PicariaWebApp.Models;

namespace PicariaWebApp.Game
{
    public class StandardRules : IRules
    {

        public StandardRules()
        {
        }

        public List<Position> GetPossibleMovesOfPawn(Board board, Position pawnPostion)
        {
            List<Position> ret = new List<Position>();
            foreach (Position e in board.Positions)
            {
                if (e.Status == Status.FreeToCapture && (e.X == pawnPostion.X || e.X == pawnPostion.X - 1 || e.X == pawnPostion.X + 1) &&
                    (e.Y == pawnPostion.Y || e.Y == pawnPostion.Y - 1 || e.Y == pawnPostion.Y + 1))
                {
                    ret.Add(e);
                }
            }
            return ret;
        }

        public List<Move> GetPossibleMovesOfPlayer(Board board, Status player)
        {
            if (board.CountCapturedPositions() == 6)
            {
                return SecondPhaseGetPossibleMoves(board, player);
            }
            else
            {
                return FirstPhaseGetPossibleMoves(board, player);
            }
        }

        public List<Move> FirstPhaseGetPossibleMoves(Board board, Status player)
        {
            List<Move> ret = new List<Move>();
            foreach (Position e in board.Positions)
            {
                if (e.Status == Status.FreeToCapture)
                {
                    ret.Add(new Move(e, player));
                }
            }
            return ret;
        }

        public List<Move> SecondPhaseGetPossibleMoves(Board board, Status player)
        {
            List<Move> ret = new List<Move>();
            foreach (Position e in board.Positions)
            {
                if (e.Status == player)
                {
                    List<Position> possibleMoves = GetPossibleMovesOfPawn(board, e);
                    foreach (Position e2 in possibleMoves)
                    {
                        ret.Add(new Move(e, e2));
                    }
                }
            }
            return ret;
        }
    }
}
