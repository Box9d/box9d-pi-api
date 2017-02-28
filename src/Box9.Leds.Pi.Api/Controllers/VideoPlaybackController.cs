using System;
using System.Net;
using System.Threading.Tasks;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Box9.Leds.Pi.Domain.VideoPlayback;
using Box9.Leds.Pi.Domain.Videos;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/Video/{videoId}/[controller]")]
    public class VideoPlaybackController : Controller
    {
        private readonly IVideoPlayer videoPlayer;
        private readonly IVideoComponentService videoComponentService;

        public VideoPlaybackController(IVideoComponentService videoComponentService, IVideoPlayer videoPlayer)
        {
            this.videoComponentService = videoComponentService;
            this.videoPlayer = videoPlayer;
        }

        [HttpGet]
        public GlobalJsonResult<LoadVideoPlaybackResult> Load(int videoId, [FromBody]LoadVideoPlaybackRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            var videoPlaybackToken = videoPlayer.Load(video);

            var result = new LoadVideoPlaybackResult();
            result.PopulateFrom(videoPlaybackToken);

            return GlobalJsonResult<LoadVideoPlaybackResult>.Success(HttpStatusCode.OK, result);
        }

        [HttpPost("{playbackToken}")]
        public GlobalJsonResult<EmptyResult> Play(int videoId, string playbackToken, [FromBody]PlayVideoRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            videoPlayer.PlayAsync(video, playbackToken);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [HttpDelete("{playbackToken}")]
        public GlobalJsonResult<EmptyResult> Stop(int videoId, string playbackToken)
        {
            videoPlayer.Stop(playbackToken);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
