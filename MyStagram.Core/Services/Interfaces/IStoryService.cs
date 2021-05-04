using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.Story;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IStoryService : IReadOnlyStoryService
    {
        Task ClearStories();
        Task<Story> CreateStory(IFormFile photo);
        Task<bool> DeleteStory(string storyId);
        Task<bool> WatchStory(string storyId);
    }
}