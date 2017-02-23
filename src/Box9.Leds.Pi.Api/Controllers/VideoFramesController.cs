using System.Net;
using Box9.Leds.Pi.Api.ApiRequests;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/Video/{videoId}/[controller]")]
    public class VideoFramesController : Controller
    {
        [HttpPost]
        public GlobalJsonResult<EmptyResult> Append(int videoId, [FromBody]AppendFramesRequest request)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.Created);
        }
        
        [HttpDelete]
        public GlobalJsonResult<EmptyResult> Clear(int videoId)
        {
            // TODO: Implement

            return GlobalJsonResult<EmptyResult>.Success(HttpStatusCode.NoContent);
        }
    }
}
