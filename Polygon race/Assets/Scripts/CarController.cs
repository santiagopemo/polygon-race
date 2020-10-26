using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float xInitialPosition;
    public float xDisplacement;
    public float zDisplacement;
    public float speed;
    public float highwaySize;
    public float higwayLanes;
    public float turningAngle;

    public GameObject mainCameraGO;
    public float deltaCameraX;
    public GameObject[] rearWheels;

    public GameObject[] frontWheels;

    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;

    public int wheelsTurn;
    public AudioSource carEngine;

    public float score;
    public Text txtScore;

    public Text txtPause;
    public GameObject cityBackGround;
    public float deltaCityBackGround;

    public AudioSource[] ObjectsWithAudio;

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
        rearWheels = GameObject.FindGameObjectsWithTag("RearWheel");
        frontWheels = GameObject.FindGameObjectsWithTag("FrontWheel");
        wheelsTurn = 0;
        // GetComponent<AudioSource>().Play();
        score = 0;
        deltaCityBackGround = xDisplacement - cityBackGround.transform.position.x;
        ObjectsWithAudio = FindObjectsOfType<AudioSource>();
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

    public void CalculateScore()
    {
        score += Time.deltaTime * (speed + speedIncrease) ;
        txtScore.text = ((int)score).ToString();
    }

    public void OnPause()
    {
        gamePaused = true;
        txtPause.gameObject.SetActive(true);
        for (int i = 0; i < ObjectsWithAudio.Length; i++)
        {
            ObjectsWithAudio[i].Pause();
        }
    }

    public void OnResume()
    {
        gamePaused = false;
        txtPause.gameObject.SetActive(false);
        for (int i = 0; i < ObjectsWithAudio.Length; i++)
        {
            ObjectsWithAudio[i].UnPause();
        }
    }

    public void Oncrash()
    {
        for (int i = 0; i < ObjectsWithAudio.Length; i++)
        {
            // Pause de backgroun music and stop the others sounds
            if (ObjectsWithAudio[i].GetComponent<DontDestroy>() != null)
                ObjectsWithAudio[i].Pause();
            else
                ObjectsWithAudio[i].Stop();
        }
        audioS = GetComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioEffects[0];
        audioS.Play();
        gameOver = true;
    }

    void OnGameOver()
    {
        txtScore.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        txtGameOver.text = "you did " + (int)score + " mts";
        data.saveInScores((int)score);
    }

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

    void Update()
    {
        float turnInY = 0;
        float wheelsTurnInY = 0;

        if (startCar == false)
            return;

        if (gameOver == true && isOver == false)
        {
            isOver = true;
            OnGameOver();
        }

        if (gameOver == false && gamePaused == false)
        {
            calculateTime();
            // Move the car forward and in the horizontal axis
            xDisplacement += 1 * Time.deltaTime * (speed + speedIncrease);
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            transform.position = new Vector3(xDisplacement, transform.position.y, transform.position.z);

            // Move the camera with the car
            mainCameraGO.transform.position = new Vector3(xDisplacement - deltaCameraX, mainCameraGO.transform.position.y, transform.position.z);

            // Rotate the car
            turnInY = Input.GetAxis("Horizontal") * (turningAngle + speed / 10);
            transform.rotation = Quaternion.Euler(0, 90 + turnInY, 0);

            // Rotate car's wheels
            wheelsTurnInY = turnInY * 3;
            /*for (int i = 0; i < frontWheels.Length; i++)
            {
                //frontWheels[i].transform.rotation = Quaternion.Euler((speed / 25) * wheelsTurn++, 90 + wheelsTurnInY, 0);
                frontWheels[i].transform.rotation = Quaternion.Euler(0, frontWheels[i].transform.rotation.y + wheelsTurnInY, (speed / 25) * wheelsTurn++);
            }
            for (int i = 0; i < rearWheels.Length; i++)
            {
                //rearWheels[i].transform.rotation = Quaternion.Euler((speed / 25) * wheelsTurn++, 90 + turnInY, 0);
                rearWheels[i].transform.rotation = Quaternion.Euler(0, rearWheels[i].transform.rotation.eulerAngles.y, (speed / 25) * wheelsTurn++);
            }*/
            frontWheelLeft.transform.rotation = Quaternion.Euler(0, wheelsTurnInY, (speed / 25) * wheelsTurn++);
            frontWheelRight.transform.rotation = Quaternion.Euler(0, 180 + wheelsTurnInY, (speed / 25) * wheelsTurn++);
            rearWheelLeft.transform.rotation = Quaternion.Euler(0, turnInY, (speed / 25) * wheelsTurn++);
            rearWheelRight.transform.rotation = Quaternion.Euler(0, 180 + turnInY, (speed / 25) * wheelsTurn++);

            // Calculate player's score
            CalculateScore();

            // Move city BackGround
            cityBackGround.transform.position = new Vector3(xDisplacement - deltaCityBackGround, cityBackGround.transform.position.y, cityBackGround.transform.position.z);
        }    

        // Pause the game
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
    /*public void StartEngine()
    {
        audioS = GetComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioEffects[1];
        audioS.Play();
    }*/
}
