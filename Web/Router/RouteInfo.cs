using System.Text.RegularExpressions;
using Web.Router.Attribute;

namespace Web.Router
{
    public class RouteInfo
    {
        public RouteInfo(HTTPMethod method, Regex regex, DefaultRouter.RouterCallback routerCallback)
        {
            Method = method;
            Regex = regex;
            RouterCallback = routerCallback;
        }

        public HTTPMethod Method { get; }
        public Regex Regex { get; }
        public DefaultRouter.RouterCallback RouterCallback { get; }
    }
}
