using System.Collections.Generic;
using System.Threading.Tasks;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Models.Helpers.Story;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyStoryService
    {
        Task<GetStoriesResult> GetStories(string userId);
        Task<IEnumerable<StoryWrapper>> CreateStoryWrappers();
    }
}