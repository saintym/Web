using GGM.Application.Service;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Web.Router;

namespace Web
{
    /// <summary>
    ///     웹 서버를 제공하는 Service입니다.
    /// </summary>

    public class WebService : IService
    {
        public WebService(IRouter router, string[] prefixes, params object[] controllers)
        {
            Router = router;
            if (Router == null)
                Router = new DefaultRouter();
            Controllers = controllers;
            foreach (var controller in controllers)
                Router.RegisterController(controller);

            HttpListener = new HttpListener();
            foreach (var prefix in prefixes)
                HttpListener.Prefixes.Add(prefix);
        }

        public Guid ID { get; set; }
        public IRouter Router { get; }
        protected object[] Controllers { get; }
        protected HttpListener HttpListener { get; }
        private bool mIsRunning = false;

        /// <summary>
        ///     WebService를 시작합니다.
        /// </summary>
        public virtual void Boot(string[] arguments)
        {
            var task = StartServer();
        }
        
        public async Task StartServer()
        {
            mIsRunning = true;
            HttpListener.Start();
            while(mIsRunning)
            {
                HttpListenerContext context = await HttpListener.GetContextAsync();
                var result = Router.Route(context.Request);

                using (var response = context.Response)
                using (var outputStream = response.OutputStream)
                using (var writer = new StreamWriter(outputStream))
                {
                    if (result != null)
                        await writer.WriteAsync(result);
                }       
            }
        }
        
        protected void Stop()
        {
            mIsRunning = false;
            HttpListener.Stop();
            HttpListener.Close();
        }
    }
}
