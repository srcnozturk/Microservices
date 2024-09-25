﻿using Course.Web.Models.PhotoStocks;
using Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoStockViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo is null || photo.Length <= 0) return null;

            var randomFileName=$"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
            using var ms=new MemoryStream();
            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFileName);

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode) return null;

            var responseSuccess= await response.Content.ReadFromJsonAsync<Response<PhotoStockViewModel>>();
            return responseSuccess.Data;
        }
    }
}