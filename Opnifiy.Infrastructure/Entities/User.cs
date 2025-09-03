
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace Opinify.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Poll> Polls { get; set; } = new List<Poll>();
    }
}
