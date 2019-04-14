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
        public Board BoardState { get; set; }
        public Status NextPlayer { get; set; }
        public List<GameTree> Children { get; set; }


        public GameTree(int maximumDepth, Board board, Status nextPlayer)
        {
            this.MaximumDepth = maximumDepth;
            this.BoardState = board;
            this.NextPlayer = nextPlayer;
        }

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
                    List<Move> possibleMoves = BoardState.Rules.GetPossibleMovesOfPlayer(BoardState, NextPlayer);
                    Children = new List<GameTree>();
                    Status nextPlayer;
                    if(NextPlayer == Status.PlayerOne)
                    {
                        nextPlayer = Status.PlayerTwo;
                    }
                    else
                    {
                        nextPlayer = Status.PlayerOne;
                    }
                    foreach(Move move in possibleMoves)
                    {
                        Children.Add(new GameTree(MaximumDepth, BoardState.GetCopyOfBoardWithMoveRealized(move), nextPlayer));
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
