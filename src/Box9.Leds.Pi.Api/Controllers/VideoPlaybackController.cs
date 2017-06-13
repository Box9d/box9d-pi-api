using System.Net;
using System.Web.Http;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Domain.Videos;
using Autofac;
using System.Collections.Generic;
using Box9.Leds.Pi.Domain.Logging;
using Box9.Leds.Pi.Domain.VideoPlayback;

namespace Box9.Leds.Pi.Api.Controllers
{
    public class VideoPlaybackController : ApiController
    {
        private readonly IVideoComponentService videoComponentService;
        private readonly ILog log;
        private readonly VideoPlayer videoPlayer;

        public VideoPlaybackController()
        {
            videoComponentService = Startup.Container.Resolve<IVideoComponentService>();
            log = Startup.Container.Resolve<ILog>();

            this.videoPlayer = new VideoPlayer();
        }

        public VideoPlaybackController(IVideoComponentService videoComponentService, ILog log)
        {
            this.videoComponentService = videoComponentService;
            this.log = log;

            this.videoPlayer = new VideoPlayer();
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
            videoPlayer.Play(request.TimeReferenceUrl, request.PlayAt);

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