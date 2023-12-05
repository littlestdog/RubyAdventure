using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

// Code by Jaylee "Nightvolki" Vick

public class FpsOrbPassiveBuff : MonoBehaviour
{
    // Required for collision detection
    FpsOrbPassiveBuff fpsBuff;
    Collider2D fpsCol;


    // Init variables
    public float spdMulti = 2.0f;
    public float spdTimerCoef =  3.0f;
    public float spdTimer;
    bool buffActive = false;
    float spdExportTim;
    public float origSpd;

    void Awake()
    {
        spdTimer = 0; // Set timer to zero on scene init
    }

    void Start()
    {
        RubyController controller = GetComponent<RubyController>(); // Probably not used for anything
    }

    void Update()
    {
        if (spdTimer > 0) // Execute while timer is not 0
        {
            RubyController controller = GetComponent<RubyController>();
            spdTimer -= Time.deltaTime; // Decrememnt timer at 1 sec / sec until exhausted
            //Debug.Log(spdTimer);
        }
    }

    void OnTriggerStay2D(Collider2D fpsCol) // Execute while player remains inside trigger zone
    {
        GameObject ruby = GameObject.FindGameObjectWithTag("Player");
        RubyController controller = GetComponent<RubyController>();
        if (fpsCol.tag == "Player") // Black magic
        {
            spdTimer = spdTimerCoef; // Timer remains at 3 until player leaves trigger zone
        }
    }

    void FixedUpdate() // I couldn't be bothered to delete this. I don't have time to fix any potential issues.
    {
        
    }
}
