using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    public BaseMenu LoseWindow;

    public Transform spawnPoint;
    public Transform goalPoint;

    public RectTransform inLevelUI;
    //public RectTransform endLevelUI;

    public PlayerStats levelPlayer;

    public List<BuyableDoorController> levelDoors = new List<BuyableDoorController>();
    public float distanceToDoorsToSeePrice = 10;
    public List<CoinController> levelCoins = new List<CoinController>();

    public int oneStarScore = 10;
    public int twoStarScore = 20;
    public int threeStarScore = 30;

    public float moneyScoreMultiplier = 2f;

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
        Time.timeScale = 1;
        inLevelUI.gameObject.SetActive(true);
        // LoseWindow.ToggleMenu(false);
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
        if (levelPlayer.IsDead && !LoseWindow.MenuIsShowing)
        {
            LoseWindow.ToggleMenu(true);
            // resetLevel();
        }

        //get player distance to end to see if they have won
        if(Vector3.Distance(levelPlayer.transform.position, goalPoint.position) <= 5){
            Debug.Log("Star: "+CalculateScore(levelPlayer.currentTime, levelPlayer.currentMoney));

        }

        foreach (BuyableDoorController b in levelDoors)
        {
            if(b.gameObject.activeSelf && Vector3.Distance(levelPlayer.transform.position, b.transform.position) <= distanceToDoorsToSeePrice){
                inLevelUI.GetComponent<HUD_Controller>().updateClosestDoorCost(b.transform.position, b.price);
                return;
            }
        }
        inLevelUI.GetComponent<HUD_Controller>().DoorText = "";
    }

    int CalculateScore(float time, int money){
        float score = money*moneyScoreMultiplier + time;
        Debug.Log("Score: "+ score);
        if(score >= oneStarScore){
            if(score >= twoStarScore){
                if(score >= threeStarScore){
                    return 3;
                }
                return 2;
            }
            return 1;
        }
        return 0;
    }
}
