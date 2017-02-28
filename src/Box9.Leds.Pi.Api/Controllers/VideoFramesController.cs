using System.Linq;
using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.Videos;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/Video/{videoId}/[controller]")]
    public class VideoFramesController : Controller
    {
        private readonly IVideoFrameComponentService frameService;
        private readonly IVideoComponentService videoService;

        public VideoFramesController(IVideoComponentService videoService, IVideoFrameComponentService frameService)
        {
            this.videoService = videoService;
            this.frameService = frameService;
        }

        [HttpPost]
        public GlobalJsonResult<EmptyResult> Append(int videoId, [FromBody]AppendFramesRequest request)
        {
            var video = videoService.GetById(videoId);
            var frames = request.AppendFrameRequests
                .Select(req => frameService.Initialize(req.Id, req));

            video.AddFrames(frames);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }
        
        [HttpDelete]
        public GlobalJsonResult<EmptyResult> Clear(int videoId)
        {
            var video = videoService.GetById(videoId);
            video.ClearFrames();

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
