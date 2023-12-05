//Coded by Alana Ackley
//External Credits From Unity Asset Store, https://assetstore.unity.com/packages/2d/characters/2d-cartoon-birds-pack-149167 
//External Credits For Sound Source, https://mixkit.co/free-sound-effects/bird/?page=2

using UnityEngine;

public class BirdySound : MonoBehaviour
{
    public float minDistance = 5f;
    public float resetTime = 5f; // New variable for reset time
    Transform player;
    AudioSource _audio;

    public AudioClip audioClip; // Audio clip field to be assigned in the Inspector

    bool isAudioPlayed = false;
    float timer = 0f;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assign player transform
        _audio.clip = audioClip; // Set the audio clip in the AudioSource
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // Check for X key press
        {
            float distance = Vector3.Distance(transform.position, player.position); // Use transform.position
            if (distance <= minDistance && !isAudioPlayed)
            {
                _audio.Play();
                isAudioPlayed = true;
                timer = resetTime; // Set the timer when audio is played
            }
        }

        if (isAudioPlayed)
        {
            timer -= Time.deltaTime; // Countdown the timer
            if (timer <= 0)
            {
                _audio.Stop();
                isAudioPlayed = false;
            }
        }
    }
}