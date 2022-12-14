using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointMove))]
[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(LineMove))]

public class PointSpawner : MonoBehaviour
{
    public GameObject prefab;

    [Range(2f, 4f)]
    public float radius;
    float radiusRemember;

    List<GameObject> points;

    PointMove pointMove;
    Grid grid;
    LineMove lineMove;

    GameObject parentSpawner;
    GameObject anchorLeft;
    GameObject anchorRight;

    void Start()
    {
        grid = GetComponent<Grid>();
        pointMove = GetComponent<PointMove>();
        lineMove = GetComponent<LineMove>();

        points = new List<GameObject>();

        radiusRemember = radius;

        parentSpawner = GameObject.Find("PointSpawner");

        Vector3 positionAnchorLeft = grid.getAnchorLeft();
        anchorLeft = createAPoint(positionAnchorLeft);
        Point pAnchorLeft = anchorLeft.GetComponent<Point>();
        pAnchorLeft.setType(PointType.Anchor);
        Vector3 positionAnchorRight = grid.getAnchorRight();
        anchorRight = createAPoint(positionAnchorRight);
        Point pAnchorRight = anchorRight.GetComponent<Point>();
        pAnchorRight.setType(PointType.Anchor);
    }

    void Update()
    {
        if (radiusRemember != radius)
        {
            updateRadiusPoints();
            radiusRemember = radius;
        }
    }

    public void mouseClick(Vector2 position)
    {
        bool res = pointMove.pointSelected();
        if (!res)
        {
            res = lineMove.lineSelected();
            if (!res)
            {
                Vector2 positionOnGrid = grid.getRealPosition(position);
                if (grid.isInGrid(positionOnGrid))
                {
                    GameObject goSelected = positionContainsAPoint(positionOnGrid);
                    if (goSelected != null)
                    {
                        if (goSelected != anchorLeft && goSelected != anchorRight)
                        {
                            pointMove.selecte(goSelected, true);
                        }
                        else
                        {
                            pointMove.selecte(goSelected, false);
                        }
                    }
                    else
                    {
                        //createAPoint(positionOnGrid);
                    }
                }
            }
        }
        else
        {
            GameObject goSelected = pointMove.getGOSelected();
            Point point = goSelected.GetComponent<Point>();
            GameObject go = positionContainsAnotherPoint(goSelected, point.transform.position);
            if (go == null || go == goSelected)
            {
                pointMove.unselecte();
            }
            
        }
    }

    public GameObject createAPoint(Vector3 position)
    {
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        go.transform.parent = parentSpawner.transform;
        updateRadiusPoint(go);
        points.Add(go);
        return go;
    }

    public GameObject positionContainsAPoint(Vector3 position)
    {
        foreach(GameObject go in points)
        {
            Point point = go.GetComponent<Point>();
            bool res = isSelectionOnAPoint(position, point);
            if(res)
            {
                return go;
            }
        }
        return null;
    }

    public GameObject positionContainsAnotherPoint(GameObject g, Vector3 position)
    {
        foreach (GameObject go in points)
        {
            if (go != g)
            {
                Point point = go.GetComponent<Point>();
                bool res = isSelectionOnAPoint(position, point);
                if (res)
                {
                    return go;
                }
            }
        }
        return null;
    }


    void updateRadiusPoints()
    {
        foreach (GameObject go in points)
        {
            updateRadiusPoint(go);
        }
    }

    void updateRadiusPoint(GameObject go)
    {
        go.GetComponent<Point>().setRadius(radius);
    }

    bool isSelectionOnAPoint(Vector3 position, Point point)
    {
        Vector2 difference = position - point.transform.position;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2);
        return distance < Mathf.Pow(point.getRadius(), 2);
    }

    public void remove(GameObject go)
    {
        if (go != anchorLeft && go != anchorRight)
        {
            points.Remove(go);
            Destroy(go);
        }
        pointMove.unselecte();   
    }

    public GameObject getAnchorLeft()
    {
        return this.anchorLeft;
    }

    public GameObject getAnchorRight()
    {
        return this.anchorRight;
    }

    public List<GameObject> getPoints()
    {
        return this.points;
    }
}
