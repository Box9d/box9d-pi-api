using System.Net;
using System.Web.Http;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Box9.Leds.Pi.Domain.VideoPlayback;
using Box9.Leds.Pi.Domain.Videos;

namespace Box9.Leds.Pi.Api.Controllers
{
    public class VideoPlaybackController : ApiController
    {
        private readonly IVideoPlayer videoPlayer;
        private readonly IVideoComponentService videoComponentService;

        public VideoPlaybackController(IVideoComponentService videoComponentService, IVideoPlayer videoPlayer)
        {
            this.videoComponentService = videoComponentService;
            this.videoPlayer = videoPlayer;
        }

        [ActionName("Load")]
        
        [HttpGet]
        public GlobalJsonResult<LoadVideoPlaybackResult> Load(int videoId, [FromBody]LoadVideoPlaybackRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            var videoPlaybackToken = videoPlayer.Load(video);

            var result = new LoadVideoPlaybackResult();
            result.Populate(videoPlaybackToken);

            return GlobalJsonResult<LoadVideoPlaybackResult>.Success(HttpStatusCode.OK, result);
        }

        [ActionName("Play")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Play(int videoId, [FromBody]PlayVideoRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            videoPlayer.PlayAsync(video, request.PlaybackToken);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [ActionName("Stop")]
        [HttpDelete]
        public GlobalJsonResult<EmptyResult> Stop(int videoId, [FromBody]StopVideoRequest request)
        {
            videoPlayer.Stop(request.PlaybackToken);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
