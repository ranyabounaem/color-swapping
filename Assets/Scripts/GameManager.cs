using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputHandler _inputHandler;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private UIManager _UIManager;

    [SerializeField]
    private Graph _graph;

    private void Awake()
    {
        _inputHandler.Setup();
        var __input = _inputHandler.GetInput();
        _graph.Setup(_camera, __input);
        _UIManager.Setup(__input, _graph);
    }

    private void Update()
    {
        _graph.Tick(Time.deltaTime);
    }
}
