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
            Board board = Board.GetBasicBoard();
            board.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Move move = new Move(board.Positions.ElementAt(4), board.Positions.ElementAt(0));
            board.ExecuteMove(move);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(0).Status = Status.PlayerTwo;
            Assert.Equal(expected, board);
        }

        [Fact]
        public void DropMoveTest()
        {
            Board board = Board.GetBasicBoard();
            Move move = new Move(board.Positions.ElementAt(4), Status.PlayerTwo);
            board.ExecuteMove(move);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Assert.Equal(expected, board);
        }

        [Fact]
        public void MoveInCopyTest()
        {
            Board board = Board.GetBasicBoard();
            board.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Move move = new Move(board.Positions.ElementAt(4), board.Positions.ElementAt(0));
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(0).Status = Status.PlayerTwo;
            Assert.Equal(expected, board.GetCopyOfBoardWithMoveExecuted(move));
        }

        [Fact]
        public void DropInCopyTest()
        {
            Board board = Board.GetBasicBoard();
            Move move = new Move(board.Positions.ElementAt(4), Status.PlayerTwo);
            Board expected = Board.GetBasicBoard();
            expected.Positions.ElementAt(4).Status = Status.PlayerTwo;
            Assert.Equal(expected, board.GetCopyOfBoardWithMoveExecuted(move));
        }
    }
}
