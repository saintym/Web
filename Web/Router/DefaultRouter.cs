using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Web.Router.Util;
using Web.Router.Attribute;

using System.Linq;

using RouteMap = System.Collections.Generic.Dictionary<Web.Router.HTTPMethod, Web.Router.RouteInfo>;
using System.Text;

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
                {
                    continue;
                }
                
                string url = routeAttribute.RouteURL;
                HTTPMethod method = routeAttribute.HTTPMethod;

                RouteMap mapValue = new RouteMap();
                RegexPath regexValue = new RegexPath(url);

                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                string[] pathAttributeValues = new string[regexValue.Keys.Length];
                int count = 0;

                foreach (var parameterInfo in parameterInfos)
                {
                    var pathAttribute = parameterInfo.GetCustomAttribute<PathAttribute>();
                    pathAttributeValues[count++] = pathAttribute.Path;
                }

                for(int i = 0; i < count; i++)
                {
                    if(regexValue.Keys[i] != pathAttributeValues[i])
                        throw new System.Exception("Route Path 와 Attribute 가 서로 일치하지 않습니다.");
                }


                if (RouteMap.ContainsKey(regexValue.RegexedPath))
                {
                    if (RouteMap[regexValue.RegexedPath].ContainsKey(method))
                        throw new System.Exception("이미 존재하는 Method 입니다.");
                }
                else
                {
                    RouteMap.Add(regexValue.RegexedPath, mapValue);
                }


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

                    string[] routeParams = new string[regexValue.Keys.Length];
                    string methodResult;

                    if (regexValue.Keys.Length != 0)
                    {
                        foreach (Match match in Regex.Matches(request.Url.AbsolutePath, regexValue.RegexedPath))
                        {
                            for (int i = 1; i < match.Groups.Count; i++)
                                routeParams[i - 1] = (match.Groups[i].Value);
                        }
                    }
                    else
                    {
                        methodResult = methodInfo.Invoke(controller, new object[] { }).ToString();
                        return methodResult;
                    }

                    methodResult = methodInfo.Invoke(controller, routeParams).ToString();
                    return methodResult;
                };

                RouteInfo routeInfo = new RouteInfo(method, routerCallback);
                RouteMap[regexValue.RegexedPath].Add(method, routeInfo);

            }
            
        }
        

        public string Route(HttpListenerRequest request)
        {
            var uri = request.Url;
            var url = uri.AbsolutePath;
            HTTPMethod method = request.GetHTTPMethod();

            foreach(var RouteRoad in RouteMap)
            {
                if(Regex.IsMatch(url, RouteRoad.Key))
                {
                    var routes = RouteMap[RouteRoad.Key];
                    if (routes.ContainsKey(method))
                        return RouteMap[RouteRoad.Key][method].RouterCallback(request);
                }
            }
            
            throw new NotImplementedException();
            
        }
    }
}
