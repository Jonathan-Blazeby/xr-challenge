using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    int speed;

    void Start()
    {
        anim = GetComponent<Animator>();
        speed = Animator.StringToHash("Speed");
    }

    public void UpdateValues(float playerSpeed)
    {
        if(playerSpeed > 4.5f)
        {
            playerSpeed = 4.5f;
        }
        else if (playerSpeed < 0)
        {
            playerSpeed = 0;
        }
        anim.SetFloat(speed, playerSpeed, 0.1f, Time.deltaTime);
    }
}
