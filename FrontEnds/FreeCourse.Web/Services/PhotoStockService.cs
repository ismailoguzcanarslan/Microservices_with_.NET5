using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _client;

        public PhotoStockService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> Delete(string photoUrl)
        {
            var response = await _client.DeleteAsync($"photos?photourl={photoUrl}");

            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> Upload(IFormFile photo)
        {
            if(photo == null || photo.Length <= 0)
            {
                return null;
            }

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multiPartContent = new MultipartFormDataContent();

            multiPartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFileName);

            var response = await _client.PostAsync("photos", multiPartContent);

            if (!response.IsSuccessStatusCode)
                return null;

            var res = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

            return res.Data;
        }
    }
}
