using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(PointMove))]
public class LineSpawner : MonoBehaviour
{
    public GameObject prefab;

    Grid grid;
    PointSpawner pointSpawner;
    PointMove pointMove;


    List<GameObject> lines;

    bool isLineSelected = false;

    void Start()
    {
        lines = new List<GameObject>();
        grid = GetComponent<Grid>();
        pointSpawner = GetComponent<PointSpawner>();
        pointMove = GetComponent<PointMove>();
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
                    if (!isLineSelected)
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
                                isLineSelected = false;
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
        isLineSelected = true;
        lines.Add(go);
    }
}
