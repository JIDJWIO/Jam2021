using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public string attackTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== attackTag)
        {
            collision.gameObject.GetComponent<Character>().Hitted(damage);
        }
    }
}
