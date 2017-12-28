using System.Text.RegularExpressions;
using Web.Router.Attribute;

namespace Web.Router
{
    public class RouteInfo
    {
        public RouteInfo(HTTPMethod method,DefaultRouter.RouterCallback routerCallback)
        {
            Method = method;
            RouterCallback = routerCallback;
        }

        public HTTPMethod Method { get; }
        public DefaultRouter.RouterCallback RouterCallback { get; }
    }
}
