using System;
using System.Collections.Generic;
using System.Text;
using Web.Router.Attribute;

namespace UnitTest.Controller
{
    [HTTPController]
    public class TestController
    {
        //test/{id}/asd/{z}
        [Get("/test")] // == [Route(HTTPMethod.Get, "/test")]
        public string TestGet()
        {
            return "Get";
        }

        [Post("/test")] // == [Route(HTTPMethod.Post, "/test")]
        public string TestPost()
        {
            return "Post";
        }

        [Get("/test/{test_id}/post/{post_id}")] // =  [Route(HTTPMethod.Get, "/test/7/post/15")]
        public string TestPath([Path("test_id")]string test_id, [Path("post_id")]string post_id)
        {
            return $"TestID = {test_id}, PostID = {post_id}";
        }
        
    }
}
