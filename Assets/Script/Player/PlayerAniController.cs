using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController playerController;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerController = gameObject.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //在地面上执行的逻辑
        if(playerController.onGround)
        {
            animator.SetBool("down", false);
            if (Mathf.Abs(rb.velocity.x) >0.1)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
            }    
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", true);
            }
        }//不在地面
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", false);
            if(rb.velocity.y<-0.2)
            {
                animator.SetBool("down", true);
            }
        }
    }
    public void PlayJumpAni()
    {
        animator.SetTrigger("jump");
    }
}
