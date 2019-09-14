using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{

    public Transform spawnPoint;
    public Transform goalPoint;

    public RectTransform inLevelUI;
    //public RectTransform endLevelUI;

    public PlayerStats levelPlayer;

    public List<BuyableDoorController> levelDoors = new List<BuyableDoorController>();
    public List<CoinController> levelCoins = new List<CoinController>();

    void Start()
    {
        foreach (BuyableDoorController b in FindObjectsOfType<BuyableDoorController>())
        {
            levelDoors.Add(b);
        }
        foreach (CoinController c in FindObjectsOfType<CoinController>())
        {
            levelCoins.Add(c);
        }
        resetLevel();
    }

    public void resetLevel()
    {
        inLevelUI.gameObject.SetActive(true);
        //endLevelUI.gameObject.SetActive(false);

        levelPlayer.Initialise(spawnPoint.position);

        foreach (BuyableDoorController b in levelDoors)
        {
            b.reset();
        }
        foreach (CoinController c in levelCoins)
        {
            c.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (levelPlayer.isDead())
        {
            resetLevel();
        }
    }
}
