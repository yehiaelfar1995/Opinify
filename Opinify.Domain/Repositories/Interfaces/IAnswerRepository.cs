using Opinify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opinify.Infrastructure.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerByIdAsync(int id);
    }
}
