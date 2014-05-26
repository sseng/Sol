using UnityEngine;
using System.Collections;

public class Gui_Battle : MonoBehaviour {
	[HideInInspector]
	public 	GUIStyle 	battleGuiStyle;
	[HideInInspector]
	public 	GUIStyle 	playerFrameGuiStyle;
	/*
	public 	int 		currentHP;
	public  int			maxHP;
	public 	int 		currentAmmo;
	public  int			maxAmmo;
	public 	int 		currentExp;
	public 	int			maxExp;
	*/
	public 	Texture2D 	yellow;
	public 	Texture2D 	blue;
	public 	Texture2D 	green;	
	public 	Rect		battleUI;
	[HideInInspector]
	public 	Rect 		battleCommandButtons;	
	public	Rect 		playerFrame;
	public 	Rect 		playerFrameName;
	public 	Rect 		playerFrameBars;	
	public 	Vector2 	frame1;
	public 	Vector2 	frame2;
	public 	Vector2 	frame3;
	public 	Vector2 	frame4;
	public 	Vector2 	frame5;
	public 	Vector2 	frame6;
		
	private SpaceShipScript[] spaceShips = new SpaceShipScript[6];
	private int shipCounter;

	void Update(){		
	}

	void Start () {
		battleUI.width = Screen.width;
		//battleUI.height = Screen.height/3;
		//battleUI.y = Screen.height *2/3;
	}

	void OnGUI(){
		GUI.Box(battleUI, "", battleGuiStyle);
		GUI.BeginGroup(battleUI, "");			
		/*
		drawPlayerFrame("Player1", frame1.x, frame1.y, spaceShips[0].armor, spaceShips[0].maxArmor, spaceShips[0].ammo, spaceShips[0].maxAmmo);
		drawPlayerFrame("Player2", frame2.x, frame2.y, spaceShips[1].armor, spaceShips[1].maxArmor, spaceShips[1].ammo, spaceShips[1].maxAmmo);
		drawPlayerFrame("Player3", frame3.x, frame3.y, spaceShips[2].armor, spaceShips[2].maxArmor, spaceShips[2].ammo, spaceShips[2].maxAmmo);
		drawPlayerFrame("Enemy1 ", frame4.x, frame4.y, spaceShips[3].armor, spaceShips[3].maxArmor, spaceShips[3].ammo, spaceShips[3].maxAmmo);
		drawPlayerFrame("Enemy2 ", frame5.x, frame5.y, spaceShips[4].armor, spaceShips[4].maxArmor, spaceShips[4].ammo, spaceShips[4].maxAmmo);
		drawPlayerFrame("Enemy3 ", frame6.x, frame6.y, spaceShips[5].armor, spaceShips[5].maxArmor, spaceShips[5].ammo, spaceShips[5].maxAmmo);
		*/
		drawPlayerFrame(GameManagerScript.battleShips[0].type, frame1.x, frame1.y, spaceShips[0].armor, spaceShips[0].maxArmor, spaceShips[0].ammo, spaceShips[0].maxAmmo);
		drawPlayerFrame(GameManagerScript.battleShips[1].type, frame2.x, frame2.y, spaceShips[1].armor, spaceShips[1].maxArmor, spaceShips[1].ammo, spaceShips[1].maxAmmo);
		drawPlayerFrame(GameManagerScript.battleShips[2].type, frame3.x, frame3.y, spaceShips[2].armor, spaceShips[2].maxArmor, spaceShips[2].ammo, spaceShips[2].maxAmmo);
		drawPlayerFrame("Enemy1 ", frame4.x, frame4.y, spaceShips[3].armor, spaceShips[3].maxArmor, spaceShips[3].ammo, spaceShips[3].maxAmmo);
		drawPlayerFrame("Enemy2 ", frame5.x, frame5.y, spaceShips[4].armor, spaceShips[4].maxArmor, spaceShips[4].ammo, spaceShips[4].maxAmmo);
		drawPlayerFrame("Enemy3 ", frame6.x, frame6.y, spaceShips[5].armor, spaceShips[5].maxArmor, spaceShips[5].ammo, spaceShips[5].maxAmmo);
		GUI.EndGroup();
	}
		
	void drawBattleCommands(){	
		GUI.BeginGroup(battleCommandButtons, "");
			GUI.Box(new Rect(0,0, battleCommandButtons.width, battleCommandButtons.height),"");
			GUI.Button(new Rect(0,0,battleCommandButtons.width/2,battleCommandButtons.height/3), "Attack");
			GUI.Button(new Rect(0,battleCommandButtons.height/3,battleCommandButtons.width/2, battleCommandButtons.height/3), "Special");
			GUI.Button(new Rect(0,battleCommandButtons.height*2/3, battleCommandButtons.width/2, battleCommandButtons.height/3), "Defend");
		GUI.EndGroup();
	}
	
	void drawPlayerFrame(string playerName, float offsetX, float offsetY, float hp, float maxHP, float ammo, float maxAmmo){	
		GUI.BeginGroup(new Rect(playerFrame.x + offsetX, playerFrame.y + offsetY, playerFrame.width, playerFrame.height), "");
			//########### Meter Frames ####################
			GUI.BeginGroup(playerFrameBars, "");			
				//############### Meter bars ###################
				GUI.DrawTexture(new Rect(0, playerFrameBars.height*0/2, (hp/maxHP)*playerFrameBars.width ,playerFrameBars.height*1/2 ),green, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(0, playerFrameBars.height*1/2, (ammo/maxAmmo)*playerFrameBars.width, playerFrameBars.height*1/2), blue, ScaleMode.StretchToFill);
				//GUI.DrawTexture(new Rect(0, playerFrameBars.height*2/2, (exp/maxExp)*playerFrameBars.width, playerFrameBars.height*1/2), yellow, ScaleMode.StretchToFill);
				//########### Meter Borders #################
				GUI.Box(new Rect(0, playerFrameBars.height*0/2, playerFrameBars.width ,playerFrameBars.height*1/2 ), hp.ToString(), playerFrameGuiStyle);
				GUI.Box(new Rect(0, playerFrameBars.height*1/2, playerFrameBars.width, playerFrameBars.height*1/2), ammo.ToString(), playerFrameGuiStyle);
				//GUI.Box(new Rect(0, playerFrameBars.height*2/3, playerFrameBars.width, playerFrameBars.height*1/3), "Exp", playerFrameGuiStyle);				
			GUI.EndGroup();
			//######### Name Frame ################
			GUI.BeginGroup(playerFrameName, "");
				GUI.Box(playerFrameName, playerName ,battleGuiStyle);
			GUI.EndGroup();			
		GUI.EndGroup();
	}

	public void setSpaceShips(GameObject _ship)
	{
		spaceShips[shipCounter] =_ship.GetComponent<SpaceShipScript>();
		shipCounter++;
	}
}
