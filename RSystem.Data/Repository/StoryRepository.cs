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
        private int total = 0;
        public StoryRepository(HttpClient httpClient, ILogger<StoryRepository> logger) {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Get Story Id List
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
                    total = ids.Count();
                    ids = ids
                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                   .Take(pagination.PageSize)
                   .ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return ids;
        }

        /// <summary>
        /// Get Story Deatils by Id
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task GetStoryContentAsync(int item, List<StoryResponseModel> ids)
        {
            try
            {
                string apiUrl = string.Format(APIConstant.TOPSTORY_BYID_URL, item);

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    var story = JsonSerializer.Deserialize<StoryResponseModel>(results);
                    story.total = total;
                    ids.Add(story);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        /// <summary>
        /// Get Stories From API by story Id list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryResponseModel>> GetStories(PaginationParameters pagination)
        {
            try
            {
                IEnumerable<int> lst = await GetStoryIds(pagination);
                List<StoryResponseModel> ids = new List<StoryResponseModel>();

                var tasks = new List<Task>();
                foreach (var item in lst)
                    tasks.Add(Task.Run(() => GetStoryContentAsync(item, ids)));

                await Task.WhenAll(tasks);

                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    ids = ids.Where(x => x.title.Contains(pagination.Search)).ToList();
                return ids;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
