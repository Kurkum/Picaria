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
        public void TestEmptyBoardEuals()
        {
            Assert.Equal(Board.GetBasicBoard(), Board.GetBasicBoard());
        }

        [Fact]
        public void TestMove()
        {
            Board board = Board.GetBasicBoard();
            board.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Move move = new Move(board.Positions.ElementAt(4), board.Positions.ElementAt(0));
            board.ExecuteMove(move);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(0).Status = Status.PlayerTwo;
            Assert.Equal(expected, board);
        }

        [Fact]
        public void TestDropMove()
        {
            Board board = Board.GetBasicBoard();
            Move move = new Move(board.Positions.ElementAt(4), Status.PlayerTwo);
            board.ExecuteMove(move);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Assert.Equal(expected, board);
        }

        [Fact]
        public void TestMoveInCopy()
        {
            Board board = Board.GetBasicBoard();
            board.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Move move = new Move(board.Positions.ElementAt(4), board.Positions.ElementAt(0));
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(0).Status = Status.PlayerTwo;
            Assert.Equal(expected, board.GetCopyOfBoardWithMoveExecuted(move));
        }

        [Fact]
        public void TestDropInCopy()
        {
            Board board = Board.GetBasicBoard();
            Move move = new Move(board.Positions.ElementAt(4), Status.PlayerTwo);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Assert.Equal(expected, board.GetCopyOfBoardWithMoveExecuted(move));
        }
    }
}
