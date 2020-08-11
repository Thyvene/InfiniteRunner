using UnityEngine;

public class ObstaclesHolder : MonoBehaviour
{
    public GameObject[] childs;

    public float limitAxisX;

    public Vector3 firstPos;
    public Vector3 secondPos;

    private void Update()
    {
        transform.position += new Vector3(-GameplayController.instance.moveSpeed * Time.deltaTime, 0f, 0f);

        if (transform.localPosition.x <= limitAxisX)
        {
            // Inform gameplay controller that the obstacle is not active
            GameplayController.instance.obstacles_Is_Active = false;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < childs.Length; i++)
            childs[i].SetActive(true);

        if (Random.value <= 0.5f)
            transform.localPosition = firstPos;

        else
            transform.localPosition = secondPos;
    }
}
