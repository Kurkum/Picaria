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

        private Dictionary<int, int> XPositionToRealPosition = new Dictionary<int, int>
        {
            {50,  0},
            {400, 1},
            {750, 2}
        };
        private Dictionary<int, int> YPositionToRealPosition = new Dictionary<int, int>
        {
            {50,  0},
            {300, 1},
            {550, 2}
        };

        public void TranslatePosition()
        {
            this.X = XPositionToRealPosition[this.X];
            this.Y = YPositionToRealPosition[this.Y];
        }
        public Position()
        {

        }

        public Position(int x, int y, Status status)
        {
            X = x;
            Y = y;
            Status = status;
        }

        public Position Clone()
        {
            return this.MemberwiseClone() as Position;
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
