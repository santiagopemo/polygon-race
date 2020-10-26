using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roads;
    public float xSpawn;
    public float roadLenght;
    public int numberOfRoads;
    public GameObject playerCar;
    private List<GameObject> activeRoads = new List<GameObject>();
    void Start()
    {
        playerCar = FindObjectOfType<CarController>().gameObject;
        xSpawn = 0;
        roadLenght = 140;
        numberOfRoads = 2;
        SpawnRoad(0);
        for (int i = 0; i < numberOfRoads; i++)
        {
            if (i == 0)
                SpawnRoad(0);
            else
                SpawnRoad(Random.Range(0, roads.Length));  
        }
    }

    void Update()
    {
        if (playerCar.transform.position.x - 40 > xSpawn - (numberOfRoads * roadLenght))
        {
            SpawnRoad(Random.Range(0, roads.Length));
            DeleteRoad();
        }
    }
    public void SpawnRoad(int roadIndex)
    {
        GameObject newRoad = Instantiate(roads[roadIndex], transform.right * xSpawn, transform.rotation);
        xSpawn += roadLenght;
        newRoad.SetActive(true);
        activeRoads.Add(newRoad);
    }
    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
