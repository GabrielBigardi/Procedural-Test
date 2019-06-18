using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float curHealth = 10;

    public Sprite heartImage;
    public GameObject heartHolder;
    public GameObject[] heartImgs;

    void Start()
    {
        DisableAllLife();
        EnableLife();

    }

    public void TakeDamage(int damageToTake)
    {
        GetComponent<Animator>().SetTrigger("Hurt");
        curHealth -= damageToTake;
        if (curHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("Hurt");
            GetComponent<Animator>().SetBool("Death", true);
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Hurt");
        }

        DisableAllLife();
        EnableLife();
    }

    public void TakeFallDamage(int damage)
    {
        //GetComponent<Animator>().SetTrigger("Hurt");
        curHealth -= damage;
        if (curHealth <= 0)
        {
            //    GetComponent<Animator>().SetTrigger("Hurt");
            //GetComponent<Animator>().SetBool("Death", true);
            Time.timeScale = 0f;
        }

        DisableAllLife();
        EnableLife();
    }

    public void DisableAllLife()
    {
        for (int i = 0; i < heartImgs.Length; i++)
        {
            heartImgs[i].SetActive(false);
        }
    }

    public void EnableLife()
    {
        for (int i = 0; i < curHealth; i++)
        {
            heartImgs[i].SetActive(true);
        }
    }

    public void HurtMovement(int on)
    {
        if (on == 1)
        {
            GetComponent<PlayerController>().isAttacking = false;
            GetComponent<PlayerController>().canAttack = false;
            GetComponent<PlayerController>().canMove = false;
            GetComponent<PlayerController>().canTurn = false;
        }
        if (on == 0)
        {
            GetComponent<PlayerController>().isAttacking = true;
            GetComponent<PlayerController>().canAttack = true;
            GetComponent<PlayerController>().canTurn = true;
            GetComponent<PlayerController>().canMove = true;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
