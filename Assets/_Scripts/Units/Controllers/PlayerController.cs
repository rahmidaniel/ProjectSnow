using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : InputController
{
    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override bool RetrieveInteractInput()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
