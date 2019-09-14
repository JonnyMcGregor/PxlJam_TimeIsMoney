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

    void Start()
    {
        resetLevel();
    }

    void resetLevel() {
        inLevelUI.gameObject.SetActive(true);
        //endLevelUI.gameObject.SetActive(false);

        levelPlayer.transform.position = spawnPoint.position;
        levelPlayer.transform.position = spawnPoint.position;
        levelPlayer.Initialise();

        foreach(BuyableDoorController b in FindObjectsOfType<BuyableDoorController>()) {
            b.reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (levelPlayer.isDead()) {
            resetLevel();
        }
    }
}
