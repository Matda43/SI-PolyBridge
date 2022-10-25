using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    public Color lineColor;

    LineRenderer lineRenderer;

    Point start;
    bool startSetted = false;

    Point end;
    bool endSetted = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineColor.a = 1;
        lineRenderer.material.SetColor("Line", lineColor);
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        lineRenderer.startWidth = 3f;
        lineRenderer.endWidth = 3f;
    }

    void Update()
    {
        draw();
    }

    public void setStart(Point p)
    {
        start = p;
        startSetted = true;
    }

    public void setEnd(Point p)
    {
        end = p;
    }

    public void fixedEnd()
    {
        endSetted = true;
    }

    void draw()
    {
        if (startSetted && endSetted)
        {
            Vector3 pStart = start.transform.position;
            Vector3 pEnd = end.transform.position;
            pStart.z = 1;
            pEnd.z = 1;
            Vector3[] positions = new Vector3[2] { pStart, pEnd };
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
    }

    public void draw(Vector3 v)
    {
        if (startSetted)
        {
            Vector3 pStart = start.transform.position;
            pStart.z = 1;
            v.z = 1;
            Vector3[] positions = new Vector3[2] { pStart, v };
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
    }
    public bool hasEnd()
    {
        return endSetted;
    }

    public Point getStart()
    {
        return start;
    }

    public Point getEnd()
    {
        return end;
    }
}
