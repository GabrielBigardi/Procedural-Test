using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float visionRange;
    public float attackRange;
    public float maxVelocity;

    bool canWalk = false;

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
        if (target == null)
            return;

        Vector2 walkDir = transform.position - target.position;
        walkDir = Vector2.ClampMagnitude(walkDir, maxVelocity);

        if (canWalk && sawPlayer() && !attackPlayer())
        {
            transform.Translate(-walkDir * moveSpeed * Time.deltaTime);
            if (walkDir.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
            
    }

    public void Walk(int onoff)
    {
        if (onoff == 0)
            canWalk = false;
        else
            canWalk = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
