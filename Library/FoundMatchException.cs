using System;

namespace MyLibrary
{
    public class FoundMatchException : Exception
    {
        public FoundMatchException(string message)
        : base(message)
        { }
    }
}
