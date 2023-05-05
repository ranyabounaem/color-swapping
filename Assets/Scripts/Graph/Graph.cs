using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelUpCallback(int level, int maxLevel);

public class Graph : MonoBehaviour
{
    [SerializeField]
    private int _currentLevel = 0;

    [SerializeField]
    private GameObject _nodePrefab;
    [SerializeField]
    private GameObject _edgePrefab;

    [SerializeField]
    private List<GraphDetails> _graphs;

    private InputControls _input;
    private List<Node> _nodes = new List<Node>();
    private List<Edge> _edges = new List<Edge>();

    private Node _selectedNode;

    private Camera _camera;

    public event LevelUpCallback OnLevelUp;

    public void Setup(Camera camera, InputControls inputControls)
    {
        _camera = camera;
        _input = inputControls;
        _input.Game.Hold.performed += _ =>
        {
            var __node = GetNodeAtMousePos();
            if (__node)
            {
                _selectedNode = __node;
                AttachCircleToCursor();
            }
        };

        _input.Game.Hold.canceled += _ =>
        {
            if (_selectedNode != null)
            {
                var __node = GetNodeAtMousePos();
                if (__node != _selectedNode && SwapNodes(__node, _selectedNode))
                {
                    // Tween node to node and update edges
                    UpdateCircles(__node, _selectedNode);
                    if (EvaluateGraph())
                    {
                        LevelUp();
                    }
                }
                else
                {
                    // Tween back to original position
                    RevertCircle(_selectedNode);
                } 
            }
            _selectedNode = null;
        };

        // Instantiate graph, nodes and edges with input graph details
        RenderCurrentGraph();
    }
    public void Tick(float deltaTime)
    {
        AttachCircleToCursor();
    }

    private void LevelUp()
    {
        _currentLevel++;
        OnLevelUp?.Invoke(_currentLevel, _graphs.Count);
    }

    private void ClearGraph()
    {
        _edges.Clear();
        _nodes.Clear();
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public bool RenderCurrentGraph()
    {
        ClearGraph();

        if (_currentLevel >= _graphs.Count)
        {
            return false;
        }

        var __nodeDetails = _graphs[_currentLevel].GetNodeDetails();

        foreach (var __nodeDetail in __nodeDetails)
        {
            var __nodePrefabInst = Instantiate(_nodePrefab, (Vector2)transform.position + __nodeDetail.GetPos(), Quaternion.identity, transform);
            var __nodeInst = __nodePrefabInst.GetComponent<Node>();
            var __nodeColor = __nodeDetail.GetColor();
            __nodeInst.SetColor(__nodeColor);
            __nodeInst.GetCircle().GetComponent<SpriteRenderer>().color = __nodeColor;
            _nodes.Add(__nodeInst);
        }

        var __edgeDetails = _graphs[_currentLevel].GetEdgeDetails();
        var __positions = new List<Vector3>();
        foreach (var __edgeDetail in __edgeDetails)
        {

            __positions.Clear();
            var __nodeIndex = (int)__edgeDetail.GetNodes().x;
            var __otherNodeIndex = (int)__edgeDetail.GetNodes().y;
            var __node = _nodes[__nodeIndex];
            var __otherNode = _nodes[__otherNodeIndex];

            var __edge = new Edge(__node, __otherNode);
            _edges.Add(__edge);

            // Draw edges
            __positions.Add(__node.transform.position);
            __positions.Add(__otherNode.transform.position);
            var __edgePrefabInst = Instantiate(_edgePrefab, transform.position, Quaternion.identity, transform);
            var __lineRenderer = __edgePrefabInst.GetComponent<LineRenderer>();
            __lineRenderer.SetPositions(__positions.ToArray());
        }

        return true;
    }


    public bool SwapNodes(Node node, Node otherNode)
    {
        foreach (var __edge in _edges)
        {
            
            if ((__edge.GetNode() == node && __edge.GetOtherNode() == otherNode) ||
            (__edge.GetNode() == otherNode && __edge.GetOtherNode() == node))
            {
                var __tempColor = node.GetColor();
                node.SetColor(otherNode.GetColor());
                otherNode.SetColor(__tempColor);
                return true;
            }
            
        }
        return false;
    }

    private Node GetNodeAtMousePos()
    {
        RaycastHit2D holdHit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_input.Game.Position.ReadValue<Vector2>()), Vector2.zero);
        if (holdHit)
        {
            return holdHit.collider.GetComponent<Node>();
        }
        else { return null; }
    }

    private void UpdateCircles(Node node, Node otherNode)
    {
        var __nodeCircle = node.GetCircle();
        var __otherNodeCircle = otherNode.GetCircle();

        __nodeCircle.transform.DOMove(otherNode.transform.position, 1).onComplete += () =>
        {
            __nodeCircle.transform.SetParent(otherNode.transform);
            otherNode.SetCircle(__nodeCircle);
        };
        __otherNodeCircle.transform.DOMove(node.transform.position, 1).onComplete += () =>
        {
            __otherNodeCircle.transform.SetParent(node.transform);
            node.SetCircle(__otherNodeCircle);
        };
    }

    private void RevertCircle(Node node)
    {
        var __nodeCircle = node.GetCircle();

        __nodeCircle.transform.DOMove(node.transform.position, 1);
    }

    private void AttachCircleToCursor()
    {
        if (_selectedNode != null)
        {
            _selectedNode.GetCircle().transform.position = (Vector2)_camera.ScreenToWorldPoint(_input.Game.Position.ReadValue<Vector2>());
        }
    }

    private bool EvaluateGraph()
    {
        foreach (var __edge in _edges)
        {
            if (__edge.GetNode().GetColor() == __edge.GetOtherNode().GetColor())
            {
                return false;
            }
        }
        Debug.Log("Win");
        return true;
    }
}

