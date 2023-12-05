using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class RubyController : MonoBehaviour
{
    //Jaylee's code:

    public float spdCoef = 3.0f; // Default speed
    float speed; // Speed after calculations
    float spdVar; // Speed multiplier
    float spdTimr; // Current time until buff expires
    bool spdActv; // Tests if buff is activated.

    public GameObject buffEffect; // Buff visual gameobject
    public AudioClip dehacktivated; // Buff deactivation sound

    //~

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get {return currentHealth;}}
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;
    
    AudioSource audioSource;

    public AudioClip throwClip;
    public AudioClip hitClip;

    public ParticleSystem takeHit;

    public GameObject LoseScreen;


    private void Awake()
    {
        
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Jaylee's code:

        GameObject orbAsset = GameObject.Find("Base"); // Find source prefab
        FpsOrbPassiveBuff orbBuff = orbAsset.GetComponent<FpsOrbPassiveBuff>(); // Import script
        spdVar = orbBuff.spdMulti; // Local speed multiplier equals source's public speed multiplier.
        buffEffect = GameObject.Find("buffEffect"); // Find status indicator
        buffEffect.SetActive(false); // Preemptively deactivate status indicator
        spdActv = false; // Buff is not active

        //~
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }
//Coded by Michelle Radcliffe
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(CogCount.instance.currentCogs > 0)
            {
                Launch();
                CogCount.instance.DecreaseCogs();
            }
        }
            
//Coded by Michelle Radcliffe
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
                DaisyInteract daisy = hit.collider.GetComponent<DaisyInteract>();
                if (daisy != null)
                {
                    daisy.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate() 
    {
        //Jaylee's code:

        GameObject orbAsset = GameObject.Find("Base"); // Find source prefab
        FpsOrbPassiveBuff orbBuff = orbAsset.GetComponent<FpsOrbPassiveBuff>(); // Import script

        // Following code regards speedup buff timer:
        spdTimr = orbBuff.spdTimer; // Sets timer; local variable is ALWAYS equal to source equivalent
        if (spdTimr > 0) // If timer is not zero, set active flag and show status indicator
        {
            buffEffect.SetActive(true);
            spdActv = true;
        } 
        else // If timer is zero,
        {
            buffEffect.SetActive(false); // Remove visual effect,
            if (spdActv == true)
            {
                PlaySound(dehacktivated); // Play deactivation sound,
                spdActv = false; // Then set inactive flag.
            }            
        }

        speed = BuffVal(spdTimr, spdCoef, spdVar); // Function call

        //~

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount){
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if(!takeHit.isPlaying && !isInvincible)
                takeHit.Play();
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hitClip);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if(currentHealth < 1)
        {
            LoseScreen.SetActive(true);
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwClip);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Jaylee's function:
    float BuffVal(float bufTim, float coef, float vrbl) // Scrape current timer, coefficient, and varaible values
    {
        float newVal; // Init output var
        

        if (bufTim > 0) // If timer remains active,
        { 
            newVal = coef * vrbl; // Multiply coefficient and variable,
            return newVal; // And return product.
        } else // Else, skip calculation and return default speed
        {
            return coef;
        }
    }
}