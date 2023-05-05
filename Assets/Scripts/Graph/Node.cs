using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{
    [SerializeField]
    private Color _nodeColor;

    [SerializeField]
    private GameObject _nodeCircle;

    public Color GetColor()
    {
        return _nodeColor;
    }

    public void SetColor(Color color)
    {
        _nodeColor = color;
    }

    public void SetCircle(GameObject circle)
    {
        _nodeCircle = circle;
    }

    public GameObject GetCircle()
    {
        return _nodeCircle;
    }

}
