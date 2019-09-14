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
                    playerStats.currentMoney -= 10;
                }
                rigidBody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
                ++jumpNumber;
            }

        }
    }
}
