using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class MoveTest
    {
        [Fact]
        void TestEquals()
        {
            Assert.Equal(new Move(new Position(0, 1, Status.FreeToCapture), new Position(0, 2, Status.FreeToCapture)),
                new Move(new Position(0, 1, Status.FreeToCapture), new Position(0, 2, Status.FreeToCapture)));
        }
        [Fact]
        void TestNotEquals()
        {
            Assert.NotEqual(new Move(new Position(0, 1), new Position(3, 2)), new Move(new Position(0, 1), new Position(0, 2)));
        }
    }
}
