using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointSpawner))]
public class Interface : MonoBehaviour
{
    PointSpawner pointSpawner;

    void Start()
    {
        pointSpawner = GetComponent<PointSpawner>();
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
            Vector2 center = new Vector2(ray.origin.x, ray.origin.y);
            pointSpawner.mouseClick(center);
        }
    }
}
