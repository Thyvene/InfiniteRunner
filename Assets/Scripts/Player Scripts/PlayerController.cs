using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Animator anim;

    private SpriteRenderer player_Renderer;

    private string jump_Animation = "PlayerJump";
    private string change_Line_Animation = "ChangeLine";

    public GameObject player;
    public GameObject shadow;
    public GameObject explosion;

    public Vector3 first_PosOfPlayer;
    public Vector3 second_PosOfPlayer;

    public Sprite TRex_Sprite;
    public Sprite player_Sprite;

    [HideInInspector]
    public bool player_Died;
    public bool player_Jump;

    private bool TRex_Trigger;

    private GameObject[] start_Effect;

    private void Awake()
    {
        MakeInstance();

        anim = player.GetComponent<Animator>();

        player_Renderer = player.GetComponent<SpriteRenderer>();

        start_Effect = GameObject.FindGameObjectsWithTag(MyTags.StarEffect);
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

            SoundManager.instance.PlayMoveLineSound();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play(change_Line_Animation);
            transform.localPosition = first_PosOfPlayer;

            SoundManager.instance.PlayMoveLineSound();
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

                SoundManager.instance.PlayJumpSound();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag(MyTags.Obstacles))
        {
            if (!TRex_Trigger)
                DieWithObstacle(target);
            else
                DestroyObstacle(target);
        }

        if (target.CompareTag(MyTags.Trex))
        {
            TRex_Trigger = true;
            player_Renderer.sprite = TRex_Sprite;
            target.gameObject.SetActive(false);

            SoundManager.instance.PlayPowerUpSound();

            StartCoroutine(TRexDuration());
        }

        if (target.CompareTag(MyTags.Star))
        {
            for (int i = 0; i < start_Effect.Length; i++)
            {
                if (!start_Effect[i].activeInHierarchy)
                {
                    start_Effect[i].transform.position = target.transform.position;
                    start_Effect[i].SetActive(true);
                    break; // To make sure we don't override with the loop
                }
            }

            target.gameObject.SetActive(false);

            SoundManager.instance.PlayCoinSound();
            // Gameplay controller increase star count TO DO
        }
    }

    void Die()
    {
        player_Died = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GameplayController.instance.moveSpeed = 0f;
        //GameplayController.instance.GameOver(); // TO DO

        SoundManager.instance.PlayDeadSound();
        SoundManager.instance.PlayeGameOverSound();
    }

    void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);

        SoundManager.instance.PlayDeadSound();
    }

    void DestroyObstacle(Collider2D target)
    {
        explosion.transform.position = target.transform.position;
        explosion.SetActive(false); // turn off the explosion if it's already turned on
        explosion.SetActive(true);

        target.gameObject.SetActive(false);

        SoundManager.instance.PlayDeadSound();
    }

    IEnumerator TRexDuration()
    {
        yield return new WaitForSeconds(7f);

        if (TRex_Trigger)
            TRex_Trigger = false;

        player_Renderer.sprite = player_Sprite;
    }
}
