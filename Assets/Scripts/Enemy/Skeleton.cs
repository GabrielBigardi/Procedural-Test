using System.Collections;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform target;
    public Transform[] instantiatePoint;
    public float moveSpeed;
    public float visionRange;
    public float attackRange;
    public float maxVelocity;
    public float minVelocity;
    public float attackTime;
    public float bulletSpeed;

    public bool canWalk = true;
    public bool alreadyAttacking = false;

    Vector2 walkDir;

    Animator anim;

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
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        walkDir = transform.position - target.position;
        walkDir = Vector2.ClampMagnitude(walkDir, maxVelocity);

        if (canWalk && sawPlayer() && !attackPlayer())
        {
            transform.Translate(-walkDir * moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", true);
            if (walkDir.x < 0f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (walkDir.x > 0f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (attackPlayer() && !alreadyAttacking)
        {
            

            StartCoroutine("Attack_CR");
        }

    }

    public void SpawnProjectile(Transform prefab)
    {
        Vector3 distance = (target.position - transform.position);
        bool spawnRight = distance.x < 0f ? true : false;

        //Vector3 rotation = new Vector3(distance.x, distance.y,distance.z);
        Transform go;

        if(spawnRight)
            go = Instantiate(prefab, instantiatePoint[0].position, Quaternion.identity, transform);
        else
            go = Instantiate(prefab, instantiatePoint[1].position, Quaternion.identity, transform);

        go.LookAt(target);

        float ZRotation = go.eulerAngles.x;

        if (spawnRight)
        {
            go.eulerAngles = new Vector3(0f,0f,ZRotation);
            go.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            go.eulerAngles = new Vector3(0f, 0f, -ZRotation);
            go.GetComponent<SpriteRenderer>().flipX = true;
        }
        //Vector3 goDir = Vector3.RotateTowards(go.position, target.position, 0f, 90f);
        //go.LookAt(target.position);

        //if (spawnRight)
        //{
        //go.GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else
        //{
        //go.GetComponent<SpriteRenderer>().flipX = true;
        //}

        go.GetComponent<Rigidbody2D>().AddForce(-walkDir * bulletSpeed, ForceMode2D.Impulse);
    }

    IEnumerator Attack_CR()
    {
        
        while (true)
         {
            if (walkDir.x < 0f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (walkDir.x > 0f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (attackPlayer())
            {
                alreadyAttacking = true;
                canWalk = false;
                Debug.Log("Atacou");
                GetComponent<Animator>().SetTrigger("Attack");
                yield return new WaitForSeconds(attackTime);
            }
            else
            {
                print("Saiu da coroutine de ataque");
                canWalk = true;
                alreadyAttacking = false;
                yield break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
