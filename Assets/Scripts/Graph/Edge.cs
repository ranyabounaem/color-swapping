using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Edge
{
    [SerializeField]
    private Node _node;
    [SerializeField]
    private Node _otherNode;

    public Edge(Node node, Node otherNode)
    {
        _node = node;
        _otherNode = otherNode;
    }

    public Node GetNode()
    {
        return _node;
    }

    public Node GetOtherNode()
    {
        return _otherNode;
    }
}
