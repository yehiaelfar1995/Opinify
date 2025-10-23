using Opinify.Domain.Entities;
using Opinify.Infrastructure.Repositories.Interfaces;
using Opnifiy.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Opinify.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly OpinifyDbContext _context;
        public VoteRepository(OpinifyDbContext context)
        {
            _context = context;

        }

        public async Task<Vote> CreateVoteAsync(Vote vote)
        {
            _context.Votes.Add(vote);
            _context.SaveChangesAsync();
            return vote;
        }

        public IQueryable<Vote> GetVotes()
        {
            return _context.Votes.AsQueryable();
        }
    }
}
