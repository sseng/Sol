using UnityEngine;
using System.Collections;

public class TestRoster : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(0,0, 75, 40), "ship1")){
			Debug.Log(GameManagerScript.battleShips[0].type);
			Debug.Log(GameManagerScript.battleShips[0].armor);
			Debug.Log(GameManagerScript.battleShips[0].ammo);
			Debug.Log(GameManagerScript.battleShips[0].damage);
		}
		if(GUI.Button(new Rect(90,0, 75, 40), "ship2")){
			Debug.Log(GameManagerScript.battleShips[1].type);
			Debug.Log(GameManagerScript.battleShips[1].armor);
			Debug.Log(GameManagerScript.battleShips[1].ammo);
			Debug.Log(GameManagerScript.battleShips[1].damage);
		}
		if(GUI.Button(new Rect(180,0, 75, 40), "ship3")){
			Debug.Log(GameManagerScript.battleShips[2].type);
			Debug.Log(GameManagerScript.battleShips[2].armor);
			Debug.Log(GameManagerScript.battleShips[2].ammo);
			Debug.Log(GameManagerScript.battleShips[2].damage);
		}
	}
}
