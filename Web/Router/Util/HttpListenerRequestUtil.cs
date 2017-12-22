using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Web.Router.Util
{
    public static class HttpListenerRequestUtil
    {
        public static HTTPMethod GetHTTPMethod(this HttpListenerRequest self)
        {
            switch(self.HttpMethod)
            {
                case "GET":
                    return HTTPMethod.Get;
                case "Post":
                    return HTTPMethod.Post;
                case "Put":
                    return HTTPMethod.Put;
                case "Delete":
                    return HTTPMethod.Delete;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
