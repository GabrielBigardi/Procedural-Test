using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPrefabSelector : MonoBehaviour
{
    public GameObject spU, spD, spR, spL,
        spUD, spRL, spUR, spUL, spDR, spDL,
        spULD, spRUL, spDRU, spLDR, spUDRL;

    public bool up, down, left, right;

    public int type; // 0 = normal, 1 = enter

    public Color normalColor, enterColor;

    Color mainColor;

    SpriteRenderer rend;

    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        mainColor = normalColor;
        PickPrefab();
        //PickColor();
    }

    void PickPrefab()
    {
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        Instantiate(spUDRL, transform.position, Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(spDRU, transform.position, Quaternion.identity, transform);
                    }
                }else if (left)
                {
                    Instantiate(spULD, transform.position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(spUD, transform.position, Quaternion.identity, transform);
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        Instantiate(spRUL, transform.position, Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(spUR, transform.position, Quaternion.identity, transform);
                    }
                }else if (left)
                {
                    Instantiate(spUL, transform.position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(spU, transform.position, Quaternion.identity, transform);
                }
            }
            return;
        }











        if (down)
        {
            if (right)
            {
                if (left)
                {
                    Instantiate(spLDR, transform.position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(spDR, transform.position, Quaternion.identity, transform);
                }
            }else if (left)
            {
                Instantiate(spDL, transform.position, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(spD, transform.position, Quaternion.identity, transform);
            }
            return;
        }







        if (right)
        {
            if (left)
            {
                Instantiate(spRL, transform.position, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(spR, transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            Instantiate(spL, transform.position, Quaternion.identity, transform);
        }
    }

    void PickColor()
    {
        if(type == 0)
        {
            mainColor = normalColor;
        }else if(type == 1)
        {
            mainColor = enterColor;
        }
        rend.color = mainColor;
    }
}
