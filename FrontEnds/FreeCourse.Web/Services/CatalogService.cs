using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {

        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.Upload(courseCreateInput.PhotoFormFile);

            if(resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }

            var response = await _client.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _client.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CourseViewModel>> GetAllByUserAsync(string userId)
        {
            var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

                result.Data.ForEach (x =>
                {
                    x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
                }) ;

                return result.Data;
            }

            return null;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var response = await _client.GetAsync("categories");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

                return result.Data;
            }

            return null;
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
        {
            var response = await _client.GetAsync($"courses/{courseId}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
                
                result.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(result.Data.Picture);

                return result.Data;
            
            }

            return null;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            var response = await _client.GetAsync("courses");

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

                result.Data.ForEach(x =>
                {
                    x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
                });

                return result.Data;
            }

            return null;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.Upload(courseUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                await _photoStockService.Delete(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }

            var response = await _client.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
