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

        public static List<Position> GetEmptyBoard()
        {
            return new List<Position>()
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
        }

        public Board()
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

        public Board(List<Position> positions)
        {
            foreach(var position in positions)
            {
                position.TranslatePosition();
            }

            Positions = positions;
        }

        public void ExecuteMove(Move move)
        {
            move.NewPosition.Status = move.OldPosition.Status;
            move.OldPosition.Status = Status.FreeToCapture;
        }

        public Board GetCopyOfBoardWithMoveExecuted(Move move)
        {
            Board board = new Board();
            Position oldPosiotion = move.OldPosition.Clone();
            Position newPosition = move.NewPosition.Clone();
            newPosition.Status = oldPosiotion.Status;
            oldPosiotion.Status = Status.FreeToCapture;
            foreach(Position position in Positions)
            {
                if (position.Equals(oldPosiotion))
                {
                    board.Positions.Add(oldPosiotion);
                }
                else if (position.Equals(newPosition))
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
            if(this.Positions.Count != that.Positions.Count)
            {
                return false;
            }
            for(int i = 0; i < this.Positions.Count; ++i)
            {
                if (!this.Positions[i].Equals(that.Positions[i])){
                    return false;
                }
            }
            return true;
        }
    }
}
