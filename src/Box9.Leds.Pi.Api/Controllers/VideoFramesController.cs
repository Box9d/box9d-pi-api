﻿using System.Linq;
using System.Net;
using System.Web.Http;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.Videos;
using Autofac;

namespace Box9.Leds.Pi.Api.Controllers
{
    public class VideoFramesController : ApiController
    {
        private readonly IVideoFrameComponentService frameService;
        private readonly IVideoComponentService videoService;

        public VideoFramesController()
        {
            this.frameService = Startup.Container.Resolve<IVideoFrameComponentService>();
            this.videoService = Startup.Container.Resolve<IVideoComponentService>();
        }

        public VideoFramesController(IVideoComponentService videoService, IVideoFrameComponentService frameService)
        {
            this.videoService = videoService;
            this.frameService = frameService;
        }

        [ActionName("AppendFrames")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Append(int videoId, [FromBody]AppendFramesRequest request)
        {
            var video = videoService.GetById(videoId);
            var frames = request.AppendFrameRequests
                .Select(req => frameService.Initialize(1, req));

            video.AddFrames(frames);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }

        [ActionName("ClearFrames")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Clear(int videoId)
        {
            var video = videoService.GetById(videoId);
            video.ClearFrames();

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }
    }
}
