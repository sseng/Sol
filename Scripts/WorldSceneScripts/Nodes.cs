using UnityEngine;
using System.Collections;
using System;

public class Nodes : MonoBehaviour {
	
	public 	bool			toBattle, toCharacterSelect;
	public 	GameObject 		up, down, left, right;
	public	bool			isCleared;
	private GameObject 		playerGobj;
	private Outline			outlineScript;
	private GameObject[] 	direction = new GameObject[4];
	
	void Start () {				
		playerGobj 		= GameObject.FindGameObjectWithTag("Player");
		outlineScript 	= this.gameObject.GetComponent<Outline> ();
		direction 		= new GameObject[]{up, down, left, right};
		
		if(!GameManagerScript.clearedStages.Contains("Node1")){
			GameManagerScript.clearedStages.Add ("Node1");
		}
		if(!GameManagerScript.clearedStages.Contains("Node2")){
			GameManagerScript.clearedStages.Add ("Node2");
		}
	}
	void Update(){
		checkNeighborNodes ();
		
		if (GameManagerScript.clearedStages.Contains (this.gameObject.name)) {
			isCleared = true;
		} else if (!GameManagerScript.clearedStages.Contains (this.gameObject.name)) {
			isCleared = false;
		}
	}	
	void OnTriggerEnter(Collider other){
		if(other.gameObject == playerGobj){	
			GameManagerScript.isMoving = false;
			GameManagerScript.currentNode = this.gameObject;
			GameManagerScript.connectedNodes = this.gameObject.GetComponent<Nodes>();
			
			if(toBattle && !isCleared){
				enterBattle();
			}
			else if(toCharacterSelect && !isCleared){	
				enterCharSelect();
			}		
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject == playerGobj){
			//GameManagerScript.isMoving = true;
			GameManagerScript.clearedStages.Remove("Base1");
		}
	}
	void enterBattle(){
		if(!GameManagerScript.clearedStages.Contains(this.gameObject.name)){
			GameManagerScript.clearedStages.Add(this.gameObject.name);
		}
		Application.LoadLevel("BattleScene");
	}
	void enterCharSelect(){
		if(!GameManagerScript.clearedStages.Contains(this.gameObject.name)){
			GameManagerScript.clearedStages.Add(this.gameObject.name);
		}
		Application.LoadLevel ("CharacterSelect");
	}
	void checkNeighborNodes(){
		foreach (GameObject node in direction) {
			if(node == GameManagerScript.currentNode){
				this.outlineScript.canOutline = true;
				break;
			}else{
				this.outlineScript.canOutline = false;
			}
		}
	}
	void OnDrawGizmos(){
		if(up != null){
			Gizmos.color = new Color(1,0,0);
			Gizmos.DrawLine(this.transform.position, up.transform.position);
		}
		if(down != null){
			Gizmos.color = new Color(.5f,0,0);
			Gizmos.DrawLine(this.transform.position, down.transform.position);
		}
		if(right != null){
			Gizmos.color = new Color(0,0.5f,0);
			Gizmos.DrawLine(this.transform.position, right.transform.position);
		}
		if(left != null){
			Gizmos.color = new Color(0,0.5f,0);
			Gizmos.DrawLine(this.transform.position, left.transform.position);
		}		
	}
}
