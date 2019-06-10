using PicariaWebApp.Models;
using PicariaWebApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class TreeTest
    {
        [Fact]
        public void TestExpansion()
        {
            GameTree gameTree = new GameTree(Board.GetBasicBoard(), Status.PlayerOne, 1, 0);
            gameTree.Expand();
            GameTree expected = new GameTree(Board.GetBasicBoard(), Status.PlayerOne, 1, 0);
            expected.Children = new List<GameTree>();
            Board basicBoard = Board.GetBasicBoard();
            for(int i =0; i< basicBoard.Positions.Count; ++i)
            {
                Board board = basicBoard.Clone();
                board.Positions.ElementAt(i).Status = Status.PlayerOne;
                expected.Children.Add(new GameTree(board, Status.PlayerTwo, 1, 1));
            }
            Assert.Equal(expected, gameTree);
        }
    }
}
