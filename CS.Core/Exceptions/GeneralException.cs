using System;

namespace CS.Core.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string msg) : base(msg)
        {

        }
    }
}
