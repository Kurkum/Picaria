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

            return new JsonResult(result);
        }
    }

    public class Board
    {
        public List<RootObject> Positions { get; set; }
    }

    public class RootObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Status { get; set; }
    }
}