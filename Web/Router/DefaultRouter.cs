using System;
using System.Collections.Generic;
using System.Net;
using Web.Router.Util;
using System.Text;
using System.Reflection;
using Web.Router.Attribute;
using System.Text.RegularExpressions;

using RouteMap = System.Collections.Generic.Dictionary<Web.Router.HTTPMethod, Web.Router.RouteInfo>;

namespace Web.Router
{
    public class DefaultRouter : IRouter
    {
        public delegate string RouterCallback(HttpListenerRequest request);
        protected Dictionary<string, RouteMap> RouteMap { get; } = new Dictionary<string, RouteMap>();

        public void RegisterController(object controller)
        {
            MethodInfo[] methodInfos = controller.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                RouteAttribute routeAttribute = methodInfo.GetCustomAttribute<RouteAttribute>();
                if (routeAttribute?.RouteURL == null)
                    continue;

                string url = routeAttribute.RouteURL;
                HTTPMethod method = routeAttribute.HTTPMethod;

                RouteMap mapValue = new RouteMap();

                RegexPath regexValue = new RegexPath(url);

                if (RouteMap.ContainsKey(url + "$"))
                {
                    if (RouteMap[url + "$"].ContainsKey(method))
                        throw new System.Exception("이미 존재하는 Method 입니다.");
                }
                else if (!RouteMap.ContainsKey(url + "$"))
                {
                    RouteMap.Add(regexValue.mRegexPath, mapValue);
                }
                else
                    throw new SystemException("RouteMap에 무슨 URL 을 넣은겁니까");


                RouterCallback routerCallback = (HttpListenerRequest request) =>
                {
                    /**
                    #region Query처리
                    string rawQuery = request.Url.Query;
                    StringBuilder queryResult = null;
                    if(rawQuery != "")
                    {
                        rawQuery = rawQuery.Substring(1);
                        string[] queries = rawQuery.Split('&');
                        Dictionary<string, string> QueryKeyValue = new Dictionary<string, string>(queries.Length);
                        foreach (string query in queries)
                        {
                            string[] KeyValue = query.Split('=');
                            QueryKeyValue.Add(KeyValue[0], KeyValue[1]);
                        }
                        foreach (string queryKey in QueryKeyValue.Keys)
                        {
                            queryResult.Append($"{queryKey} = {QueryKeyValue[queryKey]}\n"); // append 사용
                        }
                    }
                    queryResult.ToString();
                    #endregion
                    */
                    List<string> routeParams = new List<string>(regexValue.mKeys.Length);
                    string methodResult;

                    if (regexValue.mKeys.Length != 0)
                    {
                        foreach (Match match in Regex.Matches(request.Url.AbsolutePath, regexValue.mRegexPath))
                            for (int i = 1; i < match.Groups.Count; i++)
                                routeParams.Add(match.Groups[i].Value);
                    }
                    else if (regexValue.mKeys.Length == 0)
                    {
                        methodResult = methodInfo.Invoke(controller, new object[] { }).ToString();
                        return methodResult;// + "\n" + queryResult;
                    }
                    else
                        throw new System.Exception("RouteAttribute에서 Key 값을 잘못 추출한거같은데오?");

                    methodResult = methodInfo.Invoke(controller, routeParams.ToArray()).ToString();
                    return methodResult;// + "\n" + queryResult;
                };

                RouteInfo routeInfo = new RouteInfo(method, routerCallback);
                RouteMap[regexValue.mRegexPath].Add(method, routeInfo);

            }
        }

        public string Route(HttpListenerRequest request)
        {
            var uri = request.Url;
            var url = uri.AbsolutePath;
            HTTPMethod method = request.GetHTTPMethod();

            foreach (string requestURL in RouteMap.Keys)
            {

                if (Regex.IsMatch(url, requestURL))
                {
                    var routes = RouteMap[requestURL];
                    if (routes.ContainsKey(method))
                        return RouteMap[requestURL][method].RouterCallback(request);
                }
            }

            throw new NotImplementedException();
        }
    }
}
