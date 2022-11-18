using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineSpawner))]
[RequireComponent(typeof(PointSpawner))]

public class Simulation : MonoBehaviour
{
    LineSpawner lineSpawner;
    PointSpawner pointSpawner;
    bool isRunning = false;

    Dictionary<Point, List<(Point, float)>> dicoPointNum;
    Dictionary<Point, Vector3> dicoPointPosition;

    public float k;
    const float mass = 2;
    const float g = 9.8f;
    Vector2 zGravity = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        lineSpawner = GetComponent<LineSpawner>();
        pointSpawner = GetComponent<PointSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            simulteForce();
        }
    }

    void simulteForce()
    {
        Vector2 fGravity = zGravity * mass * g;
        Point anchorLeft = pointSpawner.getAnchorLeft().GetComponent<Point>();
        Point anchorRight = pointSpawner.getAnchorRight().GetComponent<Point>();

        foreach (Point key in dicoPointNum.Keys)
        {
            if (key != anchorLeft && key != anchorRight)
            {
                Vector2 Fstarts = Vector2.zero;
                List<(Point, float)> list = dicoPointNum[key];
                for (int i = 0; i < list.Count; i++)
                {
                    Vector2 direction = list[i].Item1.transform.position - key.transform.position;

                    Vector3 positionS = key.transform.position;
                    Vector3 positionE = list[i].Item1.transform.position;
                    float length = Mathf.Sqrt(Mathf.Pow(positionS.x - positionE.x, 2) + Mathf.Pow(positionS.y - positionE.y, 2) + Mathf.Pow(positionS.z - positionE.z, 2));
                    Fstarts += -1 * direction.normalized * k * (list[i].Item2 - length);
                }

                Vector3 acceleration = (fGravity + Fstarts) / mass;

                Vector3 velocity = key.getVelocity() + Time.deltaTime * acceleration;

                key.transform.position = key.transform.position + Time.deltaTime * velocity;
                key.SetVelocity(velocity);

            }
        }
    }


    void calcultateLength()
    {
        dicoPointNum = new Dictionary<Point, List<(Point, float)>>();
        dicoPointPosition = new Dictionary<Point, Vector3>();
        List<GameObject> lines = lineSpawner.getLines();
        List<GameObject> points = pointSpawner.getPoints();
        for (int j = 0; j < points.Count; j++)
        {
            Point point = points[j].GetComponent<Point>();

            dicoPointPosition[point] = point.transform.position;
            
            for (int i = 0; i < lines.Count; i++)
            {
                Line l = lines[i].GetComponent<Line>();
                Point start = l.getStart();
                Point end = l.getEnd();
                if (start == point)
                {
                    Vector3 positionS = start.transform.position;
                    Vector3 positionE = end.transform.position;
                    float length = Mathf.Sqrt(Mathf.Pow(positionS.x - positionE.x, 2) + Mathf.Pow(positionS.y - positionE.y, 2) + Mathf.Pow(positionS.z - positionE.z, 2));

                    if (!dicoPointNum.ContainsKey(point))
                    {
                        dicoPointNum[point] = new List<(Point, float)>();
                    }

                    dicoPointNum[point].Add((end, length));
                }
                if (end == point)
                {
                    Vector3 positionS = start.transform.position;
                    Vector3 positionE = end.transform.position;
                    float length = Mathf.Sqrt(Mathf.Pow(positionS.x - positionE.x, 2) + Mathf.Pow(positionS.y - positionE.y, 2) + Mathf.Pow(positionS.z - positionE.z, 2));

                    if (!dicoPointNum.ContainsKey(point))
                    {
                        dicoPointNum[point] = new List<(Point, float)>();
                    }

                    dicoPointNum[point].Add((start, length));
                }
            }
        }
    }

    public void run()
    {
        if (!isRunning)
        {
            calcultateLength();
        }
        isRunning = true;
    }

    void resetPositionPoint()
    {
        foreach(Point point in dicoPointPosition.Keys)
        {
            point.transform.position = dicoPointPosition[point];
            point.SetVelocity(Vector3.zero);
        }
    }


    public void stop()
    {
        if (isRunning)
        {
            resetPositionPoint();
            isRunning = false;
        }
    }
}
