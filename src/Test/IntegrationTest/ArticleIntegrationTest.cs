using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Services;
using Infrastructure.Data.Context;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Test.Fixtures;
using Test.Helpers;

namespace Test.IntegrationTest
{
    [Collection(nameof(IntegrationTestFixtureCollection))]
    public class ArticleControllerTest
    {
        private readonly IntegrationTestFixture _integrationTestFixture;
        private readonly HttpClient _client;
        private AppDbContext _context;

        public ArticleControllerTest(IntegrationTestFixture integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
            _integrationTestFixture.FillDatabaseAsync();
        }

        [Fact(DisplayName = "Should Retunr StatusCode Sucess")]
        [Trait("Category", "ArticleController Validation")]
        public async Task GET_GetAsync_ReturnOkResultWithData()
        {
            var url = "/api/Article";
            var responce = await _integrationTestFixture.Client.GetAsync(url);
            var articles = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);

            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
            Assert.NotNull(articles);
            Assert.True(articles.Data.Count > 2);
        }

        /*[Fact(DisplayName = "Should Retunr StatusCode Sucess and Empty Articles")]
        [Trait("Category", "ArticleController Validation")]
        public async Task GET_GetAsync_ReturnOkResultNoData()
        {
            var url = "/api/Article";
            var responce = await _integrationTestFixture.Client.GetAsync(url);
            var articles = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);

            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
            Assert.NotNull(articles);
            Assert.True(articles.Data.Count == 0);
        }*/

        [Fact(DisplayName = "Should Return StatusCode Sucess when find article")]
        [Trait("Category", "ArticleController Validation")]
        public async Task GET_GetByIdAsync_ReturnOkResultWhenFindArticle()
        {
            var url = "/api/Article/2";
            var responce = await _integrationTestFixture.Client.GetAsync(url);
            var articles = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ReadArticleResponseDto>>(url);

            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
            Assert.NotNull(articles);
        }

        [Fact(DisplayName = "Should Return StatusCode BadRequest when article doesn't exist")]
        [Trait("Category", "ArticleController Validation")]
        public async Task GET_GetByIdAsync_ReturnBadRequestResultWhenArticleNotFound()
        {
            var url = "/api/Article/99";
            var responce = await _integrationTestFixture.Client.GetAsync(url);

            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }

        [Fact(DisplayName = "Should Return StatusCode Sucess and create a new Article")]
        [Trait("Category", "ArticleController Validation")]
        public async Task POST_AddAsync_CreateNewArticleReturnOkResult()
        {
            var article = _integrationTestFixture.CreateArticleRequestDto();
            var url = "/api/Article";

            var responce = await _integrationTestFixture.Client.PostAsJsonAsync(url, article);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);
            Assert.Equal(HttpStatusCode.Created, responce.StatusCode);
            Assert.NotNull(data);
        }

        [Fact(DisplayName = "Should Return StatusCode Fail and create a new Article with empty title")]
        [Trait("Category", "ArticleController Validation")]
        public async Task POST_AddAsync_CreateNewArticleReturnBadRequestResult()
        {
            var article = _integrationTestFixture.CreateArticleRequestDtoEmptyTitle();
            var url = "/api/Article";

            var responce = await _integrationTestFixture.Client.PostAsJsonAsync(url, article);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);

            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
            Assert.NotNull(data);
        }

        [Fact(DisplayName = "Should Return StatusCode Ok and delete the article")]
        [Trait("Category", "ArticleController Validation")]
        public async Task DELETE_DeleteAsync_DeleteArticleReturnOkResult()
        {
            var url = "/api/Article/1";

            var responce = await _integrationTestFixture.Client.DeleteAsync(url);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>("/api/Article");

            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Data.FirstOrDefault(d => d.Id == 1) == null);
        }

        [Fact(DisplayName = "Should Return StatusCode Bad Request when Article doesn't exist")]
        [Trait("Category", "ArticleController Validation")]
        public async Task DELETE_DeleteAsync_DeleteArticleReturnBadRequestResult()
        {
            var url = "/api/Article/100";

            var responce = await _integrationTestFixture.Client.DeleteAsync(url);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>("/api/Article");

            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
            Assert.NotNull(data);
        }

        [Fact(DisplayName = "Should Return StatusCode OK when Article is updated")]
        [Trait("Category", "ArticleController Validation")]
        public async Task PUT_UpdateAsync_UpdateArticleReturnOkResult()
        {

            var article = new UpdateArticleRequestDto
            {
                Id = 1,
                Title = "New Title",
                Content = "New Content",
                Category = "New Category",
                Img = "New Img",
                Slug = "New Slug"
            };

            var url = "/api/Article";
            var responce = await _integrationTestFixture.Client.PutAsJsonAsync(url, article);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);

            Assert.Equal(HttpStatusCode.Created, responce.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(data.Data.FirstOrDefault(article => article.Id == 1).Title, article.Title);
        }

        [Fact(DisplayName = "Should Return StatusCode BadRequest when Article doesn't exist")]
        [Trait("Category", "ArticleController Validation")]
        public async Task PUT_UpdateAsync_UpdateArticleReturnBadRequestResult()
        {
            var article = new UpdateArticleRequestDto
            {
                Id = 99,
                Title = "New Title",
                Content = "New Content",
                Category = "New Category",
                Img = "New Img",
                Slug = "New Slug"
            };

            var url = "/api/Article";
            var responce = await _integrationTestFixture.Client.PutAsJsonAsync(url, article);
            var data = await _integrationTestFixture.Client.GetFromJsonAsync<ResultService<ICollection<ReadArticleResponseDto>>>(url);

            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }
    }
}
