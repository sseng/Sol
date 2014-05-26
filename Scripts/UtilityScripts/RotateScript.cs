using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public float rotateSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0, Time.deltaTime * rotateSpeed,0);
	}
}
