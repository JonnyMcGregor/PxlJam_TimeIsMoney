using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera camMain;
    private Rigidbody rigidBody;
    public float Speed = 1;
    public float JumpSpeed = 1;
    private int jumpNumber = 0;
    public int MaxJumpCount = 2;
    private const float onGroundHeight = 1.5f;
    private Ray groundCheckRay;
    private RaycastHit groundCheckRayHit;

    public PlayerStats playerStats;

    public float enemyCollisionForce = 10;
    public int enemyCoinSteal = 10;

    public float controlsDisableTimeOnEnemyCollision = 1.0f;
    private float disabledControlsTimer = 0;

    public ParticleSystem coinBurst;

    // Use this for initialization
    void Start()
    {
        camMain = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        groundCheckRay = new Ray(transform.position, Vector3.down);

        playerStats = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if(disabledControlsTimer > 0){
            disabledControlsTimer -= 1*Time.deltaTime;
            if(disabledControlsTimer <= 0) disabledControlsTimer = 0;
            return;
        }

        // Get Movement Input
        float xSpeed = Input.GetAxis("Horizontal");
        float zSpeed = Input.GetAxis("Vertical");

        // Calculate Left/Right Direction
        Vector3 horizontalAxis = camMain.transform.right;
        horizontalAxis.Scale(new Vector3(1, 0, 1));
        horizontalAxis.Normalize();

        // Calculate Up/Down Direction
        Vector3 verticalAxis = Vector3.Cross(horizontalAxis, Vector3.up).normalized;

        // Apply Movement
        Vector3 force = (horizontalAxis * xSpeed + verticalAxis * zSpeed).normalized * Speed;
        rigidBody.MovePosition(rigidBody.position + force);
        //rigidBody.AddForce(force, ForceMode.Impulse);



        // Apply Jump
        if (Input.GetButtonDown("Jump"))
        {

            groundCheckRay.origin = transform.position;
            if (Physics.Raycast(groundCheckRay, out groundCheckRayHit) && groundCheckRayHit.distance < onGroundHeight)
                jumpNumber = 0;

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
                rigidBody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
                ++jumpNumber;
            }

        }
    }

    void OnCollisionEnter(Collision c){

        if(c.gameObject.tag == "Enemy"){
            Debug.Log("Collided with Enemy");
            Vector3 forceDirection = transform.position - c.gameObject.transform.position;
            coinBurst.Play();
            rigidBody.AddForce(forceDirection.normalized * enemyCollisionForce, ForceMode.Impulse);
            disabledControlsTimer = controlsDisableTimeOnEnemyCollision;
            
            playerStats.currentMoney -= enemyCoinSteal;
            if(playerStats.currentMoney < 0) playerStats.currentMoney = 0;
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Coin"){
            playerStats.currentMoney += c.gameObject.GetComponent<CoinController>().getValue();
            c.gameObject.SetActive(false);
        }

    }
}
