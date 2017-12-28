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

        [Get("/test/{test_id}/post/{post_id}")]
        public string TestPath(string test_id, string post_id)
        {
            return $"test id is {test_id}, post id is {post_id}";
        }
    }
}
