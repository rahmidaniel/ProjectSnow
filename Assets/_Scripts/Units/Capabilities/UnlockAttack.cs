using System;
using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Units.Capabilities
{
    [RequireComponent(typeof(Collider2D))]
    public class UnlockAttack : Interactable, IPersistentData
    {
        protected override string UpdateMessage()
        {
            return ($"Press '{CurrentBinding}' to pick up axe.");
        }

        protected override void Interact()
        {
            var attack = Player.Instance.GetComponent<Attack>();
            attack.enabled = true;
            Destroy(gameObject);
        }

        public void SaveData(ref GameData data)
        {
            data.axeUnlocked = false;
        }

        public void LoadData(GameData data)
        {
            if(data.axeUnlocked) Destroy(gameObject);
        }
    }
}