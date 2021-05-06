using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStagram.Core.Builders;
using MyStagram.Core.Extensions;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Result;
using System.Linq;
using MyStagram.Core.Data;
using MyStagram.Core.Services.Interfaces.ReadOnly;
using MyStagram.Core.Data.Models;

namespace MyStagram.Core.Services
{
    public class FollowersService : IFollowersService
    {
        private readonly IReadOnlyProfileService profileService;
        private readonly IDatabase database;
        public FollowersService(IReadOnlyProfileService profileService, IDatabase database)
        {
            this.profileService = profileService;
            this.database = database;
        }

        public async Task<Follower> FollowUser(string recipientId)
        {
            var user = await this.profileService.GetCurrentUser();

            if (user.Id == recipientId)
                return null;


            var follower = await GetFollower(user.Id, recipientId);

            if (follower != null)
                return null;

            var recipient = await this.profileService.GetUser(recipientId);

            if (!recipient.IsPrivate)
            {
                follower = new FollowerBuilder()
                    .SentFrom(user.Id)
                    .SentTo(recipientId)
                    .IsAccepted(true)
                    .Build();

                database.FollowerRepository.Add(follower);

                return await database.Complete() ? follower : null;
            }

            else
            {
                follower = new FollowerBuilder()
                     .SentFrom(user.Id)
                     .SentTo(recipientId)
                     .IsAccepted(false)
                     .Build();

                database.FollowerRepository.Add(follower);

                return await database.Complete() ? follower : null;
            }
        }

        public async Task<bool> UnFollowUser(string recipientId)
        {
            var user = await this.profileService.GetCurrentUser();

            if (user.Id == recipientId)
                throw new EntityNotFoundException("You can't do that");

            var follower = await GetFollower(user.Id, recipientId);

            if (follower == null)
                throw new EntityNotFoundException("Follower not found");

            database.FollowerRepository.Delete(follower);

            return await database.Complete();

        }

        public async Task<bool> Accept(string senderId, string recipientId, bool accepted)
        {
            var user = await this.profileService.GetCurrentUser();

            if (user.Id != recipientId)
                return false;
            if (senderId == recipientId)
                return false;

            var follower = await GetFollower(senderId, recipientId);

            if (accepted)
            {
                follower.IsAccepted(accepted);

                var sender = await this.profileService.GetUser(senderId);


                user.Followers.Add(follower);
                sender.Following.Add(follower);

                return await database.Complete();
            }

            database.FollowerRepository.Delete(follower);
            return await database.Complete();
        }

        public async Task<GetFollowersResult> GetFollowers(GetFollowersRequest request)
        {
            var result = await database.FollowerRepository.GetFollowers(request);

            var sender = await profileService.GetCurrentUser();

            result.Followers = await MarkAsWatched(sender.Id, request.UserId, result.Followers);

            return result;
        }

        public async Task<PagedList<Follower>> GetFollowing(GetFollowersRequest request)
        {
            IEnumerable<Follower> followers;
            followers = await database.FollowerRepository.GetWhere(f => f.RecipientId == request.UserId);

            return PagedList<Follower>.Create(followers, request.PageNumber, request.PageSize);
        }

        public async Task<Follower> GetFollower(string senderId, string recipientId)
        => await database.FollowerRepository.Find(f => (f.SenderId == senderId && f.RecipientId == recipientId));

        public async Task<int> CountUnwatchedFollows()
        {
            var user = await profileService.GetCurrentUser();
            return user.Followers.OrderByDescending(f => f.Created).TakeWhile(f => !f.IsWatched).Count();
        }

        public async Task<bool> WatchFollower(string senderId)
        {
            var recipient = await profileService.GetCurrentUser();
            var follower = await GetFollower(senderId, recipient.Id);
            follower.MarkAsWatched();
            database.FollowerRepository.Update(follower);
            return await database.Complete();
        }
        private async Task<IPagedList<Follower>> MarkAsWatched(string currentUserId, string recipientId, IPagedList<Follower> userFollowers)
        {
            if ((userFollowers.FirstOrDefault())?.RecipientId != currentUserId)
                return userFollowers;

            userFollowers.TakeWhile(f => !f.IsWatched).ToList().ForEach(f => f.MarkAsWatched());

            await database.Complete();

            return userFollowers;
        }
    }
}