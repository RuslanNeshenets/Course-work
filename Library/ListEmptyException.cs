using System;

namespace MyLibrary
{
    public class ListEmptyException : Exception
    {
        public ListEmptyException(string message)
        : base(message)
        { }
    }
}
