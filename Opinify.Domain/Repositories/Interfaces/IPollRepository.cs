using Opinify.Domain.Entities;
namespace Opinify.Infrastructure.Repositories.Interfaces
{
    public interface IPollRepository
    {
        Task<Poll> GetPollByIdAsync(int id);
        Task CreatePollAsync(Poll poll);
        Task<List<Poll>> GetPolls(string anonymousId);
        Task<List<Poll>> GetAllUserPollsAsync(int? userId, string anonymousId);
    }
}
