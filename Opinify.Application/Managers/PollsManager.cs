using Microsoft.AspNet.Identity;
using Opinify.Application.Dtos.Polls;
using Opinify.Domain.Entities;
using Opinify.Infrastructure.Repositories;
using Opinify.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Opinify.Application.Managers
{
    public class PollsManager : IPollManager
    {
        private readonly IPollRepository _PollRepository;
       public PollsManager(IPollRepository PollRepository) 
        {
            _PollRepository = PollRepository;              
        }

        public async Task<int> CreatePollAsync(CreatePollDto poll, ClaimsPrincipal user, string? anonymousId)
        {
           
            if (poll == null) throw new ArgumentNullException("poll cannot be Null");
            if (poll.Questions.Count() < 1) throw new ArgumentNullException("Should be 1 question at least");
            var pollTobeCreated = MapPoll(poll);
            if (user.Identity?.IsAuthenticated ?? false)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? user.FindFirst("sub")?.Value
                      ?? user.FindFirst("id")?.Value;
                // fallback to "sub"

                


                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                {
                    pollTobeCreated.UserId = userId;
                }
            }
            else
            {
                
                pollTobeCreated.AnonymousIdentifier = anonymousId ?? Guid.NewGuid().ToString();
                var existingCount =
                   await _PollRepository.GetPolls(anonymousId);
                
        
                if (existingCount.Count() >= 2)
                    throw new Exception("Anonymous users can only create 2 polls per month.");
            }
           await  _PollRepository.CreatePollAsync(pollTobeCreated);
            return pollTobeCreated.Id;
        }

        public async Task<List<GetPollDto>> GetUserPollAsync(ClaimsPrincipal user, string anonymousId)
        {
            int UserId=0;
            if (user.Identity?.IsAuthenticated ?? false)
            {
             UserId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            }

            var polls =  MapToDtoList( await _PollRepository.GetAllUserPollsAsync(UserId, anonymousId));

            return polls;
        }

        private Poll MapPoll(CreatePollDto poll)     
        {
           var createdPoll = new Poll
            {
                Name = poll.Title,
                IsPublic = poll.isPublic,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Questions = poll.Questions.Select(q => new Question {
                    Description = q.QuestionText,
                    CreatedAt = DateTime.UtcNow,
                    Answers = q.Options.Select(o => new Answer
                    {
                        Description = o,
                        CreatedAt = DateTime.UtcNow

                    }).ToList()
                }).ToList()

            };

            return createdPoll;

        }
        private GetPollDto MapGetPoll(Poll poll)
        {
            return new GetPollDto
            {
                PollId = poll.Id,
                Title = poll.Name,
                isPublic = poll.IsPublic,
                CreatedAt = poll.CreatedAt,
                Questions = poll.Questions.Select(q => new QuestionDto
                {
                    QuestionId = q.Id,
                    Text = q.Description,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        AnswerId = a.Id,
                        Text = a.Description,
                        Votes=a.VoteCount
                    }).ToList()
                }).ToList()
            };
        }
        private List<GetPollDto> MapToDtoList(List<Poll> polls)
        {
            var dtoList = new List<GetPollDto>();
            foreach (var poll in polls)
            {
                dtoList.Add(MapGetPoll(poll));
            }
            return dtoList;
        }
    }
}

