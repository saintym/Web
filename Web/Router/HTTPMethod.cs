using System.Collections.Generic;

namespace Web.Router
{
    public enum HTTPMethod
    {
        Get, Post, Put, Delete
    }

    public class HTTPMethodComparer : IEqualityComparer<HTTPMethod>
    {
        // 딕셔너리는 값 넣고 빼고 할때 call Equals and gethashcode. 근데 이넘형은 기본적으로 Equals이랑 GetHashCode 이 없어서 저거 호출하면 Object의 저것들을 호출함 그 과정에서 박싱이 일어남 기억함? ddd이제 다 기억나지? 그래서
        // 너가 따로 저거 만들고 해줬단거 말씀하시는거죠?ㅇㅇㅇ ㅇㅋㅇㅋㅇㅋㅇㅋ인트형이나 기본 자료형들은 구조체 단계에서 지원을 해주거든 봐봐 보이지 ? ddddddddd ㅃㅂ qqㅇ
        public bool Equals(HTTPMethod x, HTTPMethod y)
        {
            return (int)x == (int)y;
        }

        public int GetHashCode(HTTPMethod obj)
        {
            return ((int)obj).GetHashCode();
        }
    }
}
