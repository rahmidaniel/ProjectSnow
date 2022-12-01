using System;
using _Scripts.Utility;
using Scenes.Sctips.Controllers;
using TMPro;
using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class Dialogue : Interactable
    {
        [SerializeField] private string message;

        protected override string UpdateMessage()
        {
            return message;
        }

        protected override void Interact() {}

        protected override void OnPlayerEnter() { }
        protected override void OnPlayerExit() { }
    }
}