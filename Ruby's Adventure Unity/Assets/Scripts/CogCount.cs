using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Coded by Michelle Radcliffe

public class CogCount : MonoBehaviour
{
    public ParticleSystem cogBurst;
    public static CogCount instance;
    public TMP_Text counterText;
    public int currentCogs = 10;
    public AudioClip cogUp;
    AudioSource audioSource;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    void Start()
    {
        RubyController Ruby = gameObject.GetComponent<RubyController>();
        counterText.text = "x " + currentCogs.ToString();
    }

    public void DecreaseCogs()
    {
        currentCogs --;
        counterText.text = "x " + currentCogs.ToString();

    }

    public void ResetCogs()
    {
        if(!cogBurst.isPlaying)
                cogBurst.Play();
        PlaySound(cogUp);
        currentCogs = 10;
        counterText.text = "x " + currentCogs.ToString();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
