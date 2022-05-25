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

    /// <summary>
    /// Updates the animator attached to the player as to the player's current speed
    /// </summary>
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
