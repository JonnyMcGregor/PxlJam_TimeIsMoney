using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {
	public PlayerStats player;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI doorCostText;

	public string DoorText{ get{return doorCostText.text;}  set{doorCostText.text = value;} }

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

	public void updateClosestDoorCost(Vector3 doorPos, int price){
		Vector2 screenPos = Camera.main.WorldToScreenPoint(doorPos);
		doorCostText.transform.position = 
		screenPos;
		doorCostText.text = "$" + price;
	}
}
