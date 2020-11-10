﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MessengerApp.Blazor.Services
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient client;
        private readonly ILogger<RestClient> logger;

        public RestClient(HttpClient client, ILogger<RestClient> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async ValueTask<T> GetContent<T>(string relativeUrl)
        {
            try
            {
                return await client.GetFromJsonAsync<T>(relativeUrl);
            }
            catch (Exception exception)
            {
                logger.LogError($"Error: {exception.Message}");
                return default;
            }
        }

        public async ValueTask<T> PostContent<T>(string relativeUrl, object data)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(relativeUrl, data);

                if (httpResponseMessage.IsSuccessStatusCode)
                    return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
                else
                    return default;
            }
            catch (Exception exception)
            {
                logger.LogError($"Error: {exception.Message}");
                return default;
            }
        }
    }
}