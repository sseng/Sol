using UnityEngine;
using System.Collections;

public class Outline : MonoBehaviour {	

	[HideInInspector]
	public bool 		canOutline;
	private	bool 		outline;	
	private Color 		outlineColor;
	private GameObject 	playerGobj;
	
	void Start(){
		outlineColor 	= renderer.material.GetVector("_OutlineColor");
		playerGobj 		= GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update(){
		renderer.material.SetVector("_OutlineColor", outlineColor);
		checkMouseOverNode ();
		moveShipToOutlined ();
	}
	
	void OnTriggerStay(Collider other){
		if(other.gameObject == playerGobj){
			canOutline = false;
			outline = false;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject == playerGobj && !GameManagerScript.isMoving){
			canOutline = true;	
		}
	}
	void checkMouseOverNode(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;		
		if(Physics.Raycast(ray, out hit) && hit.collider == this.gameObject.collider){
			if(canOutline){
				outline = true;
			}		
		}else{
			outline = false;
		}
	}
	void moveShipToOutlined(){
		if(outline){
			outlineColor.a = 1.0f;
			if(Input.GetMouseButtonDown(0) && !GameManagerScript.isMoving){
				GameManagerScript.moveTo = this.transform.position;
			}
		}else{
			outlineColor.a = 0.0f;	
		}
	}
}
