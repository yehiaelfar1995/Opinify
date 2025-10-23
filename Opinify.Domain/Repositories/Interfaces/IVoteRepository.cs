using Opinify.Domain.Entities;

namespace Opinify.Infrastructure.Repositories.Interfaces
{
    public interface IVoteRepository
    {
        Task<Vote> CreateVoteAsync(Vote poll);
        IQueryable<Vote> GetVotes();
    }
}
