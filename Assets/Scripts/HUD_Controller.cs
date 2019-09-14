using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {
	public PlayerStats player;
	public Text timeText;
	public Text moneyText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		timeText.text = "" + ((int)player.currentTime); //+ ":" + ((player.time - (int)player.time) * 60);
		moneyText.text = "" + player.currentMoney;
	}
}
