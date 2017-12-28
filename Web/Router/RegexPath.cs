using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace Web.Router
{
    class RegexPath
    {
        private const string mPattern = @"({[^/]*})";
        private const string mReplacingString = "([^/]*)";

        public string[] mKeys;
        public string mRegexPath;

        public RegexPath(string routeAttribute)
        {
            mKeys = StoreKeys(routeAttribute);
            mRegexPath = PathToRegex(routeAttribute);
        }

        public string[] StoreKeys(string routeAttribute)
        {
            string[] routePathes = routeAttribute.Split('/');
            Regex regex = new Regex(mPattern, RegexOptions.IgnoreCase);
            List<string> keys = new List<string>();
            foreach (string routePath in routePathes)
            {
                Match match = regex.Match(routePath);
                if(match.Value != "")
                    keys.Add(match.Value);
            }
            
            return keys.ToArray();
        }

        public string PathToRegex(string routeAttribute)
        {
            string result = Regex.Replace(routeAttribute, mPattern, mReplacingString);
            return result + "$";
        }

    }
}
