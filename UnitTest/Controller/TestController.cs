using System;
using System.Collections.Generic;
using System.Text;
using Web.Router.Attribute;

namespace UnitTest.Controller
{
    [HTTPController]
    public class TestController
    {

        [Get("/test")] // == [Route(HTTPMethod.Get, "/test")]
        public string TestGet()
        {
            return "Get";
        }

        [Post("/test")] // == [Route(HTTPMethod.Get, "/test")]
        public string TestPost()
        {
            return "Post";
        }
    }
}
