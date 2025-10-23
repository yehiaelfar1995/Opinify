using Opinify.Domain.Entities;
using Opinify.Infrastructure.Repositories.Interfaces;
using Opnifiy.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Opinify.Infrastructure.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly OpinifyDbContext _context;
        public PollRepository(OpinifyDbContext context)
        {
            _context = context;

        }
        public async Task<Poll> CreatePollAsync(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
            return poll;

        }

        public async Task<Poll?> GetPollByIdAsync(int id)
        {
            return await _context.Polls
                .Include(p => p.Questions)         // Include Questions
                .ThenInclude(q => q.Answers)       // Include Answers inside Questions
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }
        public async Task<List<Poll>> GetPolls(string anonymousId)
        {
            var thisMonth = DateTime.UtcNow.Month;
            return await _context.Polls.Where(x => x.AnonymousIdentifier == anonymousId && x.CreatedAt.Month==thisMonth).ToListAsync();
        }


        public async Task<List<Poll>> GetAllUserPollsAsync(int? userId,string anonymousId)
        {
            if (userId > 0)
            {
                return await _context.Polls.Where(x => x.UserId == userId)
                    .Include(p => p.Questions)
                    .ThenInclude(q => q.Answers)
                    .ToListAsync();
            }
            else
            {
                return await _context.Polls.Where(x => x.AnonymousIdentifier.ToLower() == anonymousId.ToLower())
                    .Include(p => p.Questions)
                    .ThenInclude(q => q.Answers)
                    .ToListAsync();
            }
        }
    }
}
