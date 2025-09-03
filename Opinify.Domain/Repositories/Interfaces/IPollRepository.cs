using Opinify.Domain.Entities;
namespace Opinify.Infrastructure.Repositories.Interfaces
{
    public interface IPollRepository
    {
        Task<Poll> GetPollAsync(int id);
        Task CreatePollAsync(Poll poll);
        Task SaveChangesAsync();
    }
}
