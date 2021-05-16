using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class FailedToRegisterException : Exception
    {
        public FailedToRegisterException(string message)
        : base(message)
        { }
    }
}
