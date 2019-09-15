﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera camMain;
    private Rigidbody rigidBody;
    public float Speed = 1;
    public float JumpSpeed = 1;
    private int jumpNumber = 0;
    private bool jumpKeyDown = false;
    public int MaxJumpCount = 2;

    private Vector3 movementForce;
    private float onGroundHeight = 4f;
    private Ray groundCheckRay;
    private RaycastHit groundCheckRayHit;
    public bool IsOnGround { get; private set; }
    private float timeInAir = 0;
    private const float timeInAirBeforeJump = 0.25f;

    public PlayerStats playerStats;

    public float enemyCollisionForce = 10;
    public int enemyCoinSteal = 10;

    public float controlsDisableTimeOnEnemyCollision = 1.0f;
    private float disabledControlsTimer = 0;

    public ParticleSystem coinBurst;
    
    //Audio Stuff
    [Header("Audio")]
    public AudioClip coinDropSound;
    public AudioClip coinPickUpSound;
    private AudioSource footstepSource;
    private AudioSource coinDropSource;
    private AudioSource coinPickUpSource;
    public AudioClip[] footstepClips;
    public float timeBetweenFootsteps = 0.1f;
    private float timeSinceLastFootstep = 0;

    // Use this for initialization
    void Start()
    {
        timeInAir = 0;
        jumpNumber = 0;

        camMain = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        groundCheckRay = new Ray(transform.position, Vector3.down);

        playerStats = gameObject.GetComponent<PlayerStats>();
        footstepSource = gameObject.AddComponent<AudioSource>();
        coinDropSource = gameObject.AddComponent<AudioSource>();
        coinPickUpSource = gameObject.AddComponent<AudioSource>();

        coinDropSource.clip = coinDropSound;
        coinPickUpSource.clip = coinPickUpSound;

        onGroundHeight = GetComponent<CapsuleCollider>().height + 0.01f;
    }

    public void Reset()
    {
        jumpKeyDown = false;
        movementForce = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        jumpKeyDown |= Input.GetButtonDown("Jump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(disabledControlsTimer > 0){
            disabledControlsTimer -= Time.deltaTime;
            if(disabledControlsTimer <= 0) disabledControlsTimer = 0;
            return;
        }

        // Get Movement Input
        float xSpeed = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        float zSpeed = Mathf.Round(Input.GetAxisRaw("Vertical"));

        // Calculate Left/Right Direction
        Vector3 horizontalAxis = camMain.transform.right;
        horizontalAxis.Scale(new Vector3(1, 0, 1));
        horizontalAxis.Normalize();

        // Calculate Up/Down Direction
        Vector3 verticalAxis = Vector3.Cross(horizontalAxis, Vector3.up).normalized;

        // Check is on ground
        groundCheckRay.origin = transform.position;
        IsOnGround = Physics.Raycast(groundCheckRay, out groundCheckRayHit) && groundCheckRayHit.distance < onGroundHeight;

        // Apply Movement 
        movementForce = (horizontalAxis * xSpeed + verticalAxis * zSpeed).normalized * Speed * Time.fixedDeltaTime;
        // if (IsOnGround || force.sqrMagnitude > float.Epsilon) movementForce = force;
        rigidBody.MovePosition(rigidBody.position + movementForce);
       
        // Play Footprints sounds
        if (timeSinceLastFootstep >= timeBetweenFootsteps && (xSpeed != 0 || zSpeed != 0) && IsOnGround)
        {
            setFootstepClip();
            footstepSource.Play();
            timeSinceLastFootstep = 0;  
        }
        timeSinceLastFootstep += Time.fixedDeltaTime;

        // Apply Jump
        if (IsOnGround)
        {
            timeInAir = 0;
            jumpNumber = 0;
        }
        else
        {
            timeInAir += Time.fixedDeltaTime;
            // Record first jump if falling
            if (timeInAir > timeInAirBeforeJump && jumpNumber < 1)
                jumpNumber = 1;
        }

        if (jumpKeyDown)
        {
            jumpKeyDown = false;
            if (jumpNumber < MaxJumpCount)
            {
                if (jumpNumber > 0)
                {
                    if (playerStats.currentMoney < 10)
                    {
                        return;
                    }
                    coinBurst.Play();
                    playerStats.currentMoney -= 10;
                }
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, JumpSpeed, rigidBody.velocity.z);
                ++jumpNumber;
            }
        }
    }

    public void setFootstepClip()
    {
        footstepSource.clip = footstepClips[(int)Random.Range(0, footstepClips.Length)];
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag == "Enemy"){
            Debug.Log("Collided with Enemy");
            Vector3 forceDirection = transform.position - c.gameObject.transform.position;
            coinBurst.Play();
            coinDropSource.Play();
            rigidBody.AddForce(forceDirection.normalized * enemyCollisionForce, ForceMode.Impulse);
            disabledControlsTimer = controlsDisableTimeOnEnemyCollision;
            
            playerStats.currentMoney -= enemyCoinSteal;
            if(playerStats.currentMoney < 0) playerStats.currentMoney = 0;
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Coin"){
            playerStats.currentMoney += c.gameObject.GetComponent<CoinController>().getValue();
            coinPickUpSource.Play();
            c.gameObject.SetActive(false);
        }

    }
}
