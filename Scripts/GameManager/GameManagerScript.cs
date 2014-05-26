using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameManagerScript{
	public 	static 	aShip[] 		battleShips;
	public 	static  GameObject 		currentNode;
	public 	static  Quaternion 		worldShipRotation;
	public	static 	Nodes			connectedNodes;
	public 	static	List<string>	clearedStages = new List<string>();
	public  static	Vector3			worldShipPos;
	public  static  Vector3			moveTo;
	public  static  bool			isMoving;
	public 	static	float 			worldCamFov;
	public  static  float 			gold;

	static GameManagerScript(){
		battleShips = new aShip[4];
		for (int i = 0; i < battleShips.Length; i++) {
			battleShips[i] = new aShip();
		}
	}	
}
