using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Point : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float radius;

    PointType type;

    Vector3 velocity = Vector3.zero;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black;
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
                changeColor(Color.yellow);
                break;
            default:
                changeColor(Color.red);
                break;
        }
    }

    public void setStatus(PointStatus new_status)
    {
        switch (new_status)
        {
            case PointStatus.Selected:
                changeColor(Color.green);
                break;
            default:
                setType(type);
                break;
        }
    }

    void changeColor(Color color)
    {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
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

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector3 new_velocity)
    {
        velocity = new_velocity;
    }
}


public enum PointType { Anchor, Simple }
public enum PointStatus { Selected, Unselected }