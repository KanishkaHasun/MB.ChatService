using ChatService.Application.Cqrs.Chats.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ChatService.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IConnectionMultiplexer redis, IMediator mediator)
        {
            _mediator = mediator;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat(CreateChatCommand requestCommand)
        {
            var result = await _mediator.Send(requestCommand);
            return Ok(result );
        }


        [HttpGet("status/{chatId}")]
        public IActionResult GetChatStatus(Guid chatId)
        {
            return Ok();
        }
    }
}
