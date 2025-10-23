namespace Opinify.Application.Dtos.Votes
{
    public class VoteRequest
    {
        public int AnswerId { get; set; }
        public string? AnonymousId { get; set; } // From cookie if guest
    }
    public class VoteResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string? errorCode { get; set; } // e.g., "AlreadyVoted", "AnswerNotFound"
        public object? Data { get; set; }
    }
}