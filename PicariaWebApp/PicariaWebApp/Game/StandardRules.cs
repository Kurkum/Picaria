using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PicariaWebApp.Models;

namespace PicariaWebApp.Game
{
    public class StandardRules : IRules
    {
        public bool SecondPhaseBegun;

        public StandardRules()
        {
            SecondPhaseBegun = false;
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
            if (this.SecondPhaseBegun)
            {
                return _secondPhaseGetPossibleMoves(board, player);
            }
            else
            {
                if (board.CountCapturedPositions()==6)
                {
                    this.SecondPhaseBegun = true;
                    return _secondPhaseGetPossibleMoves(board, player);
                }
                else
                {
                    return _firstPhaseGetPossibleMoves(board);
                }
            }
        }

        private List<Move> _firstPhaseGetPossibleMoves(Board board)
        {
            List<Move> ret = new List<Move>();
            foreach (Position e in board.Positions)
            {
                if(e.Status == Status.FreeToCapture)
                {
                    ret.Add(new Move(e));
                }
            }
            return ret;
        }

        private List<Move> _secondPhaseGetPossibleMoves(Board board, Status player)
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
