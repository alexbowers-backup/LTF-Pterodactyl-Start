using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	GameObject player;
	Vector3 _position;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (StringConstants.player);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x + 2, -3.5f, 1);
	}

	void OnTriggerEnter2D(){
		Debug.Log ("Enter here");
	}
}
