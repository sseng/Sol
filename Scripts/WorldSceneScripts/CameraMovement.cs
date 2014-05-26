using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public 	Transform	player;
	public 	float  		trackingSpeed = 2.0f;
	public 	float		zoomSpeed = 10.0f;
	public  float		minFov = 15f; 
	public  float 		maxFov = 90f;
	private Vector3		playerPos;
	private float		fov;
	private float		zoom;
	void Start () {
		GameManagerScript.worldCamFov = Camera.main.fieldOfView;
	}

	void Update () {
		Camera.main.fieldOfView = GameManagerScript.worldCamFov;

		playerPos	= GameManagerScript.worldShipPos;
		playerPos.y = transform.position.y;
		fov			= Camera.main.fieldOfView;
		zoom		= fov;
		transform.position = Vector3.Lerp(transform.position, playerPos, trackingSpeed * Time.deltaTime);

		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			fov++;
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			fov--;
		}
		fov = Mathf.Clamp (fov, minFov, maxFov);
		GameManagerScript.worldCamFov = fov;
	}
}
