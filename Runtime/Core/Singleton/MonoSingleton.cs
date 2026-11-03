using UnityEngine;

namespace DanlangA.Tool
{
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static bool _isShuttingDown;
        private bool _isInitialized;
        public static bool HasInstance => _instance != null && !_isShuttingDown;
        protected virtual bool IsPersistent => true;
        protected virtual bool AudoCreateIfMissing => true;
        public static T Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    return null;
                }
                if (_instance != null)
                {
                    _instance.EnsureInitialized();
                    return _instance;
                }

                _instance = FindFirstObjectByType<T>();

                if (_instance != null)
                {
                    _instance.EnsureInitialized();
                    return _instance;
                }

                var go = new GameObject($"[Singleton] {typeof(T).Name}");
                _instance = go.AddComponent<T>();

                if (!_instance.AudoCreateIfMissing)
                {
                    Debug.LogError($"[MonoSingleton] '{typeof(T).Name}' <= AudoCreateIfMissing = false");
                }

                _instance.EnsureInitialized();
                return _instance;
            }
        }
        public static bool TryGetInstance(out T instance)
        {
            if (_instance != null && !_isShuttingDown)
            {
                instance = _instance;
                return true;
            }
            instance = null;
            return false;
        }
        protected virtual void Awake()
        {
            RuntimeResetRegistry.Register(ResetStaticState);
            if (_instance == null)
            {
                _instance = (T)this;
                EnsureInitialized();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        private void EnsureInitialized()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
            RuntimeResetRegistry.Register(ResetStaticState);
            if (IsPersistent && Application.isPlaying)
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null, worldPositionStays: true);
                }

                DontDestroyOnLoad(gameObject);
            }

            OnAwake();
        }
        protected virtual void OnAwake() { }
        protected virtual void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        private static void ResetStaticState()
        {
            _instance = null;
            _isShuttingDown = false;
        }
    }
}
