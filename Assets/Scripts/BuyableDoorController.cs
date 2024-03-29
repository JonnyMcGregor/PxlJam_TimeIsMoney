﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableDoorController : MonoBehaviour
{

    public int price = 20;

    public bool canBuy(int playerMoney) {
        return playerMoney >= price;
    }

    public void buyDoor() {
        gameObject.SetActive(false);
    }

    public void reset()
    {
        gameObject.SetActive(true);
    }

    public int getCost() { return price; }
}
