using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Object.FindFirstObjectByType<T>();
                    if (_instance != null) return _instance;
                    
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = $"[Singleton] {typeof(T)}";
                    Debug.Log("Create instance of " + typeof(T));
                }

                return _instance;
            }
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}