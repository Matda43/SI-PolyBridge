using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(PointMove))]
[RequireComponent(typeof(LineMove))]
public class LineSpawner : MonoBehaviour
{
    public GameObject prefab;

    Grid grid;
    PointSpawner pointSpawner;
    PointMove pointMove;
    LineMove lineMove;

    List<GameObject> lines;

    void Start()
    {
        lines = new List<GameObject>();
        grid = GetComponent<Grid>();
        pointSpawner = GetComponent<PointSpawner>();
        pointMove = GetComponent<PointMove>();
        lineMove = GetComponent<LineMove>();
    }

    void Update()
    {

    }

    public void mouseClick(Vector2 position)
    {
        bool res = pointMove.pointSelected();
        if (!res)
        {
            Vector2 positionOnGrid = grid.getRealPosition(position);
            if (grid.isInGrid(positionOnGrid))
            {
                GameObject goSelected = pointSpawner.positionContainsAPoint(positionOnGrid);
                if (goSelected != null)
                {
                    Point p = goSelected.GetComponent<Point>();
                    res = lineMove.lineSelected();
                    if (!res)
                    {
                        createLine(p);
                    }
                    else
                    {
                        foreach(GameObject go in lines)
                        {
                            Line l = go.GetComponent<Line>();
                            if(!l.getEnd())
                            {
                                l.setEnd(p);
                                l.fixedEnd();
                                lineMove.unselecte();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    void createLine(Point start)
    {
        GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Line line = go.GetComponent<Line>();
        line.setGrid(grid);
        line.setStart(start);
        lineMove.selecte(go);
        lines.Add(go);
    }

    public void removeFromPoint(GameObject goPoint)
    {
        Point p = goPoint.GetComponent<Point>();
        if (p != null)
        {
            List<GameObject> gos = new List<GameObject>();
            foreach (GameObject go in lines)
            {
                Line l = go.GetComponent<Line>();
                if (l.getStart().Equals(p) || l.getEnd().Equals(p))
                {
                    gos.Add(go);
                }
            }
            foreach (GameObject go in gos)
            {
                lines.Remove(go);
                Destroy(go);
            }
        }
    }

    public void remove(GameObject go)
    {
        lines.Remove(go);
        Destroy(go);
        lineMove.unselecte();
    }
}
