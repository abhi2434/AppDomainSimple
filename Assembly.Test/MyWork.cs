using AssemblyLoadExample.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Test
{
    public class MyWork : MarshalByRefObject, IWork
    {
        public void Dispose()
        {
            //Nothing required
        }

        public void Execute<T>(params object[] serializables)
        {
            Console.WriteLine("Hi");
        }
    }
}
