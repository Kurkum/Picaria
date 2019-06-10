using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PicariaWebApp.Models;

namespace PicariaWebApp.Controllers
{
    public class PicariaController : BaseApiController
    {
        public PicariaController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> Move([FromBody]List<Position> board, CancellationToken token)
        {
            var query = new Requests.AiMove.Query() { Board = board };
            var result = await Mediator.Send(query, token);

            var gameResult = GameResultChecker.CheckGameResult(new Board(result));

            foreach(var pos in result)
            {
                pos.TranslateToInnerSystem();
            }

            return new JsonResult(new {
                Game = result,
                GameResult = gameResult
            });
        }
    }

}