using Microsoft.Extensions.Logging;
using RSystem.Common;
using RSystem.Data.Interface;
using RSystem.Model.RequestModel;
using RSystem.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSystem.Data.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StoryRepository> _logger;

        public StoryRepository(HttpClient httpClient, ILogger<StoryRepository> logger) {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetStoryIds(PaginationParameters pagination)
        {
            string apiUrl = APIConstant.TOPSTORIES_IDS_URL;
            List<int> ids = null;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ids = JsonSerializer.Deserialize<List<int>>(content);
                    ids = ids
                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                   .Take(pagination.PageSize)
                   .ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryResponseModel>> GetStories(PaginationParameters pagination)
        {
            IEnumerable<int> lst = await GetStoryIds(pagination);
            List<StoryResponseModel> ids = new List<StoryResponseModel>();
            foreach (var item in lst)
            {
                string apiUrl = string.Format(APIConstant.TOPSTORY_BYID_URL,item);
               
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var story = JsonSerializer.Deserialize<StoryResponseModel>(content);
                        ids.Add(story);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }
                }
            }
            
            return ids;
        }

    }
}
