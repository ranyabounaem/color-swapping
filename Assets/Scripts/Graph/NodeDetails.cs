using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NodeDetails
{
    [SerializeField]
    private Vector2 _pos;

    [SerializeField]
    private Color _color;

    public Vector2 GetPos() { return _pos; }
    public Color GetColor() { return _color;}
}
