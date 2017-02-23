using System;
using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/Video/{videoId}/[controller]")]
    public class VideoPlaybackController : Controller
    {
        [HttpGet]
        public GlobalJsonResult<LoadVideoPlaybackResult> Load(int videoId, [FromBody]LoadVideoPlaybackRequest request)
        {
            var result = new LoadVideoPlaybackResult();

            // TODO: Implement

            return GlobalJsonResult<LoadVideoPlaybackResult>.Success(HttpStatusCode.OK, result);
        }

        [HttpPost("{playbackToken}")]
        public GlobalJsonResult<PlayVideoResult> Play(int videoId, Guid playbackToken, [FromBody]PlayVideoRequest request)
        {
            var result = new PlayVideoResult();

            // TODO: Implement

            return GlobalJsonResult<PlayVideoResult>.Success(HttpStatusCode.OK, result);
        }

        [HttpDelete("{playbackToken}")]
        public GlobalJsonResult<EmptyResult> Stop(int videoId, Guid playbackToken)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
