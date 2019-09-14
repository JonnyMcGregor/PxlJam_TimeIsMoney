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

    public ParticleSystem coinBurst;

    // Use this for initialization
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (Input.GetButtonDown("UseTime"))
        {
            currentTime -= 10;
            currentMoney += 10;
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
    public void Initialise()
    {
        currentMoney = startingMoney;
        currentTime = startingTime;
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
