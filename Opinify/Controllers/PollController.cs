using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Opinify.Application.Dtos.Polls;
using Opinify.Application.Managers;

namespace Opinify.Api.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollManager _pollManager;
        public PollController(IPollManager pollManager) 
        { 
         _pollManager = pollManager;
        
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollDto dto)
        {
            string? anonymousId = Request.Cookies["anonId"];
            if (anonymousId == null)
            {
                anonymousId = Guid.NewGuid().ToString();
                Response.Cookies.Append("anonId", anonymousId, new CookieOptions { Expires = DateTime.UtcNow.AddMonths(6) });
            }

           await _pollManager.CreatePollAsync(dto, User, anonymousId);

            return Ok(1);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetPoll()
        {
            string? anonymousId = Request.Cookies["anonId"];
            if (anonymousId == null)
            {
                anonymousId = Guid.NewGuid().ToString();
                Response.Cookies.Append("anonId", anonymousId, new CookieOptions { Expires = DateTime.UtcNow.AddMonths(6) });
            }

            var poll = await _pollManager.GetUserPollAsync(User,anonymousId);

            return Ok(poll);
        }

    }
}
