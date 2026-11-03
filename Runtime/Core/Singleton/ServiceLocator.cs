using System;
using System.Collections;
using System.Collections.Generic;

namespace DanlangA.Tool
{
    public interface IServiceRegistry
    {
        void Register<TService>(TService service, bool overwrite = false) where TService : class;
        bool Unregister<TService>() where TService : class;
        bool Unregister<TService>(TService expectedInstance) where TService : class;
        TService Get<TService>() where TService : class;
        bool TryGet<TService>(out TService service) where TService : class;
        bool IsRegistered<TService>() where TService : class;
        void Clear();
    }

    public sealed class ServiceContainer : IServiceRegistry
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private readonly IServiceRegistry _parent;
        public ServiceContainer(IServiceRegistry parent = null)
        {
            _parent = parent;
        }
        public int Count => _services.Count;
        public void Register<TService>(TService service, bool overwrite = false) where TService : class
        {
            if (_services == null)
            {
                throw new ArgumentException(nameof(service));
            }

            // Type type
        }



        public void Clear()
        {
            throw new NotImplementedException();
        }

        public TService Get<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered<TService>() where TService : class
        {
            throw new NotImplementedException();
        }


        public bool TryGet<TService>(out TService service) where TService : class
        {
            throw new NotImplementedException();
        }

        public bool Unregister<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public bool Unregister<TService>(TService expectedInstance) where TService : class
        {
            throw new NotImplementedException();
        }
    }
}
