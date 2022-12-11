using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class PersistentContainer : MonoBehaviour
    {
        public static PersistentContainer Instance { get; private set; }
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
