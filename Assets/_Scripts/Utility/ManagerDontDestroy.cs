using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class ManagerDontDestroy : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
