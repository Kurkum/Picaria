using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class RulesTest
    {
        [Fact]
        public void TestGetPossibleMovesOfPlayer()
        {
            Board board = Board.GetBasicBoard();
            var got = board.Rules.GetPossibleMovesOfPlayer(board, Status.PlayerOne);
            var expected = new List<Move> {new Move(new Position(0,0), Status.PlayerOne),
                new Move(new Position(1,0), Status.PlayerOne),
                new Move(new Position(2,0), Status.PlayerOne),

                new Move(new Position(0,1), Status.PlayerOne),
                new Move(new Position(1,1), Status.PlayerOne),
                new Move(new Position(2,1), Status.PlayerOne),

                new Move(new Position(0,2), Status.PlayerOne),
                new Move(new Position(1,2), Status.PlayerOne),
                new Move(new Position(2,2), Status.PlayerOne) };
            Assert.True(Move.AreListsOfMovesEqual(expected, got));
        }

        [Fact]
        public void TestGetPossibleMovesOfPlayerInSecondPhase()
        {
            
        }
    }
}
