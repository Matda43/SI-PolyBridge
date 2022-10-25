using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(PointMove))]
[RequireComponent(typeof(LineSpawner))]
[RequireComponent(typeof(LineMove))]

public class Destructor : MonoBehaviour
{
    PointSpawner pointSpawner;
    PointMove pointMove;
    LineSpawner lineSpawner;
    LineMove lineMove;

    void Start()
    {
        pointSpawner = GetComponent<PointSpawner>();
        pointMove = GetComponent<PointMove>();
        lineSpawner = GetComponent<LineSpawner>();
        lineMove = GetComponent<LineMove>();
    }
    void Update()
    {
        
    }

    public void deleteSelected()
    {
        bool res = pointMove.pointSelected();
        if (res)
        {
            GameObject goSelected = pointMove.getGOSelected();
            pointSpawner.remove(goSelected);
            lineSpawner.removeFromPoint(goSelected);
        }
        else
        {
            res = lineMove.lineSelected();
            if (res)
            {
                GameObject goSelected = lineMove.getGOSelected();
                lineSpawner.remove(goSelected);
            }
        }
    }
}
