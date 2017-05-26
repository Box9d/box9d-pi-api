using System.Net;
using System.Web.Http;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Box9.Leds.Pi.Domain.VideoPlayback;
using Box9.Leds.Pi.Domain.Videos;
using Autofac;
using System.Collections.Generic;
using Box9.Leds.Pi.Domain.Logging;

namespace Box9.Leds.Pi.Api.Controllers
{
    public class VideoPlaybackController : ApiController
    {
        private readonly IVideoPlayer videoPlayer;
        private readonly IVideoComponentService videoComponentService;
        private readonly ILog log;

        public VideoPlaybackController()
        {
            videoPlayer = Startup.Container.Resolve<IVideoPlayer>();
            videoComponentService = Startup.Container.Resolve<IVideoComponentService>();
            log = Startup.Container.Resolve<ILog>();
        }

        public VideoPlaybackController(IVideoComponentService videoComponentService, IVideoPlayer videoPlayer, ILog log)
        {
            this.videoComponentService = videoComponentService;
            this.videoPlayer = videoPlayer;
            this.log = log;
        }

        [ActionName("Load")]
        
        [HttpGet]
        public GlobalJsonResult<EmptyResult> Load(int videoId, [FromBody]LoadVideoPlaybackRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            videoPlayer.Load(video);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [ActionName("Play")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Play(int videoId, [FromBody]PlayVideoRequest request)
        {
            var video = videoComponentService.GetById(videoId);
            videoPlayer.Play(request.PlayAt, video);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [ActionName("Stop")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Stop(int videoId, [FromBody]StopVideoRequest request)
        {
            videoPlayer.Stop();

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [ActionName("Log")]
        [HttpGet]
        public GlobalJsonResult<IEnumerable<string>> Log()
        {
            return GlobalJsonResult<IEnumerable<string>>.Success(HttpStatusCode.OK, log.Messages);
        }
    }
}
