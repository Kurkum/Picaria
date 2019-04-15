using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    public class BoardBuilder : Builder<BoardBuilder, Board>
    {
        public BoardBuilder WithPositions(params Position[] positions)
        {
            State.Positions = positions.ToList();
            return this;
        }

        public override Board Build()
        {
            var emptyBoard = Board.GetEmptyBoard();
            var missingPositions = new List<Position>();
            foreach(var position in emptyBoard)
            {
                if(!State.Positions.Contains(position))
                {
                    missingPositions.Add(position);
                }
            }
            State.Positions.AddRange(missingPositions);

            return State;
        }
    }
}
