using System.Net;

namespace Web.Router
{
    public interface IRouter
    {
        string Route(HttpListenerRequest request);
        void RegisterController(object controller);
    }
}
