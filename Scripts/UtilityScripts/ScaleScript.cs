using UnityEngine;
using System.Collections;

public class ScaleScript : MonoBehaviour {
	public float scaleSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float scaleValue = Mathf.Abs(Mathf.Cos (Time.time* scaleSpeed));
		this.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);	
	}
}
