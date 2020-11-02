using UnityEngine;

// Obstacle - Class that handelss the behaviour of the obstacles cars
public class Obstacle : MonoBehaviour
{
    public GameObject playerCar;
    public CarController carController;
    float obstacleXDisplacement;
    float obstscleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerCar = FindObjectOfType<CarController>().gameObject;
        carController = playerCar.GetComponent<CarController>();
        gameObject.GetComponent<Collider>().isTrigger = true;
        obstacleXDisplacement = transform.position.x;
        obstscleSpeed = 5;
    }

    // OnTriggerEnter is called when other object collides with this
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            carController.Oncrash();
        }
    }

    // Update is called once per frame
    void Update()
    {
        obstacleXDisplacement += 1 * Time.deltaTime * obstscleSpeed;
        transform.position = new Vector3(obstacleXDisplacement, transform.position.y, transform.position.z);
    }
}
