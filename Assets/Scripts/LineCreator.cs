using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    [SerializeField] private List<Transform> path;
    private LineRenderer lineRenderer;

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = path.Count;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = path.Count;
        lineRenderer.useWorldSpace = true;

        for (int i = 0; i < path.Count; ++i)
        {
            lineRenderer.SetPosition(i, path[i].position);
        }
    }
}
