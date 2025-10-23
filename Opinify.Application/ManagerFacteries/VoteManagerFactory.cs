using Azure.Core;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore; // ✅ This is EF Core

using Microsoft.AspNetCore.Http;
using Opinify.Application.Dtos.Votes;
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
    public class VoteManagerFactory : IVoteManagerFactory
    {
        private readonly IPollRepository _PollRepository;
        private readonly IVoteRepository _VoteRepository;
        private readonly IAnswerRepository _AnswerRepository;
        public VoteManagerFactory(IPollRepository PollRepository, IVoteRepository VoteRepository, IAnswerRepository AnswerRepository)
        {
            _PollRepository = PollRepository;
            _VoteRepository = VoteRepository;
            _AnswerRepository = AnswerRepository;
        }

        public async Task<VoteResponse> CreateVote(VoteRequest vote, int? userId, string? anonymousId, string ipAddress)
        {
            var answer = await _AnswerRepository.GetAnswerByIdAsync(vote.AnswerId);
            if (answer == null)
            {
                return new VoteResponse { success = false, message = "Answer not found", errorCode = "AnswerNotFound" };
            }

            var alreadyVoted = await _VoteRepository.GetVotes().AnyAsync(v =>
                v.Answer.QuestionId == answer.QuestionId &&
                ((userId != null && v.UserId == userId) ||
                 (anonymousId != null && v.AnonymousId == anonymousId))
            );

            if (alreadyVoted)
            {
                return new VoteResponse { success = false, message = "You have already voted on this question", errorCode = "AlreadyVoted" };
            }

            var voteToSave = new Vote
            {
                AnswerId = vote.AnswerId,
                UserId = userId,
                AnonymousId = anonymousId,
                IpAddress = ipAddress,
                VotedAt = DateTime.UtcNow
            };

            await _VoteRepository.CreateVoteAsync(voteToSave);

            return new VoteResponse
            {
                success = true,
                message = "Vote cast successfully",
                Data = new { voteToSave.AnswerId, voteToSave.UserId, voteToSave.AnonymousId }
            };
        }

    }
}

