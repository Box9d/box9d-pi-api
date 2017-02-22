using System;
using System.Collections.Generic;
using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Box9.Leds.Pi.Api.ApiResults;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/[controller]")]
    public class VideoMetadataController : Controller
    {
        [HttpGet]
        public GlobalJsonResult<IEnumerable<VideoMetadataResult>> GetAll()
        {
            var result = new List<VideoMetadataResult>();

            // TODO: Implement

            return GlobalJsonResult<IEnumerable<VideoMetadataResult>>.Success(HttpStatusCode.OK, result);
        }

        [HttpPost("{id}")]
        public GlobalJsonResult<EmptyResult> Push(Guid id, [FromBody]VideoMetadataPushRequest request)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public GlobalJsonResult<EmptyResult> Delete(Guid id)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
