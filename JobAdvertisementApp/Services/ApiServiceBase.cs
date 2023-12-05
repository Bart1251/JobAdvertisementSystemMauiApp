﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public abstract class ApiServiceBase<T> : IApiService<T>
    {
        protected readonly HttpClient httpClient;
        protected readonly string baseAddress;
        protected readonly string url;
        protected readonly JsonSerializerOptions jsonSerialzierOptions;

        public ApiServiceBase(HttpClient httpClient, string url)
        {
            this.httpClient = httpClient;
            baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5141" : "https://localhost:7141";
            this.url = baseAddress + "/" + url;

            jsonSerialzierOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async virtual Task<T> GetAsync(string id)
        {
            T result = default(T);

            HttpResponseMessage response = await httpClient.GetAsync(url + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<T>(content, jsonSerialzierOptions);
            }

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> result = new List<T>();

            HttpResponseMessage response  = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<T>>(content, jsonSerialzierOptions);
            }

            return result;
        }

        public virtual async Task<bool> AddAsync(T item)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize<T>(item, jsonSerialzierOptions), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            if(response.IsSuccessStatusCode) return true;
            return false;
        }

        public virtual async Task<bool> UpdateAsync(string id, T item)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize<T>(item, jsonSerialzierOptions), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url + "/" + id, content);

            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(url + "/" + id);

            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
