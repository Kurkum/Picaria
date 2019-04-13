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
    }
}
