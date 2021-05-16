using System;

namespace MyLibrary
{
    public class MaximumNumberOfBooksExcaption : Exception
    {
        public MaximumNumberOfBooksExcaption(string message)
        : base(message)
        { }
    }
}
