using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Game
{
    public interface IRules
    {
        List<Position> GetPossibleMovesOfPawn(Board board, Position pawnPostion);
        List<Move> GetPossibleMovesOfPlayer(Board board, Status player);
        List<Move> SecondPhaseGetPossibleMoves(Board board, Status player);//for tests
    }
}
