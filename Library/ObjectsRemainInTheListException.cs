using System;

namespace MyLibrary
{
    public class ObjectsRemainInTheListException : Exception
    {
        public ObjectsRemainInTheListException(string message)
        : base(message)
        { }
    }
}
