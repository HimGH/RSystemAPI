using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSystem.Data.Interface;
using RSystem.Data.Repository;
using RSystem.Model.RequestModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Moq;
using RSystem.Model.ResponseModel;
using System.Collections.Generic;
using RSystem.API.Controllers;

namespace RSystem.UnitTest
{
    [TestClass]
    public class StoryControllerTest
    {
        private readonly Mock<IStoryRepository> _repomock = new Mock<IStoryRepository>();
        private readonly StoryRepository _story;
        private readonly Mock<ILogger<StoryRepository>> _logmock = new Mock<ILogger<StoryRepository>>();
        private readonly Mock<HttpClient> _httpmock = new Mock<HttpClient>();
        public StoryControllerTest()
        {
            _story = new StoryRepository(_httpmock.Object, _logmock.Object);
        }
       
        [TestMethod]
        public async Task TestMethod1()
        {
            PaginationParameters paginationParameters = new PaginationParameters()
            {
                PageNumber = 0,
                PageSize = 5,
                Search = ""
            };
            int exp = 5;
          var resp = await _story.GetStories(paginationParameters);
            int count = resp.Count();
            Assert.AreEqual(exp, count);
        }
    }
}
