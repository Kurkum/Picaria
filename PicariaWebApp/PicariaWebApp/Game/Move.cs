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

        public Move(Position position)
        {
            OldPosition = NewPosition = position;
        }

        public override bool Equals(object obj)
        {
            Move that = obj as Move;
            return this.OldPosition == that.OldPosition && this.NewPosition == that.NewPosition;
        }
    }
}
