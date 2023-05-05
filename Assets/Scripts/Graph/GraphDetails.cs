using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GraphDetails", menuName = "ScriptableObjects/GraphDetails")]
public class GraphDetails : ScriptableObject
{
    [SerializeField]
    private GameObject _nodePrefab;

    [SerializeField]
    private List<EdgeDetails> _edgeDetails = new List<EdgeDetails>();
    [SerializeField]
    private List<NodeDetails> _nodeDetails = new List<NodeDetails>();

    public List<EdgeDetails> GetEdgeDetails () { return _edgeDetails; }
    public List<NodeDetails> GetNodeDetails () {  return _nodeDetails; }
}
