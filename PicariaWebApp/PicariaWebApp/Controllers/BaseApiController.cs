using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; set; }

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
