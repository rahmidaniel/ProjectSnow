using UnityEngine;

namespace _Scripts.Utility
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }

            // Start is called before the first frame update
            private void Awake()
            {
                if (Instance != null)
                {
                    //Debug.Log("Existing 'Managers' instance found, newest destroyed.");
                    Destroy(gameObject);
                    return;
                }

                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
    }
}