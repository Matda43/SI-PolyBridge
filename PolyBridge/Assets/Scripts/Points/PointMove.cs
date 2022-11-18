using UnityEngine;

[RequireComponent(typeof(Grid))]
public class PointMove : MonoBehaviour
{
    GameObject goSelected;
    bool isSelected;
    bool isMovable;

    Grid grid;

    void Start()
    {
        isSelected = false;   
        isMovable = false;
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        movePoint();
    }

    void movePoint()
    {
        if(isSelected && isMovable && goSelected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Point point = goSelected.GetComponent<Point>();
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            Vector2 positionOnGrid = grid.getRealPosition(position);
            if (grid.isInGrid(positionOnGrid))
            {
                point.setPosition(positionOnGrid);
                point.SetVelocity(Vector3.zero);
            }
        }
    }

    public bool pointSelected()
    {
        return this.isSelected;
    }

    public bool pointMovable()
    {
        return this.isMovable;
    }

    public void selecte(GameObject go, bool movable)
    {
        this.goSelected = go;
        this.isSelected = true;
        this.isMovable = movable;
        Point point = goSelected.GetComponent<Point>();
        point.setStatus(PointStatus.Selected);
    }

    public void unselecte()
    {
        this.isSelected = false;
        Point point = goSelected.GetComponent<Point>();
        point.setStatus(PointStatus.Unselected);
    }

    public GameObject getGOSelected()
    {
        return this.goSelected;
    }
}
