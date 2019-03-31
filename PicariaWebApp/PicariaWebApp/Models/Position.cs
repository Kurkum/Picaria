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
    }

    public enum Status
    {
        PlayerOne, PlayerTwo, FreeToCapture
    }
}
