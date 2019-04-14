using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Status Status { get; set; }

        public Position Clone()
        {
            Position clone = new Position();
            clone.Status = Status;
            clone.X = X;
            clone.Y = Y;
            return clone;
        }

        public bool HaveSameCoordinates(Position position)
        {
            return (X == position.X && Y == position.Y);
        }
    }

    public enum Status
    {
        PlayerOne, PlayerTwo, FreeToCapture
    }
}
