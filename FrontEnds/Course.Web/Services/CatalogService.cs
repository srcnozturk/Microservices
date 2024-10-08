﻿using Course.Web.Helpers;
using Course.Web.Models.Catalog;
using Course.Web.Services.Interfaces;
using Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> AddCourseAsync(CourseCreate courseCreate)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreate.PhotoFormFile);
            if (resultPhotoService != null) courseCreate.Picture = resultPhotoService.Url;

            var response = await _httpClient.PostAsJsonAsync<CourseCreate>("courses", courseCreate);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode) return null;

            var responseSuccess = response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseSuccess.Result.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode) return null;

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            //[controller]/GetAllByUserId/{userId}
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode) return null;

            var responseSuccess = response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            responseSuccess.Result.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Result.Data.Picture);
            return responseSuccess.Result.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdate courseUpdate)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdate.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdate.Picture);
                courseUpdate.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdate>("courses", courseUpdate);
            return response.IsSuccessStatusCode;
        }
    }
}
