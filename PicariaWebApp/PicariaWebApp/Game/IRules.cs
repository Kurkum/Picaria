﻿using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Game
{
    public interface IRules
    {
        List<Position> GetPossibleMovesOfPawn(List<Position> board, Position pawnPostion);
        List<Move> GetPossibleMovesOfPlayer(List<Position> board, Status player);
    }
}