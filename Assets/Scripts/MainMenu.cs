using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private float width, height;
	//public GUIStyle startBtn;

	public float startX, startY, startSizeX, startSizeY;
	public float mainMenuX, mainMenuY;
	public GUIStyle fontStyle;
	public float fontSize = 0;

	// Use this for initialization
	void Start () {
		width = Screen.width / 4;
		height = Screen.height / 4;
	}
	
	// Update is called once per frame
	void Update () {
		fontStyle.fontSize = (int)(width * fontSize);
	}

	void OnGUI(){
		GUI.Label (new Rect (width * mainMenuX, height * mainMenuY, width * startSizeX, height * startSizeY), "Main Menu", fontStyle);

		if(GUI.Button(new Rect(width * startX, height * startY, width * startSizeX, height * startSizeY), "Start")){
			if(PlayerPrefs.GetInt(StringConstants.firstTimePlay) == 0){
				PlayerPrefs.SetInt(StringConstants.firstTimePlay, 1);
				Application.LoadLevel(StringConstants.gameScreen);
			} else {
				Application.LoadLevel(StringConstants.lobbby);
			}
		}
	}
}
