// DuplicateAuthorException.cs
using System;

namespace LibraryManagement.Exceptions
{
    public class DuplicateAuthorException : Exception
    {
        public DuplicateAuthorException(string message) : base(message)
        {
        }
    }
}