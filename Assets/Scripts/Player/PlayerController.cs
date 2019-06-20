using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rgbd;

    Vector2 mov;
    public float speed;

    [Header("Espadas")]
    public Sword curSword;
    public string curSwordName;
    public string curSwordDescription;
    public int curSwordMinAtk;
    public int curSwordMaxAtk;
    public int curSwordType;
    

    public bool canAttack = true;
    public bool canMove = true;
    public bool canTurn = true;
    public bool isAttacking = false;
    public bool isDodging = false;
    public bool isFalling = false;

    public float attackTime;
    public float attackAfterPressedTime;
    public float dodgeSpeed;
    public float dodgeTime;

    public float fallDirectionX;
    public float fallDirectionY;

    public float chargebackAmount;

    Vector2 dodgeMov;


    public Transform[] attackObject;


    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //GetComponent<SpriteRenderer>().flipX = false;
        if(curSword != null)
        {
            UpdateSwordAttributes();
        }
    }

    // Update is called once per frame
    void Update()
    {
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (canMove)
        {
            anim.SetFloat("MovX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        }

        if(mov != Vector2.zero)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);

        }

        if (canTurn && Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (canTurn && Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        

        if (Input.GetKeyDown(KeyCode.H))
        {
            GetComponent<PlayerHealth>().TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<PlayerHealth>().RecoverLive(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DropManager.Instancia.DropSword(transform.position);
        }

        if (!isAttacking && !isDodging && Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Attack_CR());
        }

        if (!isDodging && !isAttacking && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dodge_CR());
        }



    }

    void FixedUpdate()
    {

        if(canMove)
            rgbd.MovePosition(rgbd.position + mov * speed * Time.deltaTime);

        if(isDodging)
            rgbd.MovePosition(rgbd.position + dodgeMov * dodgeSpeed * Time.deltaTime);
    }
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Bounds"){
			CameraB.Instance.SetBounds(col.GetComponent<BoxCollider2D>());
		}
        if(col.tag == "Buraco")
        {
            //mov = Vector2.zero;
            fallDirectionX = fallDirectionY = 0f;

            Vector2 playerLastPos = transform.position;
            Vector2 buracoPos = col.transform.position;

            Vector2 difference = playerLastPos - buracoPos;

            //print("Distância X:" + difference.x + " - Distância Y:" + difference.y);

            //definir se o player ta mais pro lado do que pra cima e vice versa
            bool useX()
            {
                if(Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                    return true;
                else
                    return false;
            }
            bool useY()
            {
                if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x))
                    return true;
                else
                    return false;
            }

            //definir se o ta pra cima ou pra baixo, ou se ta pro lado e pro outro
            if (useX())
            {
                if(difference.x < 0f)
                {
                    fallDirectionX = -2f;
                }
                else
                {
                    fallDirectionX = 2f;
                }
            }

            if (useY())
            {
                if (difference.y < 0f)
                {
                    fallDirectionY = -2f;
                }
                else
                {
                    fallDirectionY = 2f;
                }
            }

            //print("Usar X:" + useX() + " - Usar Y:" + useY());

            rgbd.velocity = Vector2.zero;
            dodgeMov = Vector2.zero;
            mov = Vector2.zero;
            canMove = false;
            isFalling = true;
            transform.position = col.transform.position;
            anim.SetTrigger("Fall");
        }
        if(col.tag == "Sword")
        {
            int type = col.GetComponent<SwordHolder>().swordType;
            curSword = DropManager.Instancia.swordsList[type];
            UpdateSwordAttributes();
            Destroy(col.gameObject);
        }
        if(col.tag == "Slime")
        {
            print("slime aefkaeflçeafklç");
            GetComponent<PlayerHealth>().TakeDamage(1);
            //Vector2 direction = col.transform.position - transform.position;
            //ChargeBack(direction * chargebackAmount);
        }
	}

    void CheckAttack()
    {
        if (GetComponent<SpriteRenderer>().flipX)
        {
            attackObject[0].gameObject.SetActive(true);
            print("Atacando...");
            RaycastHit2D hit = Physics2D.Raycast(attackObject[0].position, Vector2.left, 0.5f);
            
            if (hit.collider != null)
            {
                print("0 Acertou em: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    //hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
                    int randomDamage = Random.Range(curSwordMinAtk, curSwordMaxAtk);
                    print("Inimigo Acertado com dano de:" + randomDamage);
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(randomDamage);
                }
            }
        }
        else
        {
            attackObject[1].gameObject.SetActive(true);
            print("Atacando...");
            RaycastHit2D hit = Physics2D.Raycast(attackObject[1].position, Vector2.right, 0.5f);
            if (hit.collider != null)
            {
                print("1 Acertou em: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    print("Inimigo Acertado");
                    //hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
                }
            }
        }
    }

    void ResetAttack()
    {
        attackObject[0].gameObject.SetActive(false);
        attackObject[1].gameObject.SetActive(false);
    }

    public void AttackMovement(int on)
    {
        if (on == 1)
        {
            isAttacking = true;
            canAttack = false;
            canMove = false;
            canTurn = false;
        }
        if (on == 0)
        {
            isAttacking = false;
            canAttack = true;
            canTurn = true;
            canMove = true;
        }
    }

    public void UpdateSwordAttributes()
    {
        curSwordName = curSword.name;
        curSwordDescription = curSword.description;
        curSwordMinAtk = curSword.minAttackDamage;
        curSwordMaxAtk = curSword.maxAttackDamage;
        curSwordType = curSword.SwordType;
        anim.SetFloat("CurSwordType", curSwordType);
    }

    public void TeleportPlayer()
    {
        transform.position = new Vector2(transform.position.x + fallDirectionX, transform.position.y + fallDirectionY);
    }

    public void ResetMovement()
    {
        canMove = true;
        canAttack = true;
        isFalling = false;
    }

    //public void ChargeBack(Vector2 direction)
    //{
    //    transform.position = -direction;
    //}

    IEnumerator Attack_CR()
    {
        
        anim.SetFloat("MovX", 0f);
        anim.SetTrigger("Attack01");
        yield return new WaitForSeconds(attackAfterPressedTime);
        CheckAttack();
        yield return new WaitForSeconds(attackTime);
        ResetAttack();
        
    }

    IEnumerator Dodge_CR()
    {
        dodgeMov = mov;

        if (dodgeMov.x == 1f && dodgeMov.y == 1f)
        {
            dodgeMov.x = 0.75f;
            dodgeMov.y = 0.75f;
        }

        if (dodgeMov.x == -1f && dodgeMov.y == 1f)
        {
            dodgeMov.x = -0.75f;
            dodgeMov.y = 0.75f;
        }

        if (dodgeMov.x == 1f && dodgeMov.y == -1f)
        {
            dodgeMov.x = 0.75f;
            dodgeMov.y = -0.75f;
        }

        if (dodgeMov.x == -1f && dodgeMov.y == -1f)
        {
            dodgeMov.x = -0.75f;
            dodgeMov.y = -0.75f;
        }

        isDodging = true;
        canMove = false;
        canTurn = false;
        canAttack = false;
        anim.SetTrigger("Dodge");
        yield return new WaitForSeconds(dodgeTime);
        if (!isFalling)
        {
            canAttack = true;
            canTurn = true;
            canMove = true;
            isDodging = false;
        }
    }




}
