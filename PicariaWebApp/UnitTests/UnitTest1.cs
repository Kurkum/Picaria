using PicariaWebApp.Models;
using PicariaWebApp.Player;
using System;
using UnitTests.Builders;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var position1 = new Position(0, 0, Status.PlayerOne);
            var position2 = new Position(0, 1, Status.PlayerOne);
            var position3 = new Position(0, 2, Status.PlayerOne);

            var board = new BoardBuilder()
                .WithPositions(position1, position2, position3)
                .Build();
        }
    }
}
