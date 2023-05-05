using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputHandler", menuName = "ScriptableObjects/InputHandler")]
public class InputHandler : ScriptableObject
{
    private InputControls _inputControls;

    public void Setup()
    {
        _inputControls = new InputControls();
        _inputControls.Enable();
    }

    public InputControls GetInput()
    {
        return _inputControls;
    }
}
