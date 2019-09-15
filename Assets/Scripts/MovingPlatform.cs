using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float Speed;

    public bool toggleScaledSpeed = true;

    private float time;
    private float timeFraction;
    private float distance;
    private int movementDirection = 1;
    
    private Vector3 previousPosition;
    private Vector3 movementForce;
    private List<Rigidbody> objectsOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPosition;

        if(toggleScaledSpeed){
            distance = (StartPosition - EndPosition).magnitude;
            timeFraction = 1 / distance;
        }
        else{
            timeFraction = 1;
        }
        objectsOnPlatform = new List<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate position along path
        if (time >= 1) movementDirection = -1;
        if (time <= 0) movementDirection = 1;
        time += Speed * Time.fixedDeltaTime * timeFraction * movementDirection;

        // Move Platform
        transform.position = Vector3.Lerp(StartPosition, EndPosition, time);
        movementForce = transform.position - previousPosition;
        previousPosition = transform.position;

        // Move objects on platform
        foreach (Rigidbody rb in objectsOnPlatform)
            rb.MovePosition(rb.position + movementForce / 2);
    }

    // Draw the path in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float radius = 0.5f;
        Gizmos.DrawSphere(StartPosition, radius);
        Gizmos.DrawSphere(EndPosition, radius);
        Gizmos.DrawLine(StartPosition, EndPosition);
    }

    // Move objects on top of platform 
    private void OnTriggerEnter(Collider other)
    {
        objectsOnPlatform.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsOnPlatform.Remove(other.attachedRigidbody);
    }
}
