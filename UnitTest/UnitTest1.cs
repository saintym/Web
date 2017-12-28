using System;
using System.Threading.Tasks;
using UnitTest.Controller;
using Web;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var service = new WebService(null, new[] { "http://localhost:8000/" }, new TestController());
            await service.StartServer();
        }
    }
}
