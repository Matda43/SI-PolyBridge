using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class PointMove : MonoBehaviour
{
    GameObject goSelected;
    bool isSelected;

    Grid grid;

    void Start()
    {
        isSelected = false;   
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        movePoint();
    }

    void movePoint()
    {
        if(isSelected && goSelected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Point point = goSelected.GetComponent<Point>();
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            Vector2 positionOnGrid = grid.getRealPosition(position);
            if (grid.isInGrid(positionOnGrid))
            {
                point.setPosition(positionOnGrid);
            }
        }
    }

    public bool pointSelected()
    {
        return this.isSelected;
    }

    public void selecte(GameObject go)
    {
        this.goSelected = go;
        this.isSelected = true;
    }

    public void unselecte()
    {
        this.isSelected = false;
    }
}
