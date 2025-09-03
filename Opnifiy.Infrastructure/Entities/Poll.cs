using System;
namespace Opinify.Domain.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPublic { get; set; } = true;
        public string? AnonymousIdentifier { get; set; }  
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
