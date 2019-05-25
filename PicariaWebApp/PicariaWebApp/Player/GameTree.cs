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
        public int Rate { get; set; }
        public int MaximumDepth { get; set; }
        public int CurrentDepth { get; set; }
        public Board BoardState { get; set; }
        public Status NextPlayer { get; set; }
        public List<GameTree> Children { get; set; }


        public GameTree(Board board, Status nextPlayer, int maximumDepth, int currentDepth)
        {
            this.BoardState = board;
            this.NextPlayer = nextPlayer;
            this.MaximumDepth = maximumDepth;
            this.CurrentDepth = currentDepth;
            this.Rate = 0;
        }

        public void ChooseNewRoot(GameTree newRoot)
        {
            Rate = newRoot.Rate;
            BoardState = newRoot.BoardState;
            NextPlayer = newRoot.NextPlayer;
            Children = newRoot.Children;
            _setChildrenCurrentDepth();
        }


        public void Expand()
        {
            if (CurrentDepth == MaximumDepth)
            {
                return;
            }
            else
            {
                if (Children == null)
                {
                    List<Move> possibleMoves = BoardState.Rules.GetPossibleMovesOfPlayer(BoardState, NextPlayer);
                    Children = new List<GameTree>();
                    Status nextPlayer;
                    if (NextPlayer == Status.PlayerOne)
                    {
                        nextPlayer = Status.PlayerTwo;
                    }
                    else
                    {
                        nextPlayer = Status.PlayerOne;
                    }
                    foreach (Move move in possibleMoves)
                    {
                        Children.Add(new GameTree(BoardState.GetCopyOfBoardWithMoveExecuted(move), nextPlayer, MaximumDepth, CurrentDepth + 1));
                    }
                }
                foreach (GameTree child in Children)
                {
                    child.Expand();
                }
            }
        }

        public override bool Equals(object obj)
        {
            GameTree that = obj as GameTree;
            if (!this.BoardState.Equals(that.BoardState))
            {
                return false;
            }
            else if (that.CurrentDepth == that.MaximumDepth)
            {
                return true;
            }
            else
            {
                if (this.Children.Count != that.Children.Count)
                {
                    return false;
                }
                for (int i = 0; i < this.Children.Count; ++i)
                {
                    if (!this.Children.ElementAt(i).Equals(that.Children.ElementAt(i)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public override string ToString()
        {
            string result = this.BoardState.ToString()+";";
            if (this.Children != null)
            {
                foreach (GameTree gameTree in this.Children)
                {
                    result += gameTree.ToString();
                }
            }
            return result;
        }

        private void _setChildrenCurrentDepth()
        {
            if (Children == null)
            {
                return;
            }
            else
            {
                foreach (var e in Children)
                {
                    e.CurrentDepth = (CurrentDepth + 1);
                    _setChildrenCurrentDepth();
                }
            }
        }
    }
}
