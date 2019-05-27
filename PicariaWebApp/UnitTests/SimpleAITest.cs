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
            //Assert.Equal(expected, intelligence.GetBoardWithDecisonExecuted(board));

        }

        [Fact]
        public void TestHorizontalCheckResult()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(1, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerOne);

            Assert.Equal(GameResult.PlayerOneWon, intelligence.CheckGameResult(board));
        }

        [Fact]
        public void TestVerticalCheckResult()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            board.PositionAt(1, 0).Status = Status.PlayerTwo;
            board.PositionAt(1, 1).Status = Status.PlayerTwo;
            board.PositionAt(1, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerOne);

            Assert.Equal(GameResult.PlayerTwoWon, intelligence.CheckGameResult(board));
        }

        [Fact]
        public void TestIfIWin1()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            board.PositionAt(1, 0).Status = Status.PlayerTwo;
            board.PositionAt(1, 1).Status = Status.PlayerTwo;
            board.PositionAt(1, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerTwo);

            Assert.True(intelligence.IfIWin(intelligence.CheckGameResult(board)));
        }

        [Fact]
        public void TestIfIWin2()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            board.PositionAt(1, 0).Status = Status.PlayerTwo;
            board.PositionAt(1, 1).Status = Status.PlayerTwo;
            board.PositionAt(1, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerOne);

            Assert.False(intelligence.IfIWin(intelligence.CheckGameResult(board)));
        }

        [Fact]
        public void TestIfILoose1()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            board.PositionAt(1, 0).Status = Status.PlayerTwo;
            board.PositionAt(1, 1).Status = Status.PlayerTwo;
            board.PositionAt(1, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerTwo);

            Assert.False(intelligence.IfILoose(intelligence.CheckGameResult(board)));
        }

        [Fact]
        public void TestIfILoose2()
        {
            Board board = Board.GetBasicBoard();
            board.PositionAt(0, 0).Status = Status.PlayerOne;
            board.PositionAt(2, 0).Status = Status.PlayerOne;

            board.PositionAt(1, 0).Status = Status.PlayerTwo;
            board.PositionAt(1, 1).Status = Status.PlayerTwo;
            board.PositionAt(1, 2).Status = Status.PlayerTwo;

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerOne);

            Assert.True(intelligence.IfILoose(intelligence.CheckGameResult(board)));
        }
    }
}
