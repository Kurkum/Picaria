using PicariaWebApp.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Models
{
    public class Board
    {
        public List<Position> Positions { get; set; }
        public IRules Rules;

        Board()
        {
            Positions = new List<Position>();
        }

        public void RealizeMove(Move move)
        {
            move.NewPosition.Status = move.OldPosition.Status;
            move.OldPosition.Status = Status.FreeToCapture;
        }
        public Board GetCopyOfBoardWithMoveRealized(Move move)
        {
            Board board = new Board();
            Position oldPosiotion = move.OldPosition.Clone();
            Position newPosition = move.NewPosition.Clone();
            newPosition.Status = oldPosiotion.Status;
            oldPosiotion.Status = Status.FreeToCapture;
            foreach(Position position in Positions)
            {
                if (position.HaveSameCoordinates(oldPosiotion))
                {
                    board.Positions.Add(oldPosiotion);
                }
                else if (position.HaveSameCoordinates(newPosition))
                {
                    board.Positions.Add(newPosition);
                }
                else {
                    board.Positions.Add(position.Clone());
                }
            }
            return board;
        }
    }
}
