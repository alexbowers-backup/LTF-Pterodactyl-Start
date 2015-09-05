using UnityEngine;
using System.Collections;

public class Lobby : MonoBehaviour {
	
	private float width, height;
	//public GUIStyle upgradeSquare, upgradeSquarePossible;
	public GUIStyle accelerationStyle, rampHeightStyle, airResistanceStyle;
	public GUIStyle fontStyle;

	//Labels
	public float titleX, titleY;
	public float rampHeightLblX, rampHeightLblY;
	public float accelerationLblX, accelerationLblY;
	public float airResistanceLblX, airResistanceLblY;
	public float researchLblX, researchLblY;
	public float costLblX, costLblY;

	//Buttons
	public float startX, startY, startSizeX, startSizeY;
	public float rampHeightX, rampHeightY, rampHeightSizeX, rampHeightSizeY, rampHeightOffsetX;
	public float accelerationX, accelerationY, accelerationSizeX, accelerationSizeY, accelerationOffsetX;
	public float airResistanceX, airResistanceY, airResistanceSizeX, airResistanceSizeY, airResistanceOffsetX;
	public float resetX, resetY, resetSizeX, resetSizeY;

	//Stats
	private float rampHeight = 0;
	private float acceleration = 0;
	private float airResistance = 0;

	public Texture2D upgradeSquarePossibleTexture, upgradeSquareTexture, upgradedSquareTexture;
	private string hover;
	private string rampHover, accelerationHover;

	private const string rampHeightBtn = "Ramp Button Upgrade";
	private const string accelerationBtn = "Acceleration Button Upgrade";
	private const string airResistanceBtn = "Air Resistance Button Upgrade";

	private const int accelerationCost = 10;
	private const int rampHeightCost = 10;

	private int cash = 0;
	private int cost = 0;

	public float fontSize = 0;

	// Use this for initialization
	void Start () {
		width = Screen.width / 4;
		height = Screen.height / 4;
		cash = PlayerPrefs.GetInt (StringConstants.cash);
		rampHeight = PlayerPrefs.GetInt (StringConstants.rampHeight);
		//PlayerPrefs.SetInt (StringConstants.rampHeight, 0);
		acceleration = PlayerPrefs.GetInt (StringConstants.acceleration);
		airResistance = PlayerPrefs.GetInt (StringConstants.airResistance);
		Debug.Log ("Ramp Height : " + rampHeight + " Acceleration: " + acceleration + " Air Resistance: " + airResistance);
		cash = 1200;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (rampHover == rampHeightBtn) {
			cost = 150;
		} else if (accelerationHover == accelerationBtn) {
			cost = 250;
		} else {
			cost = 0;
		}*/
		fontStyle.fontSize = (int)(width * fontSize);
	}
	
	void OnGUI(){
		GUI.Label (new Rect (width * titleX, height * titleY, 0, 0), "Lobby", fontStyle);
		GUI.Label (new Rect (width * rampHeightLblX, height * rampHeightLblY, 100, 100), "Ramp Height", fontStyle);
		GUI.Label (new Rect (width * accelerationLblX, height * accelerationLblY, 100, 100), "Acceleration", fontStyle);
		GUI.Label (new Rect (width * airResistanceLblX, height * airResistanceLblY, 100, 100), "Air Resistance", fontStyle);
		GUI.Label (new Rect (width * researchLblX, height * researchLblY, 400, 100), "Research Funds" + "   " + cash, fontStyle);
		GUI.Label (new Rect (width * costLblX, height * costLblY, 400, 100), "Cost" + "   " + cost, fontStyle);

		if(GUI.Button(new Rect (width * resetX, height * resetY, width * resetSizeX, height * resetSizeY), "Reset Upgrades")){
			PlayerPrefs.SetInt (StringConstants.rampHeight, 0);
			PlayerPrefs.SetInt (StringConstants.acceleration, 0);
			PlayerPrefs.SetInt (StringConstants.airResistance, 0);
			rampHeight = 0;
			acceleration = 0;
			airResistance = 0;
		}

		//Ramp Height
		for (int i = 1; i < 11; i++) {
			//Debug.Log("Ramp Height Level: " + rampHeight);
			if(rampHeight >= i){
				rampHeightStyle.normal.background = upgradedSquareTexture;
			} else {
				if(cash >= rampHeightCost * i){
					rampHeightStyle.normal.background = upgradeSquarePossibleTexture;
				} else {
					rampHeightStyle.normal.background = upgradeSquareTexture;
				}
			}

			if(GUI.Button(new Rect(width * rampHeightX + width * rampHeightOffsetX * i, height * rampHeightY, width * rampHeightSizeX, height * rampHeightSizeY), new GUIContent ("", rampHeightBtn), rampHeightStyle)){
				PlayerPrefs.SetInt (StringConstants.rampHeight, 1);
				rampHeight++;
			}
			rampHover = GUI.tooltip;
		}

		//Acceleration
		for (int i = 1; i < 11; i++) {
			if(acceleration >= i){
				accelerationStyle.normal.background = upgradedSquareTexture;
			} else {
				if(cash >= accelerationCost * i){
					accelerationStyle.normal.background = upgradeSquarePossibleTexture;
				} else {
					accelerationStyle.normal.background = upgradeSquareTexture;
				}
			}

			if(GUI.Button(new Rect(width * accelerationX + width * accelerationOffsetX * i, height * accelerationY, width * accelerationSizeX, height * rampHeightSizeY), new GUIContent ("", accelerationBtn /*+ i.ToString()*/), accelerationStyle)){
				if(cash >= accelerationCost){
					cash -= accelerationCost;
					acceleration++;
					int accelerationLevel = PlayerPrefs.GetInt (StringConstants.acceleration);
					if(accelerationLevel <= 10){
						PlayerPrefs.SetInt (StringConstants.acceleration, accelerationLevel + 1);
						PlayerPrefs.SetInt (StringConstants.cash, cash);
					}
				}
			}
			accelerationHover = GUI.tooltip;

		}

		if (GUI.tooltip == accelerationBtn) {
			cost = 250;
		} else if (GUI.tooltip == rampHeightBtn) {
			cost = 150;
		}

		//Air Resistance
		for (int i = 1; i < 11; i++) {
			if(GUI.Button(new Rect(width * airResistanceX + width * airResistanceOffsetX * i, height * airResistanceY, width * airResistanceSizeX, height * airResistanceSizeY), new GUIContent ("", airResistanceBtn), airResistanceStyle)){
				
			}
		}

		if(GUI.Button(new Rect(width * startX, height * startY, width * startSizeX, height * startSizeY), "Start")){
			Application.LoadLevel(StringConstants.gameScreen);
		}
	}
}
