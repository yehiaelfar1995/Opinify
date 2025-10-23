using System;

namespace Opinify.Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int? UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? AnonymousId { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;

        public Answer Answer { get; set; } = null!;
        public User? User { get; set; }
    }

}
