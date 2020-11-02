using System.Collections.Generic;
using UnityEngine;

// RoadManager - Class that handels the endless road mode
public class RoadManager : MonoBehaviour
{
    public GameObject[] roads;
    public float xSpawn;
    public float roadLenght;
    public int numberOfRoads;
    public GameObject playerCar;
    private List<GameObject> activeRoads = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerCar = FindObjectOfType<CarController>().gameObject;
        xSpawn = 0;
        roadLenght = 140;
        numberOfRoads = 2;
        SpawnRoad(0);

        // Generate the first section of the road with numberOfRoads pieces of road
        for (int i = 0; i < numberOfRoads; i++)
        {
            if (i == 0)
                SpawnRoad(0);
            else
                SpawnRoad(Random.Range(0, roads.Length));  
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Place a new piece of road at the end and delete the piece of road at the beggining of a road section
        if (playerCar.transform.position.x - 40 > xSpawn - (numberOfRoads * roadLenght))
        {
            SpawnRoad(Random.Range(0, roads.Length));
            DeleteRoad();
        }
    }

    // Create a new piece of road and enable it
    public void SpawnRoad(int roadIndex)
    {
        GameObject newRoad = Instantiate(roads[roadIndex], transform.right * xSpawn, transform.rotation);
        xSpawn += roadLenght;
        newRoad.SetActive(true);
        activeRoads.Add(newRoad);
    }

    // Destoy a piece of road
    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
