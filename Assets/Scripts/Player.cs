using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3 startPosition = new Vector3(0, 0.8f, 0);
	private const float startingPoint = 4.46f;
	private const float waterTop = -2.35f;
	bool hasStarted = false;
	bool hasHitWater = false;
	bool floatOnWater = false;
	bool showStats = false;
	bool startStats = false;
	Rigidbody2D _rigidBody;
	private GameGUI _gameGui;

	//Stats
	private float rampHeight = 0;
	public float acceleration = 0;
	private float airResistance = 0;

	private const float accelerationBase = 50;
	private const float airResistanceBase = 0;

	private float accelerationSpeed = 0;
	private float throttleSpeed = 0;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody2D> ();
		transform.position = startPosition;
		_gameGui = GetComponent<GameGUI> ();
		rampHeight = PlayerPrefs.GetInt (StringConstants.rampHeight);
		acceleration = PlayerPrefs.GetInt (StringConstants.acceleration);
		acceleration = 10;
		airResistance = PlayerPrefs.GetInt (StringConstants.airResistance);
		accelerationSpeed = acceleration * accelerationBase;
		//Debug.Log("Acceleration Speed Start: " + accelerationSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted && transform.position.x >= startingPoint) {
			hasStarted = true;
			_gameGui.isGameStart = true;
			_rigidBody.mass = 0.5f;
			_rigidBody.gravityScale = 0.1f;
			_rigidBody.drag = 1f;
			_rigidBody.angularDrag = 1;
			GetComponent<BoxCollider2D> ().isTrigger = true;
		} else if (!hasStarted) {
			//_rigidBody.AddRelativeForce (Vector2.right * accelerationSpeed);
		} else {
			throttleSpeed = _rigidBody.velocity.magnitude / 5;
			//Debug.Log("MAGNITUDE : " +_rigidBody.velocity.magnitude);
			if(throttleSpeed > 0){
				//Debug.Log("ACCELERATE : " +throttleSpeed);

				_rigidBody.AddRelativeForce(Vector2.up * (throttleSpeed));
			}
			//Accelerate if not started
			//_rigidBody.AddRelativeForce(Vector2.up * (accelerationSpeed / 50));
		}

		if (hasHitWater) {
			transform.position = new Vector3(transform.position.x, waterTop, transform.position.z);
			_rigidBody.drag = 5;
		}

		if (Input.GetKey (KeyCode.A)) {
			float rotateZ = transform.eulerAngles.z + 1;
			Vector3 _rotation1 = new Vector3(0, 0, rotateZ);
			transform.eulerAngles = _rotation1;
		}

		if (Input.GetKey (KeyCode.D)) {
			float rotateZ = transform.eulerAngles.z - 1;
			Vector3 _rotation2 = new Vector3(0, 0, rotateZ);
			transform.eulerAngles = _rotation2;
		}

		#if UNITY_ANDROID
		if(hasStarted){
			Vector3 _rotation = new Vector3(0, 0, transform.eulerAngles.z + -Input.acceleration.x * 2.5f);
			transform.eulerAngles = _rotation;
		}
		#endif
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name == StringConstants.startingPoint) {
			Debug.Log ("Enter trigger");
			showStats = true;
			startStats = true;
			_gameGui.flightDuration = 0;
		}

		if (col.name == StringConstants.water) {
			StartCoroutine(startToFloat());
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.name == StringConstants.water && floatOnWater) {
			_rigidBody.velocity = _rigidBody.velocity / 1.1f;
			_rigidBody.AddForce(Vector2.up * 5f, ForceMode2D.Force);
			if(!hasHitWater){
				hasHitWater = true;
				StartCoroutine(_stopMovement());
			}

			/*if(!hasHitWater && transform.position.y > waterTop){
				transform.position = new Vector3(transform.position.x, waterTop, transform.position.z);
				hasHitWater = true;
				StartCoroutine(_stopMovement());
			}*/
		}
	}

	void OnCollisionStay2D(Collision2D col){
//		Debug.Log ("Collider tag: " + col.transform.tag);
		if (col.transform.tag == StringConstants.slope) {
			Debug.Log("Accelerate");
			//transform.position = new Vector3(transform.position.x, col.collider.bounds.extents.y, transform.position.z);
			_rigidBody.AddForce (Vector2.right * 50);
			_rigidBody.AddForce (Vector2.right * accelerationSpeed);
			//_rigidBody.AddForce(Vector3.right * 25 * (acceleration + 1));
			/*if(!hasHitWater && transform.position.y > waterTop){
				transform.position = new Vector3(transform.position.x, waterTop, transform.position.z);
				hasHitWater = true;
				StartCoroutine(_stopMovement());
			}*/
		}
	}

	private IEnumerator _stopMovement(){
		yield return new WaitForSeconds(1.00f);
		_rigidBody.isKinematic = true;
		_gameGui.isShowResults = true;
	}

	private IEnumerator startToFloat(){
		yield return new WaitForSeconds(0.00f);
		floatOnWater = true;
	}
}
