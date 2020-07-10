using System;

namespace CS.Core.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string msg) : base(msg)
        {

        }
    }
}