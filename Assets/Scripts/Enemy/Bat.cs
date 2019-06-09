using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float visionRange;
    public float attackRange;
    public float maxVelocity;
    public float minVelocity;

    bool canWalk = true;

    bool sawPlayer()
    {
        Collider2D OverlapCircle1 = Physics2D.OverlapCircle(transform.position, visionRange, 1 << LayerMask.NameToLayer("Player"));
        return OverlapCircle1;
    }

    bool attackPlayer()
    {
        Collider2D OverlapCircle2 = Physics2D.OverlapCircle(transform.position, attackRange, 1 << LayerMask.NameToLayer("Player"));
        return OverlapCircle2;
    }

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
    {
        Vector2 walkDir = transform.position - target.position;
        walkDir = Vector2.ClampMagnitude(walkDir,maxVelocity);
        print(walkDir.x + " - " + walkDir.y);

        ////impoe velocidade minima
        //if (walkDir.x > 0f && walkDir.x < minVelocity) // se a direção x for maior do que 0 (positivo) e for menor do que a velocidade minima (3f)
        //    walkDir.x = minVelocity; // a direção x é igual a velocidade minima
        //if (walkDir.y > 0f && walkDir.y < minVelocity) // se a direção y for maior do que 0 (positivo) e for menor do que a velocidade minima (3f)
        //    walkDir.y = minVelocity; // a direção y é igual a velocidade minima
        //if (walkDir.x < 0f && walkDir.x > -minVelocity) // se a direção x for menor do que 0 (negativo) e for maior do que -velocidade minima (-3f)
        //    walkDir.x = -minVelocity; // a direção x é igual a -velocidade minima
        //if (walkDir.y < 0f && walkDir.y > -minVelocity) // se a direção y for menor do que 0 (negativo) e for maior do que -velocidade minima (-3f)
        //    walkDir.y = -minVelocity; // a direção y é igual a -velocidade minima
        //
        //
        ////limita velocidade maxima
        //if (walkDir.x > 0f && walkDir.x > maxVelocity) // se a direção x for positiva e for maior do que a velocidade maxima (3f)
        //    walkDir.x = maxVelocity; // a direção x é igual a velocidade maxima
        //if (walkDir.y > 0f && walkDir.y > maxVelocity) // se a direção y for positiva e for maior do que a velocidade maxima (3f)
        //    walkDir.y = maxVelocity; // a direção y é igual a velocidade maxima
        //if (walkDir.x < 0f && walkDir.x < -maxVelocity) // se a direção x for negativa e for menor do que -velocidade maxima (3f)
        //    walkDir.x = -maxVelocity; // a direção x é igual a -velocidade maxima
        //if (walkDir.y < 0f && walkDir.y < -maxVelocity) // se a direção y for negativa e for meno do que -velocidade maxima (3f)
        //    walkDir.y =- maxVelocity; // a direção y é igual a -velocidade maxima

        if (canWalk && sawPlayer() && !attackPlayer())
        {
            transform.Translate(-walkDir * moveSpeed * Time.deltaTime);
            if (walkDir.x < 0f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (walkDir.x > 0f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (attackPlayer())
        {
            print("Atacando Player");
        }

    }

    //public void Walk(int onoff)
    //{
    //    if (onoff == 0)
    //        canWalk = false;
    //    else
    //        canWalk = true;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
