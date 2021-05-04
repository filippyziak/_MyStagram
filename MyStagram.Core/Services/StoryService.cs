using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Story;
using System.Collections.Generic;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Models.Dtos.Story;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MyStagram.Core.Data;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services
{

    public class StoryService : IStoryService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IFilesService filesService;
        private readonly IMapper mapper;

        public StoryService(IDatabase database, IReadOnlyProfileService profileService, IFilesService filesService,
                            IMapper mapper)
        {
            this.database = database;
            this.profileService = profileService;
            this.filesService = filesService;
            this.mapper = mapper;
        }

        public async Task<Story> CreateStory(IFormFile photo)
        {
            var user = await profileService.GetCurrentUser();

            var story = new Story();

            user.Stories.Add(story);

            if (!await database.Complete())
                return null;

            var uploadedPhoto = await filesService.UploadFile(photo, $"stories/{story.Id}", Path.GetExtension(photo.FileName));
            story.SetStoryUrl(uploadedPhoto.FileUrl);
            database.FileRepository.AddFile(uploadedPhoto.FileUrl, uploadedPhoto.FilePath);

            return await database.Complete() ? story : null;
        }

        public async Task<bool> DeleteStory(string storyId)
        {
            var story = (await profileService.GetCurrentUser()).Stories.FirstOrDefault(s => s.Id == storyId)
            ?? throw new EntityNotFoundException("Cannot find this story");

            database.StoryRepository.Delete(story);

            string path = $"files/stories/{story.Id}";
            filesService.DeleteDirecetory(path);
            await database.FileRepository.DeleteFileByPath(path);

            return await database.Complete();
        }

        public async Task<bool> WatchStory(string storyId)
        {
            var user = await profileService.GetCurrentUser();
            var story = await database.StoryRepository.Get(storyId) ?? throw new EntityNotFoundException("Story not found");

            if (story.UserStories.Any(us => us.UserId == user.Id) || user.Id == story.UserId)
                return true;

            var userStory = UserStory.Create(story.Id, user.Id);

            story.UserStories.Add(userStory);

            return await database.Complete();
        }


        public async Task ClearStories()
        {
            var storiesToDelete = await database.StoryRepository.GetWhere(s => s.DateExpires < DateTime.Now);

            database.StoryRepository.DeleteRange(storiesToDelete);

            foreach (var story in storiesToDelete)
            {
                string path = $"files/stories/{story.Id}";
                filesService.DeleteDirecetory(path);
                await database.FileRepository.DeleteFileByPath(path);
            }

            await database.Complete();
        }

        public async Task<IEnumerable<StoryWrapper>> CreateStoryWrappers()
        {
            var currentUser = await profileService.GetCurrentUser();

            return currentUser.Following.GroupBy(f => f.Recipient)
                                    .Where(g => g.Key.Stories.Count != 0 && g.Key.Followers.Any(f => f.SenderId == currentUser.Id && f.RecipientAccepted))
                                    .Select(g =>
                                     {
                                         var storiesResult = GetStories(g.Key.Id).Result;
                                         //  var stories = mapper.Map<List<StoryDto>>(storiesResult.Stories);
                                         //  var storyToWatch = mapper.Map<StoryDto>(storiesResult.StoryToWatch);
                                         //  stories.ForEach(s => s.IsWatched = isWatched(s, currentUser.Id));

                                         var storyWrapper = new StoryWrapper(storiesResult.StoryToWatch.UserId, storiesResult.StoryToWatch.UserName, storiesResult.StoryToWatch.UserPhotoUrl, storiesResult.Stories, storiesResult.StoryToWatch);
                                         storyWrapper.SetIsWatched(currentUser.Id);
                                         return storyWrapper;
                                     });
        }

        public async Task<GetStoriesResult> GetStories(string userId)
        {
            var user = await profileService.GetUser(userId);
            var currentUserId = (await profileService.GetCurrentUser()).Id;
            var stories = user.Stories.Where(s => s.DateExpires >= DateTime.Now)
            .OrderBy(s => s.DateExpires)
            .ToList();

            var storyToWatch = stories.FirstOrDefault(s => s.UserStories.All(us => us.UserId != currentUserId) || s.UserStories.Count == 0);
            if (storyToWatch == null)
                storyToWatch = stories.FirstOrDefault();

            var storiesToReturn = mapper.Map<List<StoryDto>>(stories);
            var storyToWatchToReturn = mapper.Map<StoryDto>(storyToWatch);
            storiesToReturn.ForEach(s => s.IsWatched = isWatched(s, currentUserId));

            return new GetStoriesResult(storiesToReturn, storyToWatchToReturn);
        }
        private static bool isWatched(StoryDto story, string currentUserId)
         => story.UserStories.Any(us => us.UserId == currentUserId);

    }
}