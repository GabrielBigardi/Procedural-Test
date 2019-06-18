using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    public static DropManager Instancia;

    public Sword[] swordsList;

    public Transform swordPrefab;


    void Awake()
    {
       if(Instancia == null)
            Instancia = this;
        else if (Instancia != this)
            Destroy(Instancia);
    }

    public void DropSword(Vector2 position)
    {
        int RandomItem = Random.Range(0, swordsList.Length);
        Transform droppedSword = Instantiate(swordPrefab, position, Quaternion.identity, transform);
        droppedSword.GetComponent<SpriteRenderer>().sprite = swordsList[RandomItem].sprite;
        droppedSword.GetComponent<SwordHolder>().swordType = RandomItem;
    }

    
}
