using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{

    public static FadeManager Instance;

    Animator anim;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);


        anim = GetComponent<Animator>();
    }

    public void FadeIN()
    {
        anim.SetTrigger("Fade_IN");
    }

    public void FadeOUT()
    {
        anim.SetTrigger("Fade_OUT");
    }
}
