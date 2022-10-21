using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject prefab;

    [Range(0.1f, 2f)]
    public float radius;
    float radiusRemember;

    List<GameObject> points;

    void Start()
    {
        this.points = new List<GameObject>();
        this.radiusRemember = this.radius;
    }

    void Update()
    {
        checkMouseClick();

        if (radiusRemember != radius)
        {
            updateRadiusPoints();
            radiusRemember = radius;
        }
    }

    void updateRadiusPoints()
    {
        foreach(GameObject go in points)
        {
            updateRadiusPoint(go);
        }
    }

    void checkMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject go = Instantiate(prefab, ray.origin, Quaternion.identity);
            go.transform.parent = transform.parent;
            updateRadiusPoint(go);
            points.Add(go);
        }
    }

    void updateRadiusPoint(GameObject go)
    {
        go.GetComponent<Point>().setRadius(radius);
    }

    /*
    bool PointInSphere(Vector2 center, Point point)
    {
        Vector2 difference = center - point.getPosition();
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2);
        return distance < Mathf.Pow(point.getRadius(), 2);
    }
    */
}
