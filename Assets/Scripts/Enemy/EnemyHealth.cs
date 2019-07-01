using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int curLife = 100;

    public void TakeDamage(int amount)
    {
        curLife -= amount;
        if(curLife <= 0)
        {
            Collider2D[] colliders;
            colliders = GetComponents<Collider2D>();
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }
            GetComponent<Animator>().SetTrigger("Hurt");
            GetComponent<Animator>().SetBool("Death", true);
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Hurt");
        }
    }

    public void Destroy()
    {
        DropManager.Instancia.DropSword(transform.position);
        Destroy(gameObject);
    }
}
