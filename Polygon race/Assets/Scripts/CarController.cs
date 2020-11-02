using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float xDisplacement;
    public float speed;
    public float turningAngle;

    public GameObject mainCameraGO;
    public float deltaCameraX;

    public int wheelsTurn;
    public AudioSource carEngine;

    public float score;
    public Text txtScore;

    public Text txtPause;
    public GameObject pausedPanel;
    public GameObject cityBackGround;
    public float deltaCityBackGround;

    public AudioSource[] objectsWithAudio;

    public bool gameOver;
    public bool gamePaused;

    public AudioClip[] audioEffects;
    AudioSource audioS;
    bool isOver;

    public GameObject gameOverPanel;
    public Text txtGameOver;
    public float speedIncrease;
    float time;

    public bool startCar;
    public DontDestroy data;

    GameObject frontWheelLeft;
    GameObject frontWheelRight;
    GameObject rearWheelLeft;
    GameObject rearWheelRight;


    // Start is called before the first frame update
    void Start()
    {
        xDisplacement = transform.position.x;
        mainCameraGO = GameObject.Find("MainCamera");
        deltaCameraX = xDisplacement - mainCameraGO.transform.position.x;
        wheelsTurn = 0;
        score = 0;
        deltaCityBackGround = xDisplacement - cityBackGround.transform.position.x;
        objectsWithAudio = FindObjectsOfType<AudioSource>();
        gameOver = false;
        gamePaused = false;
        isOver = false;
        speedIncrease = 0;
        time = 0;
        startCar = false;
        data = FindObjectOfType<DontDestroy>();
        frontWheelLeft = GameObject.FindGameObjectWithTag("FrontWheelLeft");
        frontWheelRight = GameObject.FindGameObjectWithTag("FrontWheelRight");
        rearWheelLeft = GameObject.FindGameObjectWithTag("RearWheelLeft");
        rearWheelRight = GameObject.FindGameObjectWithTag("RearWheelRight");
    }

    // Calculates the score and shows it in the Canvas Text object 
    public void CalculateScore()
    {
        score += Time.deltaTime * (speed + speedIncrease) ;
        txtScore.text = ((int)score).ToString();
    }

    // OnPause is called when the player pause the game pressing 'p' 
    public void OnPause()
    {
        gamePaused = true;
        txtPause.gameObject.SetActive(true);
        pausedPanel.SetActive(true);

        // Pause all the sounds
        for (int i = 0; i < objectsWithAudio.Length; i++)
        {
            objectsWithAudio[i].Pause();
        }
    }

    // OnResume is called when the player unpause the game pressing 'p' 
    public void OnResume()
    {
        gamePaused = false;
        txtPause.gameObject.SetActive(false);
        pausedPanel.SetActive(false);

        // Unpause all the sounds
        for (int i = 0; i < objectsWithAudio.Length; i++)
        {
            objectsWithAudio[i].UnPause();
        }
    }

    // Oncrash is called when the player crashes against an obstacle car 
    public void Oncrash()
    {
        for (int i = 0; i < objectsWithAudio.Length; i++)
        {
            // Pause de backgroun music and stop the others sounds
            if (objectsWithAudio[i].GetComponent<DontDestroy>() != null)
                objectsWithAudio[i].Pause();
            else
                objectsWithAudio[i].Stop();
        }

        // Place and plays the crash sound effect
        audioS = GetComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioEffects[0];
        audioS.Play();

        gameOver = true;
    }

    // OnGameOver is called when the game is over, hidding and showing necessary elements
    void OnGameOver()
    {
        txtScore.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        txtGameOver.text = "you did " + (int)score + " mts";
        data.saveInScores((int)score);
    }

    // Calculates the time for increase the speed and increases the spped according that time
    void calculateTime()
    {
        time += Time.deltaTime;
        int seconds = (int)time % 60;
        if (seconds >= 5)
        {
            speedIncrease += 5.0f;
            time = 0;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        float turnInY = 0;
        float wheelsTurnInY = 0;

        // If the game has not start yet do not execute the lines below the return statement
        if (startCar == false)
            return;

        // If the game is over
        if (gameOver == true && isOver == false)
        {
            isOver = true;
            OnGameOver();
        }

        // If the game is not over and not paused
        if (gameOver == false && gamePaused == false)
        {
            calculateTime();
            // Move the car forward and in the horizontal axis
            xDisplacement += 1 * Time.deltaTime * (speed + speedIncrease);
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            transform.position = new Vector3(xDisplacement, transform.position.y, transform.position.z);

            // Move the camera with the player's car in the x and z axis
            mainCameraGO.transform.position = new Vector3(xDisplacement - deltaCameraX, mainCameraGO.transform.position.y, transform.position.z);

            // Rotate the car according the user input
            turnInY = Input.GetAxis("Horizontal") * (turningAngle + speed / 10);
            transform.rotation = Quaternion.Euler(0, 90 + turnInY, 0);

            // Rotate car's wheels
            wheelsTurnInY = turnInY * 3;
            frontWheelLeft.transform.rotation = Quaternion.Euler(0, wheelsTurnInY, (speed / 25) * wheelsTurn++);
            frontWheelRight.transform.rotation = Quaternion.Euler(0, 180 + wheelsTurnInY, (speed / 25) * wheelsTurn++);
            rearWheelLeft.transform.rotation = Quaternion.Euler(0, turnInY, (speed / 25) * wheelsTurn++);
            rearWheelRight.transform.rotation = Quaternion.Euler(0, 180 + turnInY, (speed / 25) * wheelsTurn++);

            // Calculate player's score
            CalculateScore();

            // Move city BackGround with the player's car in the x axis
            cityBackGround.transform.position = new Vector3(xDisplacement - deltaCityBackGround, cityBackGround.transform.position.y, cityBackGround.transform.position.z);
        }    

        // Pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P) && gameOver == false)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                OnPause();
                
            } else if (Time.timeScale == 0){
                Time.timeScale = 1;
                OnResume();
            }
        }        
    }
}
