using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PicariaWebApp.Models;

namespace PicariaWebApp.Game
{
    public class StandardRules : IRules
    {
        public List<Position> GetPossibleMovesOfPawn(List<Position> board, Position pawnPostion)
        {
            List<Position> ret = new List<Position>();
            foreach (Position e in board)
            {
                if (e.Status == Status.FreeToCapture && (e.X == pawnPostion.X || e.X == pawnPostion.X - 1 || e.X == pawnPostion.X + 1) &&
                    (e.Y == pawnPostion.Y || e.Y == pawnPostion.Y - 1 || e.Y == pawnPostion.Y + 1))
                {
                    ret.Add(e);
                }
            }
            return ret;
        }

        public List<Tuple<Position, Position>> GetPossibleMovesOfPlayer(List<Position> board, Status player)
        {
            List<Tuple<Position, Position>> ret = new List<Tuple<Position, Position>>();
            foreach (Position e in board)
            {
                if (e.Status == player)
                {
                    List<Position> possibleMoves = GetPossibleMovesOfPawn(board, e);
                    foreach (Position e2 in possibleMoves)
                    {
                        ret.Add(Tuple.Create(e, e2));
                    }
                }
            }
            return ret;
        }
    }
}
