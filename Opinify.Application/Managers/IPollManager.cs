using Opinify.Application.Dtos.Polls;
using Opinify.Domain.Entities;
using System.Security.Claims;


namespace Opinify.Application.Managers
{
    public interface IPollManager
    {
        Task<List<GetPollDto>> GetUserPollAsync(ClaimsPrincipal user, string anonymousId);
        Task<int> CreatePollAsync(CreatePollDto poll, ClaimsPrincipal user, string? anonymousId);

    }
}
