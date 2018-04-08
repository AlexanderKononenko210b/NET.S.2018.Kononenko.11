using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Books.Exceptions
{
    /// <summary>
    /// Exception connected with exist instance in storage
    /// </summary>
    [Serializable]
    public class ExistInStorageException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ExistInStorageException()
        {
        }

        public ExistInStorageException(string message) : base(message)
        {
        }

        public ExistInStorageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExistInStorageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
                
        }
    }
}
