using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    GameObject goSelected;
    bool isSelected;

    void Start()
    {
        isSelected = false;   
    }

    // Update is called once per frame
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
            Vector2 center = new Vector2(ray.origin.x, ray.origin.y);
            point.setPosition(center);
        }
    }

    public bool pointSelected()
    {
        return this.isSelected;
    }

    public void selecte(GameObject go)
    {
        this.isSelected = true;
        this.goSelected = go;
    }

    public void unselecte()
    {
        this.isSelected = false;
    }
}
