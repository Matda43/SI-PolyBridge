using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Point : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float radius;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
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
