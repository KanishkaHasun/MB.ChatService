using Microsoft.AspNetCore.Mvc;

namespace ChatService.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ChatController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateChat()
        {
            return Ok();
        }

        [HttpGet("status/{chatId}")]
        public IActionResult GetChatStatus(Guid chatId)
        {
            return Ok();
        }
    }
}
