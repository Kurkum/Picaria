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
            Positions = new List<Position>()
            {
                new Position(0,0),
                new Position(1,0),
                new Position(2,0),

                new Position(0,1),
                new Position(1,1),
                new Position(2,1),

                new Position(0,2),
                new Position(1,2),
                new Position(2,2)
            };
            Rules = new StandardRules();
        }

        public void RealizeMove(Move move)
        {
            move.NewPosition.Status = move.OldPosition.Status;
            move.OldPosition.Status = Status.FreeToCapture;
        }
<<<<<<< HEAD
        public Board GetCopyOfBoardWithMoveRealized(Move move)
=======

        public Board GetCopyOfBoardWithMoveExecuted(Move move)
>>>>>>> Rules
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

        public int CountCapturedPositions()
        {
            int result = 0;
            foreach(Position position in Positions)
            {
                if (position.Status != Status.FreeToCapture)
                {
                    ++result;
                }
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            Board that = obj as Board;
            return this.Positions.Count() == that.Positions.Count() && !this.Positions.Except(that.Positions).Any();
        }
    }
}
