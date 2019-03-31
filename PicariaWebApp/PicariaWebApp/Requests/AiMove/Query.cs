using MediatR;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PicariaWebApp.Requests.AiMove
{
    public class Query : IRequest<List<Position>>
    {
        public List<Position> Board { get; set; }
    }
    public class Handler : IRequestHandler<Query, List<Position>>
    {
        public async Task<List<Position>> Handle(Query request, CancellationToken cancellationToken)
        {
            request.Board.FirstOrDefault(x => x.Status == Status.FreeToCapture).Status = Status.PlayerTwo;

            return request.Board;
        }
    }
}
