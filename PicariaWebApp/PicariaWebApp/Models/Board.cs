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

        static public Board GetBasicBoard()
        {
            Board board = new Board();
            board.Positions = new List<Position>()
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
            return board;
        }

        public Board()
        {
            Positions = new List<Position>();
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
            if (move.OldPosition.HasSameCoordinates(move.NewPosition))
            {
                move.NewPosition.Status = move.OldPosition.Status;
            }
            else
            {
                move.NewPosition.Status = move.OldPosition.Status;
                move.OldPosition.Status = Status.FreeToCapture;
            }
        }

        public Board GetCopyOfBoardWithMoveExecuted(Move move)
        {            
            Board board = new Board();
            if (move.OldPosition.HasSameCoordinates(move.NewPosition))
            {
                foreach(Position position in Positions)
                {
                    if (position.HasSameCoordinates(move.OldPosition))
                    {
                        board.Positions.Add(move.OldPosition.Clone());
                    }
                    else
                    {
                        board.Positions.Add(position.Clone());
                    }
                }
                return board;
            }
            Position oldPosition = move.OldPosition.Clone();
            Position newPosition = move.NewPosition.Clone();
            newPosition.Status = oldPosition.Status;
            oldPosition.Status = Status.FreeToCapture;
            foreach(Position position in Positions)
            {
                if (position.HasSameCoordinates(oldPosition))
                {
                    board.Positions.Add(oldPosition);
                }
                else if (position.HasSameCoordinates(newPosition))
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

        public Board Clone()
        {
            Board other = (Board) this.MemberwiseClone();
            other.Positions = new List<Position>();
            foreach(Position position in Positions)
            {
                other.Positions.Add(position.Clone());
            }
            return other;
        }

        public override string ToString()
        {
            string result = "{";
            foreach(Position position in Positions)
            {
                result += position.ToString();
            }
            result += ")";
            return result;
        }
    }
}
