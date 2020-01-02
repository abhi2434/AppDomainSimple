using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoadExample.domain
{
    public sealed class IsolateToNewDomain<T> : IDisposable where T : MarshalByRefObject
    {
        private AppDomain _domain;
        private readonly T _value;

        public IsolateToNewDomain()
            : this("AutoGen-" + Guid.NewGuid().ToString())
        {
        }
        public IsolateToNewDomain(string domainName)
        {
            _domain = AppDomain.CreateDomain(domainName,
               null, AppDomain.CurrentDomain.SetupInformation);

            Type type = typeof(T);

            _value = (T)_domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        }

        /// <summary>
        /// Represents the object that is created inside the new AppDomain
        /// </summary>
        public T Instance
        {
            get
            {
                return _value;
            }
        }

        private void DisposeImpl()
        {
            if (_domain != null)
            {
                AppDomain.Unload(_domain);

                _domain = null;
            }
        }
        /// <summary>
        /// Call this only when you want to dispose
        /// </summary>
        public void Dispose()
        {
            this.DisposeImpl();

            GC.SuppressFinalize(this);

        }

        ~IsolateToNewDomain()
        {
            //In case the user does not dispose, we will do it. 
            this.DisposeImpl();

        }
        /// <summary>
        /// Returns the actual domain created underneath
        /// </summary>
        public AppDomain Domain
        {
            get
            {
                return this._domain;
            }
        }
    }
}
