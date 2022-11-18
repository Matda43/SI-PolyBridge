using UnityEngine;

[RequireComponent(typeof(PointSpawner))]
[RequireComponent(typeof(LineSpawner))]
[RequireComponent(typeof(Destructor))]
[RequireComponent(typeof(Simulation))]
public class Interface : MonoBehaviour
{
    PointSpawner pointSpawner;
    LineSpawner lineSpawner;
    Destructor destructor;
    Simulation simulation;

    public bool simulationRun = false;

    void Start()
    {
        pointSpawner = GetComponent<PointSpawner>();
        lineSpawner = GetComponent<LineSpawner>();
        destructor = GetComponent<Destructor>();
        simulation = GetComponent<Simulation>();
    }

    void Update()
    {
        checkMouseClick();

        if (!simulationRun)
        {
            checkButtonPressed();
            simulation.stop();
        }
        else
        {
            simulation.run();
        }
    }

    void checkMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
            pointSpawner.mouseClick(position);
        }

        if (!simulationRun)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 position = new Vector2(ray.origin.x, ray.origin.y);
                lineSpawner.mouseClick(position);
            }
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
