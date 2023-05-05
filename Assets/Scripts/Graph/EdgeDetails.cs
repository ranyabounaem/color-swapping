using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EdgeDetails
{
    [SerializeField]
    private Vector2 _nodes;

    [SerializeField]
    public Vector2 GetNodes () { return _nodes; }
}
