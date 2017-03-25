using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Box9.Leds.Pi.Api.RequestParsing;
using Box9.Leds.Pi.Domain.Videos;

namespace Box9.Leds.Pi.Api.Controllers
{
    public class VideoMetadataController : ApiController
    {
        private readonly IVideoComponentService videoComponentService;

        public VideoMetadataController(IVideoComponentService videoComponentService)
        {
            this.videoComponentService = videoComponentService;
        }

        [ActionName("GetVideos")]
        [HttpGet]
        public GlobalJsonResult<IEnumerable<VideoMetadataResult>> GetAll()
        {
            var results = videoComponentService
                .GetAll()
                .Select(v =>
                {
                    var result = new VideoMetadataResult();
                    result.Populate(v);
                    return result;
                });

            return GlobalJsonResult<IEnumerable<VideoMetadataResult>>.Success(HttpStatusCode.OK, results);
        }

        [ActionName("NewVideo")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> New([FromBody]VideoMetadataCreateRequest request)
        {
            var video = videoComponentService.Initialize(request.Id, request);
            videoComponentService.Save(video);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }

        [ActionName("UpdateVideo")]
        [HttpPut]
        public GlobalJsonResult<EmptyResult> Put([FromBody]VideoMetadataPutRequest request)
        {
            var video = videoComponentService.GetById(request.Id);

            PutRequest.DoThisIfValueIsNotDefault(request.FileName, v => video.SetFileName(v));
            PutRequest.DoThisIfValueIsNotDefault(request.FrameRate, v => video.SetFrameRate(v));

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [ActionName("DeleteVideo")]
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Delete(int id)
        {
            videoComponentService.Delete(id);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
