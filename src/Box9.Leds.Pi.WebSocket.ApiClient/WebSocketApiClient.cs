﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class WebSocketApiClient
    {
        private readonly HttpClient client;

        public WebSocketApiClient(Uri baseUri)
        {
            client = new HttpClient()
            {
                BaseAddress = baseUri
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GetFramesResult> GetFrames()
        {
            return await this.Get<GetFramesResult>("api/frames");
        }

        public async Task Load(LoadRequest request)
        {
            await this.Post("api/load", request);
        }

        public async Task<IsWebsocketConnectionOpenResult> IsWebsocketConnectionOpen()
        {
            return await this.Get<IsWebsocketConnectionOpenResult>("api/checkConnection");
        }

        public async Task Play(string timeReferenceUrl, DateTime? playAt)
        {
            var request = new PlayRequest
            {
                TimeReferenceUrl = timeReferenceUrl,
                PlayAt = playAt
            };

            await this.Post("api/play", request);
        }

        public async Task Stop()
        {
            await this.Post("api/stop", new StopRequest());
        }

        internal async Task<TResponse> Get<TResponse>(string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(result);
        }

        internal async Task Post<TRequest>(string requestUri, TRequest request, CancellationToken? cancellationToken = null)
        {
            var response = cancellationToken.HasValue 
                ? await client.PostAsJsonAsync(requestUri, request, cancellationToken.Value)
                : await client.PostAsJsonAsync(requestUri, request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
