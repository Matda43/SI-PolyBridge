using UnityEngine;

[RequireComponent(typeof(Grid))]
public class LineMove : MonoBehaviour
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
        moveLine();
    }

    void moveLine()
    {
        if (isSelected && goSelected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Line line = goSelected.GetComponent<Line>();
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            Vector2 positionOnGrid = grid.getRealPosition(position);
            if (grid.isInGrid(positionOnGrid))
            {
                line.draw(positionOnGrid);
            }
        }
    }

    public bool lineSelected()
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

    public GameObject getGOSelected()
    {
        return this.goSelected;
    }
}
