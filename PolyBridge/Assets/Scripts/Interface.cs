using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(LineSpawner))]
public class Interface : MonoBehaviour
{
    PointSpawner pointSpawner;
    LineSpawner lineSpawner;

    void Start()
    {
        pointSpawner = GetComponent<PointSpawner>();
        lineSpawner = GetComponent<LineSpawner>();
    }

    void Update()
    {
        checkMouseClick();
    }

    void checkMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            pointSpawner.mouseClick(position);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            lineSpawner.mouseClick(position);
        }
    }
}
