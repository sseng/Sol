using UnityEngine;
using System.Collections;

public class WorldShip : MonoBehaviour {
	public 	aShip[] 		playerShips = new aShip[4];
	public 	GameObject 		currentNode;
	public 	float			moveSpeed = 2;
	public	bool 			isMoving;	
	private Vector3 		currentPos;
	private Vector3 		moveTo;
	private	Quaternion		shipRotation;
	private	Nodes			connectedNodes;
	
	void Start(){
		
	}
	void Update () {
		isMoving 			= GameManagerScript.isMoving;
		transform.position 	= GameManagerScript.worldShipPos;
		this.playerShips 	= GameManagerScript.battleShips;
		transform.rotation 	= GameManagerScript.worldShipRotation;
		currentNode 		= GameManagerScript.currentNode;
		connectedNodes 		= GameManagerScript.connectedNodes;
		currentPos 			= this.transform.position;
		
		shipMovement ();
		Vector3 relativePos = GameManagerScript.moveTo - currentPos;
		if (relativePos != Vector3.zero) {
			shipRotation = Quaternion.LookRotation (relativePos);
			GameManagerScript.worldShipRotation = shipRotation;
		}
		transform.position = new Vector3(
			Mathf.Lerp(currentPos.x, GameManagerScript.moveTo.x, Time.deltaTime * moveSpeed),
			Mathf.Lerp(currentPos.y, GameManagerScript.moveTo.y, Time.deltaTime * moveSpeed),
			Mathf.Lerp(currentPos.z, GameManagerScript.moveTo.z, Time.deltaTime * moveSpeed));
		GameManagerScript.worldShipPos 	= transform.position;
		GameManagerScript.currentNode 	= currentNode;
	}	
	
	public void setDestination(Vector3 destination){
		GameManagerScript.moveTo = destination;
	}
	public void shipMovement(){
		if(!isMoving){
			if(Input.GetKeyDown(KeyCode.LeftArrow) && connectedNodes.left != null){
				GameManagerScript.moveTo = connectedNodes.left.transform.position;
			}
			if(Input.GetKeyDown(KeyCode.RightArrow) && connectedNodes.right != null){
				GameManagerScript.moveTo = connectedNodes.right.transform.position;
			}
			if(Input.GetKeyDown(KeyCode.UpArrow) && connectedNodes.up != null){
				GameManagerScript.moveTo = connectedNodes.up.transform.position;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) && connectedNodes.down != null){
				GameManagerScript.moveTo = connectedNodes.down.transform.position;
			}
		}
	}
}
