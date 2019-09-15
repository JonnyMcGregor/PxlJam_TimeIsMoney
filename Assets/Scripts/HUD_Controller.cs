using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {
	public PlayerStats player;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI moneyText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = string.Format("{0}:{1:00}", 
                            (int)(player.currentTime / 60), 
                            (int)(player.currentTime % 60));
		moneyText.text = "x" + player.currentMoney;
	}
}
