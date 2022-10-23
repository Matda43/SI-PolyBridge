using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointMove))]
public class PointSpawner : MonoBehaviour
{
    public GameObject prefab;

    [Range(0.1f, 2f)]
    public float radius;
    float radiusRemember;

    List<GameObject> points;

    PointMove pointMove;

    void Start()
    {
        this.pointMove = GetComponent<PointMove>();
        this.points = new List<GameObject>();
        this.radiusRemember = this.radius;
    }

    void Update()
    {
        if (radiusRemember != radius)
        {
            updateRadiusPoints();
            radiusRemember = radius;
        }
    }

    public void mouseClick(Vector2 center)
    {
        bool res = pointMove.pointSelected();
        if (!res)
        {
            GameObject goSelected = positionContainsAPoint(center);
            if (goSelected == null)
            {
                createAPoint(center);
            }
            else
            {
                pointMove.selecte(goSelected);
            }
        }
        else
        {
            pointMove.unselecte();
        }
    }

    void createAPoint(Vector3 center)
    {
        GameObject go = Instantiate(prefab, center, Quaternion.identity);
        go.transform.parent = transform.parent;
        updateRadiusPoint(go);
        points.Add(go);
    }

    GameObject positionContainsAPoint(Vector3 center)
    {
        foreach(GameObject go in points)
        {
            Point point = go.GetComponent<Point>();
            bool res = isSelectionOnAPoint(center, point);
            if(res)
            {
                return go;
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

    bool isSelectionOnAPoint(Vector3 center, Point point)
    {
        Vector2 difference = center - point.transform.position;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2);
        return distance < Mathf.Pow(point.getRadius(), 2);
    }
}
