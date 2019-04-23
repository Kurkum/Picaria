using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    public class PositionBuilder : Builder<PositionBuilder, Position>
    {
        public PositionBuilder WithCords(int x, int y)
        {
            State.X = x;
            State.Y = y;

            return this;
        }

        public PositionBuilder WithStatus(Status status)
        {
            State.Status = status;

            return this;
        }
    }
}
