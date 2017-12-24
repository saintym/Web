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
        private readonly string PATTERN = @"({[^/]*})";
        private readonly string REPLACING_STRING = "([^/]*)";

        public string[] Keys { get; }
        public string RegexedPath { get; }

        public RegexPath(string routeAttribute)
        {
            Keys = StoreKeys(routeAttribute);
            RegexedPath = PathToRegex(routeAttribute);
        }

        private string[] StoreKeys(string controllerPath)
        {
            List<string> keys = new List<string>();
            int leftBraceIndex = -1;
            int rightBraceIndex = -1;

            while (true)
            {
                leftBraceIndex = controllerPath.IndexOf('{', leftBraceIndex + 1);
                rightBraceIndex = controllerPath.IndexOf('}', rightBraceIndex + 1);

                if (leftBraceIndex == -1 && rightBraceIndex == -1)
                    break;
                else if (leftBraceIndex >= rightBraceIndex)
                    throw new System.Exception("중괄호의 위치가 이상합니다.");
                else
                {
                    string key = controllerPath.Substring(leftBraceIndex + 1, rightBraceIndex - leftBraceIndex - 1);
                    keys.Add(key);
                }
            }
            
            return keys.ToArray();
        }

        private string PathToRegex(string controllerPath)
        {
            string result = Regex.Replace(controllerPath, PATTERN, REPLACING_STRING) + "$";
            return result;
        }

    }
}
