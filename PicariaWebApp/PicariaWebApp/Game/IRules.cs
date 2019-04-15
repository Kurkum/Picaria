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
    }
}
