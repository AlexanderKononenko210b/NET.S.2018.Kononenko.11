using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Exception connected with input format value properties
    /// </summary>
    [Serializable]
    public class BookFormatException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BookFormatException()
        {
        }

        public BookFormatException(string message) : base(message)
        {
        }

        public BookFormatException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BookFormatException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
