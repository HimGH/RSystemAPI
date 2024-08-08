using RSystem.Model.RequestModel;
using RSystem.Model.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSystem.Data.Interface
{
    public interface  IStoryRepository
    {
         Task<IEnumerable<int>> GetStoryIds(PaginationParameters pagination);
        Task<IEnumerable<StoryResponseModel>> GetStories(PaginationParameters pagination);
    }
}
