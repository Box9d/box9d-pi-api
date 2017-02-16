using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController
    {
        [HttpGet]
        public GlobalJsonResult<IEnumerable<string>> GetAll()
        {
            var result = new[] { "1", "2" };

            return GlobalJsonResult<IEnumerable<string>>.Success(System.Net.HttpStatusCode.OK, result);
        }
    }
}
