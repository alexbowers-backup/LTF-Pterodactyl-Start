using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	GameObject player;
	Vector3 _position;

	private float minimumCameraHeight = -0.75f;
	private float _cameraHeight;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (StringConstants.player);
	}
	
	// Update is called once per frame
	void Update () {
		_cameraHeight = player.transform.position.y;
		if (_cameraHeight < minimumCameraHeight) {
			_cameraHeight = minimumCameraHeight;
		}
		transform.position = new Vector3(player.transform.position.x + 1.5f, _cameraHeight, -10);
	}
}
