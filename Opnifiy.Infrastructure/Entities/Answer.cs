using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opinify.Domain.Entities;
namespace Opinify.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; } = null!;
        public int VoteCount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Question Question { get; set; } = null!;
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }


}
