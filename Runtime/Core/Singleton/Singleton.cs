using System;

namespace DanlangA.Tool
{
    public abstract class Singleton<T> where T : class
    {
        private static readonly object LockObj = new object();
        private static volatile T _instance;
        public static bool HasInstance => _instance != null;
        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                lock (LockObj)
                {
                    if (_instance == null)
                    {
                        var created = (T)Activator.CreateInstance(typeof(T), nonPublic: true);
                        if (created is Singleton<T> singleton)
                        {
                            singleton.OnInitialized();
                        }

                        RuntimeResetRegistry.Register(ResetInstance);
                    }
                }

                return _instance;
            }
        }
        protected virtual void OnInitialized() { }
        protected virtual void OnDisposed() { }
        public static void ResetInstance()
        {
            lock (LockObj)
            {
                if (_instance == null)
                {
                    return;
                }
                if (_instance is Singleton<T> singleton)
                {
                    singleton.OnDisposed();
                }
                if (_instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _instance = null;
            }
        }
    }
}
