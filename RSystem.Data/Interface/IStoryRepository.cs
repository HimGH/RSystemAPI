using RSystem.Model.RequestModel;
using RSystem.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystem.Data.Interface
{
    public interface  IStoryRepository
    {
         Task<IEnumerable<int>> GetStoryIds(PaginationParameters pagination);
        Task<IEnumerable<StoryResponseModel>> GetStories(PaginationParameters pagination);
    }
}
