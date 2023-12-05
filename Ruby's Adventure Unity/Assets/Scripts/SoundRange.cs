//Coded by Alana Ackley
//External Credits From Unity Asset Store, https://assetstore.unity.com/packages/2d/gui/icons/tool-icons-and-blood-sprites-118526 
//External Credits For Sound Source, https://pixabay.com/sound-effects/search/flicker/
using UnityEngine;

public class SoundRange : MonoBehaviour
{
    public float triggerDistance = 5f; // Distance to trigger the sound
    public AudioClip audioClip; // Audio clip to play
    [Range(0f, 1f)]

    private Transform player;
    private AudioSource audioSource;
    private bool isPlayerInRange = false;
    private bool hasPlayed = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip; // Set the audio clip
    }

    void Update()
    {
        if (!hasPlayed)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= triggerDistance && !isPlayerInRange)
            {
                audioSource.Play();
                isPlayerInRange = true;
                hasPlayed = true;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > triggerDistance && isPlayerInRange)
            {
                audioSource.Stop();
                isPlayerInRange = false;
            }
        }
    }
}

