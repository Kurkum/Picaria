using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Player
{
    public class GameTree
    {
        public int MaximumDepth { get; set; }
        public int CurrentDepth { get; set; }
        public List<Position> BoardState { get; set; }
        public Status NextPlayer { get; set; }
        public List<GameTree> Children { get; set; }
        public IRules Rules { get; set; }


        public GameTree(int maximumDepth, List<Position> boardState, Status nextPlayer)
        {
            this.MaximumDepth = maximumDepth;
            this.BoardState = boardState;
            this.NextPlayer = nextPlayer;
        }

        //TO DO: finish implementation
        public void Expand()
        {
            if(CurrentDepth== MaximumDepth)
            {
                return;
            }
            else
            {
                if(Children == null)
                {
                    List<Move> possibleMoves = Rules.GetPossibleMovesOfPlayer(BoardState, NextPlayer);
                    Children = new List<GameTree>();
                    foreach(Move move in possibleMoves)
                    {
                        //Children.Add(new GameTree(MaximumDepth, ))
                    }
                }
                foreach (GameTree child in Children)
                {
                    child.Expand();
                }
            }
        }
    }
}
