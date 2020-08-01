using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Animator anim;

    private string jump_Animation = "PlayerJump";
    private string change_Line_Animation = "ChangeLine";

    public GameObject player;
    public GameObject shadow;

    public Vector3 first_PosOfPlayer;
    public Vector3 second_PosOfPlayer;

    [HideInInspector]
    public bool player_Died;
    public bool player_Jump;

    private void Awake()
    {
        MakeInstance();

        anim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        HandleChangeLine();
        HandleJump();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    void HandleChangeLine()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.Play(change_Line_Animation);
            transform.localPosition = second_PosOfPlayer;

            // Play the sound
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play(change_Line_Animation);
            transform.localPosition = first_PosOfPlayer;

            // Play the sound
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player_Jump)
            {
                anim.Play(jump_Animation);
                player_Jump = true;
            }
        }
    }
}
