using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Point : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float radius;

    PointType type;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        setType(PointType.Simple);
    }

    void Update()
    {

    }

    public void setType(PointType new_type)
    {
        type = new_type;
        switch (new_type)
        {
            case PointType.Simple:
                spriteRenderer.color = Color.yellow;
                break;
            default:
                spriteRenderer.color = Color.red;
                break;
        }
    }

    public void setStatus(PointStatus new_status)
    {
        switch (new_status)
        {
            case PointStatus.Selected:
                spriteRenderer.color = Color.green;
                break;
            default:
                setType(type);
                break;
        }
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


public enum PointType { Anchor, Simple }
public enum PointStatus { Selected, Unselected }