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

    GameObject parentSpawner;

    void Start()
    {
        lines = new List<GameObject>();
        grid = GetComponent<Grid>();
        pointSpawner = GetComponent<PointSpawner>();
        pointMove = GetComponent<PointMove>();
        lineMove = GetComponent<LineMove>();

        parentSpawner = GameObject.Find("LineSpawner");
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
                        GameObject go = lineMove.getGOSelected();
                        Line l = go.GetComponent<Line>();
                        Point start = l.getStart();
                        res = lineAlreadyExistant(start, p);
                        if (!res && start != p)
                        {
                            l.setEnd(p);
                            l.fixedEnd();
                            lineMove.unselecte();
                        }
                    }
                }
                else
                {
                    res = lineMove.lineSelected();
                    if (res)
                    {
                        GameObject go = lineMove.getGOSelected();
                        Line l = go.GetComponent<Line>();
                        GameObject g = pointSpawner.createAPoint(positionOnGrid);
                        Point end = g.GetComponent<Point>();
                        l.setEnd(end);
                        l.fixedEnd();
                        lineMove.unselecte();
                    }
                }
            }
        }
    }

    bool lineAlreadyExistant(Point p1, Point p2)
    {
        bool res = false;
        foreach(GameObject go in lines)
        {
            Line line = go.GetComponent<Line>();
            if((line.getStart() == p1 && line.getEnd() == p2) || (line.getStart() == p2 && line.getEnd() == p1))
            {
                res = true;
                break;
            }
        }
        return res;
    }

    void createLine(Point start)
    {
        GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = parentSpawner.transform;
        Line line = go.GetComponent<Line>();
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
