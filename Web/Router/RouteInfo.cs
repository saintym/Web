using System.Text.RegularExpressions;
using Web.Router.Attribute;

namespace Web.Router
{
    public class RouteInfo
    {
        public RouteInfo(HTTPMethod method, DefaultRouter.RouterCallback routerCallback)
        {
            Method = method;
            //Regex = regex;
            RouterCallback = routerCallback;
        }

        public HTTPMethod Method { get; set; }
        //public Regex Regex { get; set; }
        public DefaultRouter.RouterCallback RouterCallback { get; set; }
    }
}
