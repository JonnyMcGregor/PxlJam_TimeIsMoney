using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int value = 0;
    public int getValue(){return value;}

    [Header("Animation")]
    public float rotationSpeed = 5f;
    public float bounceAmount = 0.1f;
    public float bounceSpeed = 0.1f;

    private float rotation;
    private float bounceTime;
    private Vector3 initialPosition;


    void Start()
    {
        rotation = Random.Range(0, 360);
        initialPosition = transform.position;
        bounceTime = Random.Range(0, Mathf.PI * 2);
    }

    void Update()
    {
        bounceTime = Mathf.Repeat(bounceTime + bounceSpeed * Time.deltaTime, Mathf.PI * 2);
        rotation = Mathf.Repeat(rotation + rotationSpeed * Time.deltaTime, 360);
        Vector3 newPosition = initialPosition + Vector3.up * Mathf.Cos(bounceTime) * bounceAmount;
        transform.SetPositionAndRotation(newPosition, Quaternion.Euler(0, rotation, 0));
    }
        
}
