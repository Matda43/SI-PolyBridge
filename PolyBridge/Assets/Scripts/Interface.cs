using UnityEngine;

[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(LineSpawner))]
[RequireComponent(typeof(Destructor))]
public class Interface : MonoBehaviour
{
    PointSpawner pointSpawner;
    LineSpawner lineSpawner;
    Destructor destructor;

    void Start()
    {
        pointSpawner = GetComponent<PointSpawner>();
        lineSpawner = GetComponent<LineSpawner>();
        destructor = GetComponent<Destructor>();
    }

    void Update()
    {
        checkMouseClick();
        checkButtonPressed();
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

    void checkButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            destructor.deleteSelected();
        }
    }
}
