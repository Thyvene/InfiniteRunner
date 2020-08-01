using System.Collections;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed;
    public float distance_Factor = 1f;

    public GameObject obstacles_Obj;
    public GameObject[] obstacles_List;

    private float distance_Move;
    private bool gameJustStarted;

    [HideInInspector]
    public bool obstacles_Is_Active;

    private void Awake()
    {
        MakeInstance();
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

        // Count and show the score TODO

        if (round >= 30.0f && round < 60.0f)
            moveSpeed = 14.0f;
        else if (round >= 60.0f)
            moveSpeed = 16f;
    }

    void GetObstacles()
    {
        obstacles_List = new GameObject[obstacles_Obj.transform.childCount];

        for (int i = 0; i < obstacles_List.Length; i++)
        {
            obstacles_List[i] = obstacles_Obj.GetComponentsInChildren<ObstaclesHolder>(true) [i].gameObject;
        }
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
}
