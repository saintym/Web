using System;
using System.Collections.Generic;
using System.Net;
using Web.Router.Util;

using RouteMap = System.Collections.Generic.Dictionary<Web.Router.HTTPMethod, Web.Router.RouteInfo>;

namespace Web.Router
{
    public class DefaultRouter : IRouter
    {
        public delegate string RouterCallback(HttpListenerRequest request);
        protected Dictionary<string, RouteMap> RouteMap { get; } = new Dictionary<string, RouteMap>();

        public void RegisterController(object controller)
        {
            //TODO: Implements RegisterController.
            throw new NotImplementedException();
        }

        public string Route(HttpListenerRequest request)
        {
            var uri = request.Url;
            var url = uri.AbsolutePath;
            HTTPMethod method = request.GetHTTPMethod();

            if (RouteMap.ContainsKey(url))
            {
                var routes = RouteMap[url];
                if (routes.ContainsKey(method))
                    return RouteMap[url][method].RouterCallback(request);
            }

            //TODO: Implements the regex routing.
            throw new NotImplementedException();
        }
    }
}
