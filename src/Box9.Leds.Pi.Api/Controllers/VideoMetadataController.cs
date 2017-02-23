using System.Collections.Generic;
using System.Linq;
using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Box9.Leds.Pi.Api.RequestParsing;
using Box9.Leds.Pi.Domain.Videos;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/[controller]")]
    public class VideoMetadataController : Controller
    {
        private readonly IVideoComponentService videoComponentService;

        public VideoMetadataController(IVideoComponentService videoComponentService)
        {
            this.videoComponentService = videoComponentService;
        }

        [HttpGet]
        public GlobalJsonResult<IEnumerable<VideoMetadataResult>> GetAll()
        {
            var results = videoComponentService
                .GetAll()
                .Select(v =>
                {
                    var result = new VideoMetadataResult();
                    result.PopulateFrom(v);
                    return result;
                });

            return GlobalJsonResult<IEnumerable<VideoMetadataResult>>.Success(HttpStatusCode.OK, results);
        }

        [HttpPost("{id}")]
        public GlobalJsonResult<EmptyResult> New(int id, [FromBody]VideoMetadataCreateRequest request)
        {
            var video = videoComponentService.Initialize(id, request);
            videoComponentService.Save(video);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public GlobalJsonResult<EmptyResult> Put(int id, [FromBody]VideoMetadataPutRequest request)
        {
            var video = videoComponentService.GetById(id);

            PutRequest.DoThisIfValueIsNotDefault(request.FileName, v => video.SetFileName(v));
            PutRequest.DoThisIfValueIsNotDefault(request.FrameRate, v => video.SetFrameRate(v));
            PutRequest.DoThisIfValueIsNotDefault(request.TotalFrames, v => video.SetTotalFrames(v));

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public GlobalJsonResult<EmptyResult> Delete(int id)
        {
            videoComponentService.Delete(id);

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
