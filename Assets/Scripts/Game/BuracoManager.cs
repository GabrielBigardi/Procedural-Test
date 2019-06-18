using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuracoManager : MonoBehaviour
{

    [Header("Spawn Buracos")]
    public Transform[] buracoPos;
    public Transform prefabBuraco;
    public Transform buracosHolder;
    public int minBuracos;
    public int curBuracos;

    [Header("Spawn Inimigos")]
    public Transform[] inimigoPos;
    public Transform prefabInimigo;
    public Transform inimigoHolder;
    public int minInimigos;
    public int curInimigos;


    void Start()
    {
        //Geração buracos
        for (int i = 0; i < buracoPos.Length; i++)
        {
            int randomPct = Random.Range(0, 10);
            if(randomPct >= 5)
            {
                SpawnBuraco(buracoPos[i].position);
            }
            else
            {
                //print("Buraco não spawnado");
                if (curBuracos < minBuracos)
                {
                    SpawnBuraco(buracoPos[i].position);
                }
            }
        }

        //Geração inimigos
        for (int i = 0; i < inimigoPos.Length; i++)
        {
            int randomPct = Random.Range(0, 10);
            if (randomPct >= 5)
            {
                SpawnInimigo(inimigoPos[i].position);
            }
            else
            {
                //print("Inimigo não spawnado");
                if (curInimigos < minInimigos)
                {
                    SpawnInimigo(inimigoPos[i].position);
                }
            }
        }
    }

    void SpawnBuraco(Vector2 pos)
    {
        Instantiate(prefabBuraco, pos, Quaternion.identity, buracosHolder);
        curBuracos++;
    }

    void SpawnInimigo(Vector2 pos)
    {
        Instantiate(prefabInimigo, pos, Quaternion.identity, inimigoHolder);
        curInimigos++;
    }
}
