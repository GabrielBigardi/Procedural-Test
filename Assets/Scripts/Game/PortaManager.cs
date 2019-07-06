using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaManager : MonoBehaviour
{
    //public static PortaManager Instance;

    public Transform[] portasObjeto;
    public Sprite[] portasAbertas;
    //public Sprite[] portasFechadas;
    public Transform[] saidaColliders;

    public int killedEnemies = 0;
    public int currentEnemies = 0;

   // void Awake()
   // {
   //     if (Instance == null)
   //     {
   //         Instance = this;
   //     }else if (Instance != this)
   //     {
   //         Destroy(gameObject);
   //     }
   // }

    public void AbrirPortas()
    {
        for (int i = 0; i < portasObjeto.Length; i++)
        {
            if (portasObjeto[i].gameObject.activeInHierarchy)
            {
                portasObjeto[i].gameObject.GetComponent<SpriteRenderer>().sprite = portasAbertas[i];
                    saidaColliders[i].gameObject.SetActive(false);

            }
        }
    }
}
