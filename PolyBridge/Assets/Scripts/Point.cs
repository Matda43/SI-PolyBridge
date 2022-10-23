using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    float radius;

    void Start()
    {

    }

    void Update()
    {

    }

    public void setPosition(Vector2 new_position)
    {
        this.transform.position = new_position;
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
        this.transform.localScale = Vector2.one * new_radius * 2;
    }

    public float getRadius()
    {
        return this.radius;
    }
}
