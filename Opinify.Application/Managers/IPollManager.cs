using Opinify.Application.Dtos.Polls;
using Opinify.Domain.Entities;
using System.Security.Claims;


namespace Opinify.Application.Managers
{
    public interface IPollManager
    {
        Task<List<GetMyPollDto>> GetUserPollAsync(ClaimsPrincipal user, string anonymousId);
        Task<CreatedPollDto> CreatePollAsync(CreatePollDto poll, ClaimsPrincipal user, string? anonymousId);
        Task<GetPollDto> GetByIdPollAsync(int id);


    }
}
