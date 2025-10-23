using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Opinify.Domain.Entities;
using Opinify.Infrastructure.Repositories.Interfaces;
using Opnifiy.Infrastructure;

namespace Opinify.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly OpinifyDbContext _context;
        public AnswerRepository(OpinifyDbContext context)
        {
            _context = context;

        }
        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            var answer = await _context.Answers
        .Include(a => a.Votes)
        .FirstOrDefaultAsync(a => a.Id == id);

            return answer;
        }
    }
}
