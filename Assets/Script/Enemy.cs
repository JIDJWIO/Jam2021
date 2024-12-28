using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask playerLayer;
    public Vector2 offSet;
    public float checkR;
    public float attackSpeed;
    Rigidbody2D rb;
    bool canAttack;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        //¼ì²âÍæ¼ÒÊÇ·ñÔÚ¹¥»÷·¶Î§
        canAttack = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, playerLayer);
        if (canAttack)
        {
            animator.SetBool("run", true);
            Collider2D collider = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, playerLayer);
            Vector3 playerPos = collider.gameObject.transform.position;
            float dis = playerPos.x - transform.position.x;
            int dir = 0;
            if (dis < 0)
            {
                dir = -1;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }               
            else if (dis > 0)
            {
                dir = 1;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            rb.velocity = new Vector2(dir * attackSpeed, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("run", false);
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR);
    }
}
