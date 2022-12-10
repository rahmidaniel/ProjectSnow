using UnityEngine;

namespace _Scripts.Utility
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioSource musicSource, effectSource;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Existing 'Managers' instance found, newest destroyed.");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(AudioClip clip)
        {
            effectSource.PlayOneShot(clip);
        }
    }
}