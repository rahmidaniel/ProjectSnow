using Unity;
using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return 1f;
        }

        public override bool RetrieveJumpInput()
        {
            return true;
        }

        public override bool RetrieveInteractInput()
        {
            throw new System.NotImplementedException();
        }
    }