using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Models
{
    public class GameResultChecker
    {
        static public GameResult CheckGameResult(Board board)
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

        static private GameResult _checkResultHorizontally(Board board)
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

        static private GameResult _checkResultVertically(Board board)
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

        static private GameResult _checkResultDiagonally(Board board)
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
