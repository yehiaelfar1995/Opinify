using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opinify.Application.Dtos.Polls
{
       public class GetPollDto
        {
            public int PollId { get; set; }
            public string Title { get; set; } = string.Empty;
            public bool isPublic { get; set; }
            public DateTime CreatedAt { get; set; }

            public List<QuestionDto> Questions { get; set; } = new();
        }

        public class QuestionDto
        {
            public int QuestionId { get; set; }
            public string Text { get; set; } 
            public List<AnswerDto> Answers { get; set; } = new();
        }

        public class AnswerDto
        {
            public int AnswerId { get; set; }
            public string Text { get; set; } = string.Empty;
            public int Votes { get; set; }
        }
    public class GetMyPollDto
    {
        public int PollId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool isPublic { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
