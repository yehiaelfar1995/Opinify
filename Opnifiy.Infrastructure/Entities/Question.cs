using System;
namespace Opinify.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Poll Poll { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

}
