using CriptografiaAPI.Application.Operations.Command;
using CriptografiaAPI.Application.Operations.Queries;
using CriptografiaAPI.Application.Operations.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CriptografiaAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class OperationController : Controller
    {
        private readonly IMediator _mediatr;
        public OperationController(IMediator mediatr) 
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ActionResult<OperationViewModel>> AddOperation([FromBody] CreateOperationCommand req)
        {
            return await _mediatr.Send(req);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationViewModel>> GetOperationById(int id)
        {
            var req = new GetOperationByIdQuery() { Id = id };
            return await _mediatr.Send(req);
        }
    }
}
