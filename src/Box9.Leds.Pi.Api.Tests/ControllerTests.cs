using System.Linq;
using System.Reflection;
using Box9.Leds.Pi.Api.Controllers;
using Xunit;

namespace Box9.Leds.Pi.Api.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void AllControllers_ReturnGlobalJsonResult()
        {
            var controllers = Metadata.GetAllControllers();

            foreach (var controller in controllers)
            {
                var methods = controller.GetTypeInfo().GetMethods()
                    .Where(m => m.DeclaringType.Namespace.Contains("Box9.Leds.Pi.Api") && m.IsPublic);

                foreach (var method in methods)
                {
                    if (method.ReturnType.GetTypeInfo().IsGenericType)
                    {
                        Assert.Equal(typeof(GlobalJsonResult<>), method.ReturnType.GetGenericTypeDefinition());
                    }
                    else
                    {
                        Assert.True(false, string.Format("Method {0} in controller {1} does not have return type GlobalJsonResult<>", method.Name, controller.Name));
                    }
                }
            }
        }
    }
}
