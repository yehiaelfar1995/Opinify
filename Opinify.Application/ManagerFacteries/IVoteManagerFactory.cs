using Azure.Core;
using Opinify.Application.Dtos.Votes;
using Opinify.Domain.Entities;
using System.Security.Claims;


namespace Opinify.Application.Managers
{
    public interface IVoteManagerFactory
    {
        Task<VoteResponse> CreateVote(VoteRequest vote, int? userId, string anonymousId,string IpAddress);        

    }
}
