using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject[] playerList;
    public float speed;

    private int playerNum;
    private GameObject currentPlayer;

    Rigidbody playerRb;
    Vector3 movement;

    //Screen Boundaries
    Vector2 screenBounds;
    float playerWidth;
    float playerHeight;


    //----------------UI---------------------
    //InGame UI
    [Header("Game UI")]
    [SerializeField] private GameObject gameUI = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI multiplierText = null;
   
    //Death UI
    [Header("DeathUI")]
    [SerializeField] private GameObject deathUI = null;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text deathText;
    [SerializeField] private Text currentScore = null;
    [SerializeField] private Text highScore;
    [SerializeField] private Text maxMultiplier = null;
    [SerializeField] private Text totalObjectsCollided = null;

    //Score
    private int score;
    public int Score { get { return score; } }
    private int multiplier;
    public int Mulitplier { get { return multiplier; } }
    private float scoreTime;
    private int multiplierTime;
    private int objectsCollided;

    //--------------------------------------

    //Shape Change
    private float changeTime;
    private int changePercentage;


    bool first = true;
    Vector3 previousAccel = Vector3.zero;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        currentPlayer = Instantiate(playerList[0]);
        currentPlayer.transform.SetParent(transform);
       
        //Boundaries
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerWidth = transform.GetComponentInChildren<MeshRenderer>().bounds.extents.x;
        playerHeight = transform.GetComponentInChildren<MeshRenderer>().bounds.extents.y;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Score
        score = 0;
        multiplier = 0;
        scoreTime = 0;
        multiplierTime = 0;
        objectsCollided = 0;

        scoreText.text = score.ToString();
        multiplierText.text = "x" + multiplier.ToString();

        //Shape Change
        changeTime = 0;
        changePercentage = 0;

        //UI setting
        gameUI.SetActive(true);
        deathUI.SetActive(false);

        //Reseting time to normal
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        //Input
        movement = GetSmoothedAcceleration();

        //inc score with time
        scoreTime += Time.deltaTime;

        if(scoreTime > 1)
        {
            score += 1;
            scoreTime = 0;
        }
        if (multiplierTime % 10 == 0 && multiplierTime > 0 && multiplier < 9) 
        {
            multiplier += 1;
            multiplierTime = 0;
        }


        //Change Available
        changeTime += Time.deltaTime;
        if (changeTime > 10) 
        {
            changePercentage = Random.Range(0, 10);
            changeTime = 0;
        }
        if (changePercentage > 5)
        {
            ChangeShape();
            changePercentage = 0;
        }
    }

    private void FixedUpdate()
    {
        playerRb.velocity = movement * speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + playerWidth, (screenBounds.x * -1) - playerWidth);
       //c viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + playerHeight, (screenBounds.y * -1) - playerHeight);
        
        transform.position = viewPos;

        //Update Score
        scoreText.text = score.ToString();
        multiplierText.text = "x" + multiplier.ToString();
    }
    Vector3 GetSmoothedAcceleration()
    {
        if (first)
        {
            previousAccel = Input.acceleration;
            first = false;
        }

        Vector3 smoothedAccel = Vector3.Lerp(Input.acceleration, previousAccel, 0.1f);
        smoothedAccel = new Vector3(smoothedAccel.x, 0, 0);
        previousAccel = smoothedAccel;
        return smoothedAccel;
    }
    void ChangeShape()
    {
        Destroy(currentPlayer);
        GetComponentInChildren<PlayerCollision>().ChangeAnim();
        playerNum = Random.Range(0, playerList.Length);

        currentPlayer = Instantiate(playerList[playerNum]);
        currentPlayer.transform.SetParent(transform);
        currentPlayer.transform.localPosition = Vector3.zero;
    }
    
    public void OnChildTriggerEnter(bool isAlive)
    {
        if (isAlive)
        {
            if (multiplier < 1)
            {
                score += 5;
            }
            else
            {
                score += 5 * multiplier;
            }

            objectsCollided += 1;
            multiplierTime += 1;
            ChangeShape();
        }
        else
            PlayeDeath();
    }

    void PlayeDeath()
    {
        //Updating UI
        currentScore.text = score.ToString();
        maxMultiplier.text = "Multiplier:  x" + multiplier.ToString();
        totalObjectsCollided.text = "Objects Collided:  " + objectsCollided.ToString();


        StartCoroutine(ChangeMenu());

    }

    IEnumerator ChangeMenu()
    {
        yield return new WaitForSeconds(1);

        //Changing UI
        gameUI.SetActive(false);
        deathUI.SetActive(true);

        //Destroying Object
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        Destroy(gameObject);
    }

}