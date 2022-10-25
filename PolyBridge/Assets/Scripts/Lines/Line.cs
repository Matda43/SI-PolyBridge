using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    public Material lineMaterial;
    
    LineRenderer lineRenderer;
    Grid grid;
    bool gridSetted = false;

    Point start;
    bool startSetted = false;

    Point end;
    bool endSetted = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 4f;
        lineRenderer.endWidth = 4f;
    }

    void Update()
    {
        if (gridSetted && startSetted)
        {
            if (endSetted)
            {
                draw();
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
                Vector2 positionOnGrid = grid.getRealPosition(position);
                if (grid.isInGrid(positionOnGrid))
                {
                    draw(positionOnGrid);
                }
            }
        }
    }

    public void setGrid(Grid new_grid)
    {
        grid = new_grid;
        gridSetted = true;
    }

    public void setStart(Point p)
    {
        start = p;
        startSetted = true;
    }

    public void setEnd(Point p)
    {
        end = p;
        endSetted = true;
    }

    void draw()
    {
        if (startSetted && endSetted && gridSetted)
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

    void draw(Vector3 v)
    {
        if (startSetted && gridSetted)
        {
            Vector3 pStart = start.transform.position;
            pStart.z = 1;
            v.z = 1;
            Vector3[] positions = new Vector3[2] { pStart, v };
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
    }
    public bool getEnd()
    {
        return endSetted;
    }
}
