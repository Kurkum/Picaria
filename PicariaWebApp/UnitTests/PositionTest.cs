using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class PositionTest
    {
        [Fact]
        public void TestEquals()
        {
            Position pos1 = new Position(5, 5, Status.FreeToCapture);
            Position pos2 = new Position(5, 5, Status.FreeToCapture);
            Assert.Equal(pos1, pos2);
        }

        [Fact]
        public void TestNotEquals()
        {
            Position pos1 = new Position(5, 5, Status.FreeToCapture);
            Position pos2 = new Position(5, 5, Status.PlayerOne);
            Assert.NotEqual(pos1, pos2);
        }

        [Fact]
        public void TestHasSameCoordinates()
        {
            Position pos1 = new Position(5, 5, Status.FreeToCapture);
            Position pos2 = new Position(5, 5, Status.PlayerOne);
            Assert.True(pos1.HasSameCoordinates(pos2));
        }

        [Fact]
        public void TestNotHasSameCoordinates()
        {
            Position pos1 = new Position(4, 5, Status.FreeToCapture);
            Position pos2 = new Position(5, 5, Status.PlayerOne);
            Assert.False(pos1.HasSameCoordinates(pos2));
        }
    }
}
