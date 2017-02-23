using System.Collections.Generic;
using System.Linq;
using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
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
        public GlobalJsonResult<EmptyResult> Push(int id, [FromBody]VideoMetadataPushRequest request)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public GlobalJsonResult<EmptyResult> Delete(int id)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
