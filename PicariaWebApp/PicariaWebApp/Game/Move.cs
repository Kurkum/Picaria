using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Game
{
    public class Move
    {
        public Position OldPosition { get; set; }
        public Position NewPosition { get; set; }

        public Move(Position oldPosition, Position newPosition)
        {
            this.OldPosition = oldPosition;
            this.NewPosition = newPosition;
        }

        public Move(Position position, Status status)
        {
            OldPosition = new Position(position.X, position.Y, status);
            NewPosition = position;
        }

        public override bool Equals(object obj)
        {
            Move that = obj as Move;
            return this.OldPosition.Equals(that.OldPosition) && this.NewPosition.Equals(that.NewPosition);
        }

        static public bool AreListsOfMovesEqual(List<Move> l1, List<Move> l2)
        {
            {
                if (l1.Count != l2.Count)
                {
                    return false;
                }
                for (int i = 0; i < l1.Count; ++i)
                {
                    if (!l1.ElementAt(i).Equals(l2.ElementAt(i)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
