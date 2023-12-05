//Coded by Alana Ackley
using UnityEngine;

public class AudioDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Change 'P' to any key you want to use as a trigger
        {
            CheckPlayingAudioSources();
        }
    }

    void CheckPlayingAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                Debug.Log("Audio source playing: " + audioSource.gameObject.name);
            }
        }
    }
}
