using PicariaWebApp.Models;
using PicariaWebApp.Player;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class SimpleAITest
    {
        [Fact]
        public void TestGetBoardWithDecisonExecuted()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(1, 0).Status = Status.PlayerOne;


            board.PositionAt(2, 1).Status = Status.PlayerTwo;
            board.PositionAt(2, 2).Status = Status.PlayerTwo;

            Board expected = board.Clone();
            expected.PositionAt(2, 0).Status = Status.PlayerOne;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerOne);

            Assert.Equal(expected, intelligence.GetBoardWithDecisonExecuted(board));
        }

        [Fact]
        public void TestGetBoardWithDecisonExecutedBeginning()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(1, 1).Status = Status.PlayerOne;

            Board expected = board.Clone();
            expected.PositionAt(2, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerTwo);

            Assert.Equal(expected, intelligence.GetBoardWithDecisonExecuted(board));
        }

        
    }
}
