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
            Board statusQuo = gameTree.BoardState; //it may not be true status quo, but only when I can't win
            foreach (GameTree child in gameTree.Children)
            {
                if (IfIWin(CheckGameResult(child.BoardState)))
                {
                    return child.BoardState;
                }
                foreach (GameTree child2 in child.Children)
                {
                    if (IfILoose(CheckGameResult(child2.BoardState)))
                    {
                        goto GoFurther;
                    }
                }
                statusQuo = child.BoardState;
            GoFurther:
                continue;
            }
            return statusQuo;

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

        public GameResult CheckGameResult(Board board)
        {
            GameResult result;
            result = _checkResultHorizontally(board);
            if (result != GameResult.Other)
            {
                return result;
            }
            result = _checkResultVertically(board);
            if (result != GameResult.Other)
            {
                return result;
            }
            result = _checkResultDiagonally(board);
            if (result != GameResult.Other)
            {
                return result;
            }
            return GameResult.Other;
        }

        private GameResult _checkResultHorizontally(Board board)
        {
            Status checkedStatus;
            for (int y = 0; y < 3; ++y)
            {
                checkedStatus = board.PositionAt(0, y).Status;
                if (checkedStatus == Status.FreeToCapture)
                {
                    continue;
                }
                for (int x = 0; x < 3; ++x)
                {
                    if (checkedStatus != board.PositionAt(x, y).Status)
                    {
                        break;
                    }
                    else if (x == 2)
                    {
                        if (checkedStatus == Status.PlayerOne)
                        {
                            return GameResult.PlayerOneWon;
                        }
                        else
                        {
                            return GameResult.PlayerTwoWon;
                        }
                    }
                }
            }
            return GameResult.Other;
        }

        private GameResult _checkResultVertically(Board board)
        {
            Status checkedStatus;
            for (int x = 0; x < 3; ++x)
            {
                checkedStatus = board.PositionAt(x, 0).Status;
                if (checkedStatus == Status.FreeToCapture)
                {
                    continue;
                }
                for (int y = 0; y < 3; ++y)
                {
                    if (checkedStatus != board.PositionAt(x, y).Status)
                    {
                        break;
                    }
                    else if (y == 2)
                    {
                        if (checkedStatus == Status.PlayerOne)
                        {
                            return GameResult.PlayerOneWon;
                        }
                        else
                        {
                            return GameResult.PlayerTwoWon;
                        }
                    }
                }
            }
            return GameResult.Other;
        }

        private GameResult _checkResultDiagonally(Board board)
        {
            if (board.PositionAt(0, 0).Status == board.PositionAt(1, 1).Status &&
                board.PositionAt(1, 1).Status == board.PositionAt(2, 2).Status)
            {
                if (board.PositionAt(0, 0).Status == Status.PlayerOne)
                {
                    return GameResult.PlayerOneWon;
                }
                else if (board.PositionAt(0, 0).Status == Status.PlayerTwo)
                {
                    return GameResult.PlayerTwoWon;
                }
            }
            if (board.PositionAt(2, 0).Status == board.PositionAt(1, 1).Status &&
                board.PositionAt(1, 1).Status == board.PositionAt(0, 2).Status)
            {

                if (board.PositionAt(2, 0).Status == Status.PlayerOne)
                {
                    return GameResult.PlayerOneWon;
                }
                else if (board.PositionAt(2, 0).Status == Status.PlayerTwo)
                {
                    return GameResult.PlayerTwoWon;
                }
            }
            return GameResult.Other;
        }

    }
    public enum GameResult
    {
        PlayerOneWon, PlayerTwoWon, Other
    }
}
