using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed;
    public float distance_Factor = 1f;

    public GameObject obstacles_Obj;
    public GameObject[] obstacles_List;

    public GameObject pause_Panel;
    public GameObject gameOver_Panel;

    public Animator pause_Anim;
    public Animator gameOver_Anim;

    public Text final_Score_Text;
    public Text best_Score_Text;
    public Text final_Star_Score_Text;

    private float distance_Move;
    private bool gameJustStarted;

    private Text score_Text;
    private Text star_Score_Text;

    private int star_Score_Count;
    private int score_Count;

    [HideInInspector]
    public bool obstacles_Is_Active;

    private void Awake()
    {
        MakeInstance();

        score_Text = GameObject.Find("ScoreText").GetComponent<Text>();
        star_Score_Text = GameObject.Find("StarText").GetComponent<Text>();
    }

    private void Start()
    {
        gameJustStarted = true;

        GetObstacles();
        StartCoroutine(SpawnObstacles());
    }

    private void Update()
    {
        MoveCamera();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    void MoveCamera()
    {
        if (gameJustStarted)
        {
            // check if player is alive
            if (PlayerController.instance.player_Died == false)
            {
                if (moveSpeed < 12.0f)
                    moveSpeed += Time.deltaTime * 5.0f;
                else
                {
                    moveSpeed = 12f;
                    gameJustStarted = false;
                }
            }
        }

        // check if player is alive
        if (PlayerController.instance.player_Died == false)
        {
            Camera.main.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            UpdateDistance();
        }
    }

    void UpdateDistance()
    {
        distance_Move += Time.deltaTime * distance_Factor;
        float round = Mathf.Round(distance_Move);

        score_Count = (int)round; //< Save the score when the player dies
        score_Text.text = round.ToString();

        if (round >= 30.0f && round < 60.0f)
            moveSpeed = 14.0f;
        else if (round > 60.0f)
            moveSpeed = 16.0f;
        else if (round > 90.0f)
            moveSpeed = 18.0f;
        else if (round > 120.0f)
            moveSpeed = 20.0f;
        else if (round > 150.0f)
            StartCoroutine(IncreaseMoveSpeed());

    }

    void GetObstacles()
    {
        obstacles_List = new GameObject[obstacles_Obj.transform.childCount];

        for (int i = 0; i < obstacles_List.Length; i++)
        {
            obstacles_List[i] = obstacles_Obj.GetComponentsInChildren<ObstaclesHolder>(true) [i].gameObject;
        }
    }

    public void UpdateStarScore()
    {
        star_Score_Count++;
        star_Score_Text.text = star_Score_Count.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pause_Panel.SetActive(true);
        pause_Anim.Play("SlideIn");
    }

    public void ResumeGame()
    {
        pause_Anim.Play("SlideOut");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        best_Score_Text.text = GameManager.instance.score_Count.ToString();

        GameManager.instance.starScore += star_Score_Count;
        GameManager.instance.SaveGameData();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;

        best_Score_Text.text = GameManager.instance.score_Count.ToString();

        GameManager.instance.starScore += star_Score_Count;
        GameManager.instance.SaveGameData();

        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver_Panel.SetActive(true);
        gameOver_Anim.Play("SlideIn");

        final_Score_Text.text = score_Count.ToString();
        final_Star_Score_Text.text = star_Score_Count.ToString();

        // Save the best score in data
        if (GameManager.instance.score_Count < score_Count)
            GameManager.instance.score_Count = score_Count;

        best_Score_Text.text = GameManager.instance.score_Count.ToString();

        GameManager.instance.starScore += star_Score_Count;
        GameManager.instance.SaveGameData();
    }

    IEnumerator SpawnObstacles()
    {
        // To make sure that the game don't crash in an infinite loop
        while (true)
        {
            if (!PlayerController.instance.player_Died)
            {
                if (!obstacles_Is_Active)
                {
                    if (Random.value <= 0.85f)
                    {
                        int randomIndex;

                        do
                            randomIndex = Random.Range(0, obstacles_List.Length);
                        while (obstacles_List[randomIndex].activeInHierarchy);

                        obstacles_List[randomIndex].SetActive(true);
                        obstacles_Is_Active = true;
                    }
                }
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

    IEnumerator IncreaseMoveSpeed()
    {
        yield return new WaitForSeconds(10f);

        moveSpeed += 5f;
    }
}
