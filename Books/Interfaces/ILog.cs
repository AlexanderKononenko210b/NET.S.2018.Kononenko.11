using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Interfaces
{
    /// <summary>
    /// Interface for logger framework
    /// </summary>
    public interface ILog
    {
        void WriteInfo(string info);

        void WriteError(string stackTrace);

        void WriteDebug(string stackTrace);

        void WriteWarn(string stackTrace);

        void WriteFatal(string stackTrace);
    }
}
