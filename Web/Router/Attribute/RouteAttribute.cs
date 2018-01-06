﻿using System;

namespace Web.Router.Attribute
{
    /// <summary>
    ///     Route매핑을 지정하는 애트리뷰트입니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RouteAttribute : System.Attribute
    {
        public RouteAttribute(HTTPMethod httpMethod, string routeUrl)
        {
            HTTPMethod = httpMethod;
            RouteURL = routeUrl;
        }
        
        public HTTPMethod HTTPMethod { get; }
        public string RouteURL { get; }
    }

    public class GetAttribute : RouteAttribute
    {
        public GetAttribute(string routeUrl) : base(HTTPMethod.Get, routeUrl) { }
    }

    public class PostAttribute : RouteAttribute
    {
        public PostAttribute(string routeUrl) : base(HTTPMethod.Post, routeUrl) { }
    }

    public class PutAttribute : RouteAttribute
    {
        public PutAttribute(string routeUrl) : base(HTTPMethod.Put, routeUrl) { }
    }

    public class DeleteAttribute : RouteAttribute
    {
        public DeleteAttribute(string routeUrl) : base(HTTPMethod.Delete, routeUrl) { }
    }
}
