using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	private float width, height;

	public Texture2D resultImage;

	public bool isShowResults = false;
	public GUIStyle fontStyle;
	public float fontSize = 0;

	//Values
	public int distanceGold;
	public int altitudeGold;
	public float flightDuration;
	public int totalGold;
	public float highestAltitude = 0;

	//GUIs
	public float resultsX, resultsY, resultsSizeX, resultsSizeY;
	public float distanceX, distanceY;
	public float altitudeX, altitudeY;
	public float flightDurationX, flightDurationY;
	public float totalX, totalY;

	//Game GUI
	public float currentDistanceX, currentDistanceY;
	public float currentSpeedX, currentSpeedY;
	public float currentAltitudeX, currentAltitudeY;
	public float currentDistanceLblX, currentDistanceLblY;
	public float currentSpeedLblX, currentSpeedLblY;
	public float currentAltitudeLblX, currentAltitudeLblY;

	//Display Values
	private float currentDistance, currentSpeed, currentAltitude;

	public GUISkin guiSkin;
	public bool isGameStart = false;
	private bool callOnce = false;
	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		width = Screen.width / 4;
		height = Screen.height / 4;
		/*distanceGold = 100;
		altitudeGold = 50;
		flightDuration = 5.6543f;*/
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if (isGameStart && !isShowResults) {
			flightDuration += Time.deltaTime;
		}
	}

	// Update is called once per frame
	void Update () {
		fontStyle.fontSize = (int)(width * fontSize);

		if (isShowResults && Input.anyKeyDown) {
			PlayerPrefs.SetInt(StringConstants.cash, totalGold + PlayerPrefs.GetInt(StringConstants.cash));
			Application.LoadLevel(StringConstants.lobbby);
		}

		if (!callOnce && isShowResults) {
			callOnce = true;
			distanceGold = Mathf.RoundToInt(currentDistance * 1.2f);
			altitudeGold = Mathf.RoundToInt((highestAltitude + 1) * 5.6f);
			float _flightDurationModifier = flightDuration / 4;
			if(_flightDurationModifier < 1){
				_flightDurationModifier = 1;
			}
			totalGold = Mathf.RoundToInt((distanceGold + altitudeGold) * _flightDurationModifier);
			PlayerPrefs.SetInt(StringConstants.cash, PlayerPrefs.GetInt(StringConstants.cash) + totalGold);
		}
	}

	void OnGUI(){
		GUI.skin = guiSkin;

		if (isGameStart) {
			currentDistance = transform.position.x - 4;	
			currentDistance = Mathf.Round(currentDistance * 100f) / 100f;

			currentAltitude = transform.position.y;
			currentAltitude = Mathf.Round(currentAltitude * 100f) / 100f;

			if(highestAltitude < transform.position.y){
				highestAltitude = transform.position.y;
			}

			currentSpeed = _rigidbody.velocity.x;
			currentSpeed = Mathf.Round(currentSpeed * 100f) / 100f;

			GUI.Label (new Rect (width * currentDistanceLblX, height * currentDistanceLblY, 200, 50), "Distance", fontStyle);
			GUI.Label (new Rect (width * currentSpeedLblX, height * currentSpeedLblY, 200, 50), "Speed", fontStyle);
			GUI.Label (new Rect (width * currentAltitudeLblX, height * currentAltitudeLblY, 200, 50), "Altitude", fontStyle);

			GUI.Label (new Rect (width * currentDistanceX, height * currentDistanceY, 200, 50), currentDistance.ToString (), fontStyle);
			GUI.Label (new Rect (width * currentSpeedX, height * currentSpeedY, 200, 50), currentSpeed.ToString (), fontStyle);
			GUI.Label (new Rect (width * currentAltitudeX, height * currentAltitudeY, 200, 50), currentAltitude.ToString (), fontStyle);
		}

		if (isShowResults) {
			GUI.DrawTexture (new Rect (width * resultsX, height * resultsY, width * resultsSizeX, height * resultsSizeY), resultImage);
			GUI.Label (new Rect (width * distanceX, height * distanceY, 200, 50), "$" + distanceGold.ToString (), fontStyle);
			GUI.Label (new Rect (width * altitudeX, height * altitudeY, 200, 50), "$" + altitudeGold.ToString (), fontStyle);
			flightDuration = Mathf.Round (flightDuration * 100f) / 100f;
			GUI.Label (new Rect (width * flightDurationX, height * flightDurationY, 200, 50), flightDuration.ToString (), fontStyle);
			GUI.Label (new Rect (width * totalX, height * totalY, 200, 50), "$" + totalGold.ToString (), fontStyle);
		}
	}
}
