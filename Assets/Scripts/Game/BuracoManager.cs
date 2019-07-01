using UnityEngine;

public class BuracoManager : MonoBehaviour
{

    [Header("Spawn Buracos")]
    public Transform[] buracoPos;
    public Transform prefabBuraco;
    public Transform buracosHolder;
    public int minBuracos;
    public int maxBuracos;
    public int curBuracos;
    bool gerarBuracos = true;


    [Header("Spawn Inimigos")]
    public Transform[] inimigoPos;
    public Transform prefabInimigo;
    public Transform inimigoHolder;
    public int minInimigos;
    public int maxInimigos;
    public int curInimigos;
    bool gerarInimigos = true;

    [Header("Spawn Obstáculos")]
    public Transform[] obstaclePos;
    public Transform[] prefabObstacle;
    public Transform obstacleHolder;
    public int minObstacle;
    public int maxObstacle;
    public int curObstacle;
    bool gerarObstacle = true;

    public bool generated = false;

    void Start()
    {
        //GenerateRoomObjects();
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

    void SpawnObstacle(Vector2 pos)
    {
        Instantiate(prefabObstacle[Random.Range(0,prefabObstacle.Length)], pos, Quaternion.identity, obstacleHolder);
        curObstacle++;
    }

    public void GenerateRoomObjects()
    {
        generated = true;
        //Geração buracos
        for (int i = 0; i < buracoPos.Length; i++)
        {
            if (!gerarBuracos)
                break;


            if (curBuracos == maxBuracos)
            {
                gerarBuracos = false;
                break;
            }


            int randomPct = Random.Range(0, 10);
            if (randomPct >= 5)
            {
                SpawnBuraco(buracoPos[i].position);
            }
            else
            {
                if (curBuracos < minBuracos)
                {
                    SpawnBuraco(buracoPos[i].position);
                }
            }
        }

        //Geração inimigos
        for (int i = 0; i < inimigoPos.Length; i++)
        {
            if (!gerarInimigos)
                break;

            if (curInimigos == maxInimigos)
            {
                gerarInimigos = false;
                break;
            }

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


        //Geração obstáculos
        for (int i = 0; i < obstaclePos.Length; i++)
        {
            if (!gerarObstacle)
                break;


            if (curObstacle == maxObstacle)
            {
                gerarObstacle = false;
                break;
            }


            int randomPct = Random.Range(0, 10);
            if (randomPct >= 5)
            {
                SpawnObstacle(obstaclePos[i].position);
            }
            else
            {
                if (curObstacle < minObstacle)
                {
                    SpawnObstacle(obstaclePos[i].position);
                }
            }
        }
    }
}
