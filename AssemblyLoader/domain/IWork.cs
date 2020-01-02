using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoadExample.domain
{
    public interface IWork : IDisposable
    {
        void Execute<T>(params object[] serializables);
    }
}
