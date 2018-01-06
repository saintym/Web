﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Router.Attribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PathAttribute : System.Attribute
    { 
        public PathAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
