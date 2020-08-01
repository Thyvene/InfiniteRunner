using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed;
    public float distance_Factor = 1f;

    private float distance_Move;
    private bool gameJustStarted;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        gameJustStarted = true;
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
}
