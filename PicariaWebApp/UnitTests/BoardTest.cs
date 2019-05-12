using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class BoardTest
    {
        [Fact]
        public void EmptyBoardEualsTest()
        {
            Assert.Equal(Board.GetBasicBoard(), Board.GetBasicBoard());
        }

        [Fact]
        public void MoveTest()
        {
            Board board = new Board();
            board.Positions.ElementAt(5).Status = Status.PlayerTwo;
            Move move = new Move(board.Positions.ElementAt(5), board.Positions.ElementAt(0));
        }
    }
}
