namespace Opinify.Application.Dtos.Polls
{
    public class CreatePollDto
    {
        public string Title { get; set; }
        public bool isPublic { get; set; }
        public List<CreateQuestionDto> Questions { get; set; }
    }

    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
    }

    public class VoteDto
    {
        public int OptionId { get; set; }
        public string UserId { get; set; }
    }
}