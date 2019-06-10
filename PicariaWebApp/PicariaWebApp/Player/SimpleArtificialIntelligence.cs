using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Player
{
    public class SimpleArtificialIntelligence
    {
        public Status Me { get; set; } //Status of this AI player

        public SimpleArtificialIntelligence(Status me)
        {
            Me = me;
        }

        public Board GetBoardWithDecisonExecuted(Board state)
        {
            GameTree gameTree = new GameTree(state, Me, 2, 0);
            gameTree.Expand();
            Board statusQuo = gameTree.Children.ElementAt(0).BoardState; //it may not be true status quo, but only when I can't win
            Board boardOnWhichIOccupyMiddlePoint = null;
            foreach (GameTree child in gameTree.Children)
            {
                if (IfIWin(GameResultChecker.CheckGameResult(child.BoardState)))
                {
                    return child.BoardState;
                }
                foreach (GameTree child2 in child.Children)
                {
                    if (IfILoose(GameResultChecker.CheckGameResult(child2.BoardState)))
                    {
                        goto GoFurther;
                    }
                }
                if (child.BoardState.PositionAt(1,1).Status==Me)
                {
                    boardOnWhichIOccupyMiddlePoint = child.BoardState;
                }
                statusQuo = child.BoardState;
            GoFurther:
                continue;
            }
            if (boardOnWhichIOccupyMiddlePoint == null)
            {
                return statusQuo;
            }
            else
            {
                return boardOnWhichIOccupyMiddlePoint;
            }

        }

        public bool IfIWin(GameResult result)
        {
            if (Me == Status.PlayerOne && result == GameResult.PlayerOneWon)
            {
                return true;

            }
            else if (Me == Status.PlayerTwo && result == GameResult.PlayerTwoWon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IfILoose(GameResult result)
        {
            if (Me == Status.PlayerOne && result == GameResult.PlayerTwoWon)
            {
                return true;

            }
            else if (Me == Status.PlayerTwo && result == GameResult.PlayerOneWon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
