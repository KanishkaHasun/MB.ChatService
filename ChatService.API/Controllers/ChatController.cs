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
        private readonly IDatabase _redisDb;
        private readonly IMediator _mediator;
        public ChatController(IConnectionMultiplexer redis, IMediator mediator)
        {
            _redisDb = redis.GetDatabase();
            _mediator = mediator;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat(CreateChatCommand requestCommand)
        {
            var result = await _mediator.Send(requestCommand);
            return Ok(result );
        }

        [MapToApiVersion("1.0")]
        [HttpPost("create2")]
        public IActionResult CreateChat1()
        {




            //var _redisDb = ConnectionMultiplexer.Connect("localhost:6379");
            //var _redisDb = this._redisDb.GetDatabase();

            // Enqueue
            //_redisDb.ListRightPush("chatQueue", "chat1");
            //_redisDb.ListRightPush("chatQueue", "chat2");
            //_redisDb.ListRightPush("chatQueue", "chat3");

            //// Dequeue
            //var chat1 = _redisDb.ListLeftPop("chatQueue");
            //var chat2 = _redisDb.ListLeftPop("chatQueue");
            //Console.WriteLine(chat1); // "chat1"
            //Console.WriteLine(chat2);






            return Ok();
        }

        [HttpGet("status/{chatId}")]
        public IActionResult GetChatStatus(Guid chatId)
        {
            return Ok();
        }
    }
}
