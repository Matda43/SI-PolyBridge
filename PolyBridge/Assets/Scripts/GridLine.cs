using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GridLine : MonoBehaviour
{
    public Material lineMaterial;
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
    }

    public void draw(Vector3 start, Vector3 end)
    {
        start.z = 1;
        end.z = 1;
        Vector3[] positions = new Vector3[2] { start, end };
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}
