using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float startingTime = 111;
    public int startingMoney = 522;
    public int currentMoney;
    public float currentTime;

    public int minimumHeight = -10;
    private Rigidbody rigidBody;

    public ParticleSystem coinBurst;

    public float maxTransferSpeed = 5;
    public float minTransferSpeed = 3;
    public float transferRateIncreaseRate = 1f;
    public float transferSpeed = 2f;
    private float coinBeingTransfered = 0f;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if(Input.GetButtonDown("UseTime")){
            currentTime -= 1;
            currentMoney += 1;
        }

        if (Input.GetButton("UseTime"))
        {
            coinBeingTransfered += transferSpeed*Time.deltaTime;
            if(coinBeingTransfered >= 1){
                coinBeingTransfered = 0;
                currentTime -= 5;
                currentMoney += 5;

                if(transferSpeed <= maxTransferSpeed) transferSpeed += transferRateIncreaseRate*Time.deltaTime; //Speed up the rate of time to coin transfer the longer they hold the button
            }
        }

        if(Input.GetButtonUp("UseTime")){
            coinBeingTransfered = 0;
            transferSpeed = minTransferSpeed;
        }

        if (Input.GetButtonDown("UseMoney"))
        {
            Debug.Log("Input pressed");
            buyNearbyItem();
        }
    }

    void buyNearbyItem()
    {
        Collider[] nearbyItems = Physics.OverlapSphere(transform.position, 10);
        foreach (Collider c in nearbyItems)
        {
            Debug.Log(c.gameObject.tag);
            if (c.gameObject.tag == "Buyable")
            {
                BuyableDoorController itemToBuy = c.gameObject.GetComponent<BuyableDoorController>();
                if (itemToBuy.canBuy(currentMoney))
                {
                    coinBurst.Play();
                    currentMoney -= itemToBuy.getCost();
                    itemToBuy.buyDoor();
                }
            }
        }

    }

    //Initialise the players stats
    public void Initialise(Vector3 initPosition)
    {

        currentMoney = startingMoney;
        currentTime = startingTime;

        // Reset physics
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.position = initPosition;

        coinBeingTransfered = 0;
        transferSpeed = minTransferSpeed;
    }

    public bool isDead()
    {
        if (transform.position.y < minimumHeight)
        {
            return true;
        }

        return currentTime <= 0;
    }

}
