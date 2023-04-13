using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color lineColor;
    [SerializeField] private List<Transform> _nodes = new List<Transform>();
    private Vector3 _currentNode;

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        _nodes = new List<Transform>();

        //Add node into list
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                _nodes.Add(pathTransforms[i]);
            }
        }

        //Draw line base on previous node and current node
        Vector3 firstNode = _nodes[0].position;
        _currentNode = _nodes[1].position;
        Gizmos.DrawLine(firstNode, _currentNode);
        Gizmos.DrawWireSphere(_currentNode, 5f);
        if (_nodes.Count > 2)
        {
            for (int i = 2; i < _nodes.Count; i++)
            {
                _currentNode = _nodes[i].position;
                Vector3 previousNode = _nodes[i - 1].position;
                Gizmos.DrawLine(previousNode, _currentNode);
                Gizmos.DrawWireSphere(_currentNode, 5f);
            }
        }
    }
}
