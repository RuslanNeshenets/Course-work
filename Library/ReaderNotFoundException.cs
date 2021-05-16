using System;

namespace MyLibrary
{
    public class ReaderNotFoundException : Exception
    {
        public ReaderNotFoundException(string message)
        : base(message)
        { }
    }
}
