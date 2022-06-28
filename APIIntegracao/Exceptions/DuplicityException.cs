using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Exceptions
{
    public class DuplicityException : Exception
    {
        public DuplicityException() { }

        public DuplicityException(string message) : base(message) { }

        public DuplicityException(string message, Exception inner) : base(message, inner) { }
    }
}
