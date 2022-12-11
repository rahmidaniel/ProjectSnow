using FMODUnity;
using UnityEngine;

namespace _Scripts.Utility
{
    public class FMODEvents : MonoBehaviour
    {

        [field: Header("Wood SFX")]
        [field: SerializeField] public EventReference LogPickup { get; private set; }
        [field: SerializeField] public EventReference Damage { get; private set; }
        
        [field: Header("Player SFX")]
        [field: SerializeField] public EventReference Running { get; private set; }
        [field: SerializeField] public EventReference Jump { get; private set; }
        [field: SerializeField] public EventReference FrozenBreath { get; private set; }
        [field: SerializeField] public EventReference Hit { get; private set; }
        [field: SerializeField] public EventReference Death { get; private set; }
        
        [field: Header("Ambiance")]
        [field: SerializeField] public EventReference Ambience { get; private set; }
        
        [field: Header("Music")]
        [field: SerializeField] public EventReference Menu { get; private set; }
        [field: SerializeField] public EventReference Mountain { get; private set; }
        [field: SerializeField] public EventReference Forest { get; private set; }
        [field: SerializeField] public EventReference Night { get; private set; }
        [field: SerializeField] public EventReference HouseFireOn { get; private set; }
        
        
        public static FMODEvents Instance { get; private set; }
        
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