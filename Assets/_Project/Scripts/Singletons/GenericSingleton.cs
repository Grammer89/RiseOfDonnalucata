using UnityEngine;

//public class ScreenFader : MonoBehaviour
public abstract class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    protected static bool _isApplicationQuitting;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (_instance == null && !_isApplicationQuitting)
                {
                    // questo primo passaggio lo carica in memoria
                    string resourcePath = $"Singletons/{typeof(T)}";
                    Debug.Log($"Trying to load: {resourcePath}");
                    T prefab = Resources.Load<T>(resourcePath);
                    // questo secondo passaggio lo istanzia in scena
                    _instance = Instantiate(prefab);
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            Debug.Log($"{gameObject.name} registered as instance for {nameof(T)}");
            _instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.Log($"{gameObject.name} tried to register as instance for {nameof(T)} but there is already {_instance.gameObject.name}");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }
    
}