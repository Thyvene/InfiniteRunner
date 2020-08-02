using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator anim;

    private string walk_Animation = "PlayerWalk";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Called in Unity animation system
    void PlayerWalkAnimation()
    {
        anim.Play(walk_Animation);

        if (PlayerController.instance.player_Jump)
            PlayerController.instance.player_Jump = false;
    }

    // Called in Animation system of Unity
    void AnimationEnded()
    {
        gameObject.SetActive(false);
    }
}
