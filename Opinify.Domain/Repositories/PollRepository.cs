//using Opinify.Domain.Entities;
//using Opinify.Infrastructure.Repositories.Interfaces;
//using Opnifiy.Infrastructure;
//using Microsoft.EntityFrameworkCore;

//namespace Opinify.Infrastructure.Repositories
//{
//    public class PollRepository : IPollRepository
//    {
//        private readonly OpinifyDbContext _context;
//        public PollRepository(OpinifyDbContext context) 
//        {
//            _context = context;
        
//        }
//        public async Task CreatePollAsync(Poll poll)
//        {
//           _context.Polls.Add(poll);
//            _context.SaveChanges();

//        }

//        public Task<Poll> GetPollAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<Poll?> GetPollByIdAsync(int id)
//        {
//            return await _context.Polls
//                .Include(p => p.Questions)         // Include Questions
//                .ThenInclude(q => q.Answers)       // Include Answers inside Questions
//                .FirstOrDefaultAsync(p => p.Id == id);
//        }

//        public async Task<IEnumerable<Poll>> GetAllPollsAsync(int userId)
//        {
//            return await _context.Polls
//                .Include(p => p.Questions)
//                .ThenInclude(q => q.Answers)
//                .ToListAsync();
//        }
//    }
//}
