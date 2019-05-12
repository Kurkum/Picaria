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
            var position0 = new Position(0, 0, Status.PlayerOne);
            var position1 = new Position(0, 1, Status.PlayerOne);
            var position2 = new Position(0, 2, Status.PlayerOne);
            var position3 = new Position(0, 2, Status.FreeToCapture);
            var position4 = new Position(0, 2, Status.PlayerTwo);
            var position5 = new Position(0, 2, Status.FreeToCapture);
            var position6 = new Position(0, 2, Status.PlayerTwo);
            var position7 = new Position(0, 2, Status.FreeToCapture);
            var position8 = new Position(0, 2, Status.PlayerTwo);


            var board = new BoardBuilder()
                .WithPositions()//Przemek uzupe³nia do 9 pozycji w Board automatycznie, dlatego wynik = 0, bo wyœle another result
                .Build();
            board.Positions[0] = position0;
            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -3));
        }
    }
}
