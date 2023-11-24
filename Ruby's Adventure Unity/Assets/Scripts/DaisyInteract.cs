using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coded by Michelle Radcliffe

public class DaisyInteract : MonoBehaviour
{
    float displayTime = 4.0f;
    public GameObject empty;
    public GameObject full;
    float timerDisplay;

    Animator animator;
    AudioSource audioSource;
    public AudioClip bark;


    void Start()
    {
        empty.SetActive(false);
        full.SetActive(false);
        timerDisplay = -1.0f;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                empty.SetActive(false);
                full.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        animator.SetTrigger("Daisytalk");
        PlaySound(bark);
        timerDisplay = displayTime;
        if(CogCount.instance.currentCogs < 10)
        {
            empty.SetActive(true);
            CogCount.instance.ResetCogs();
        }
        else if (CogCount.instance.currentCogs == 10)
        {
            full.SetActive(true);
        }
    }

     public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
