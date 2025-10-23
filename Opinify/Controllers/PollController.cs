using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Opinify.Application.Dtos.Polls;
using Opinify.Application.Dtos.Votes;
using Opinify.Application.Managers;
using Opinify.Domain.Entities;
using System.Net;

namespace Opinify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : Controller
    {
        private readonly IPollManager _pollManager;
        private readonly IVoteManagerFactory _voteManagerFactory;
        public PollController(IPollManager pollManager,IVoteManagerFactory voteManagerFactory) 
        { 
         _pollManager = pollManager;
         _voteManagerFactory = voteManagerFactory;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollDto dto)
        {
            // Create the poll and get its anonId (from DB or generated)
            var poll = await _pollManager.CreatePollAsync(dto, User, Request.Cookies["anonId"]);
          //  var poll = await _pollManager.GetPollByIdAsync(pollId); // make sure you can fetch the one just created

            // ⚡ Delete old cookie first to ensure overwrite
            Response.Cookies.Delete("anonId");

            // ⚡ Append with exact same attributes so browser replaces it
            Response.Cookies.Append("anonId", poll.anymousId, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMonths(6),
                HttpOnly = true,         // fine (not accessible from JS)
                Secure = true,          // MUST be false for localhost (true only when you run over HTTPS in production)
                SameSite = SameSiteMode.None // must be None if you want it sent with cross-origin requests
            });


            return Ok(poll);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetPoll()
        {
                if (!Request.Cookies.TryGetValue("anonId", out var anonymousId))
            {
                // no anonId cookie, so user has no polls
                return Ok(new List<GetMyPollDto>());
            }

            var poll = await _pollManager.GetUserPollAsync(User, anonymousId);

            return Ok(poll);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPollDetails(int id)
        {
            var poll= await _pollManager.GetByIdPollAsync(id);
            if (poll == null)
            {
                return NotFound("Poll not Found");
            }

               return (Ok(poll));
        }
        [HttpPost("vote")]
        public async Task<IActionResult> Vote([FromBody] VoteRequest request)
        {
            int? userId = null;
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst("id");
                if (idClaim != null && int.TryParse(idClaim.Value, out var parsedId))
                {
                    userId = parsedId;
                }
            }



            string? anonymousId = userId == null ? request.AnonymousId : null;
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var voteResponse = await _voteManagerFactory.CreateVote(request, userId, anonymousId, ipAddress);

            // Standardized response object
            var response = new
            {
                success = voteResponse.success,
                message = voteResponse.message,
                data = voteResponse.Data // optional, if you include extra info
            };

            // Map messages to proper status codes
            return voteResponse.success
                ? Ok(response) // 200
                : voteResponse.errorCode switch
                {
                    "AnswerNotFound" => NotFound(response),     // 404
                    "AlreadyVoted" => Conflict(response),     // 409
                    _ => BadRequest(response)    // 400 fallback
                };
        }


    }
}
