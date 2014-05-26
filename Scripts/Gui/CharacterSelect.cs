using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {
	public	bool		useInspectorPositions;
	public 	Rect 		characterFrames;
	public 	Rect 		selectedFrames;
	public	Rect[] 		characters;
	public	Rect[]		charTextFrame;
	public  string[]	charText;
	public 	Rect[] 		selected;
	public 	Rect 		okButton;
	public 	Texture2D[] charTextures;
	public  GUIStyle	portraitStyle;
	private	aShip[]		playerShips = new aShip[3];
	private Texture2D[] selectedTexture = new Texture2D[3];
	private	int			shipType;
	private	int			activeSelection;
	private bool		toggle;
	public  GUIStyle	textStyle;
		
	void Start(){
		for(int i = 0; i < 3; i++){
			selectedTexture[i] = charTextures[6];
        }
		playerShips = new aShip[4];
		for (int i = 0; i < playerShips.Length; i++) {
			playerShips[i] = new aShip();
		}
		//set GUI elements proprotionate to screen size.
		if (!useInspectorPositions) {
			characterFrames.x = 0;
			characterFrames.y = Screen.width *1/6;
			selectedFrames.x = 0; 
			selectedFrames.y = Screen.height/2;
			characterFrames.width = Screen.width;
			characterFrames.height = Screen.height * 2/5;
			selectedFrames.width = Screen.width;

			for(int i = 0; i < 3; i++){
				characters [i].x = (characterFrames.width * (i+1)/4) - (characters [i].width / 2);
				selected[i].x = characters[i].x;
			}
			okButton.x = (Screen.width / 2) - (okButton.width / 2);
			okButton.y = (Screen.height *3/4) - (okButton.height/2);
		}

	}
	void Update(){
		for(int i = 0; i < playerShips.Length; i++){
			GameManagerScript.battleShips[i] = playerShips[i];
		}
	}

	void OnGUI(){
		drawFrame(characters.Length);
		drawSelectedFrame(selected.Length);

		if(GUI.Button(okButton, "Ok") && toggle == true){
			if(activeSelection < 3){
				playerShips[activeSelection] = makeShip(shipType);
				activeSelection++;
				toggle = false;
			}
			if(activeSelection == 3){
				Application.LoadLevel("WorldScene");
			}
		}
		GUI.skin.box.wordWrap = false;
	}

	public void drawFrame(int amount){
		for(int i = 0; i < amount; i++){
			GUI.BeginGroup(characterFrames, "");
			if(GUI.Button(characters[i], new GUIContent(charTextures[i]),portraitStyle)){
				selectedTexture[activeSelection] = charTextures[i];
				shipType = i;
				toggle = true;
			}
			GUI.Box(charTextFrame[i], charText[i], textStyle);
			GUI.EndGroup();
		}
	}

	public void drawSelectedFrame(int amount){
		for(int i = 0; i < amount; i++){
			GUI.BeginGroup(selectedFrames, "");
				GUI.DrawTexture(selected[i], selectedTexture[i], ScaleMode.ScaleToFit);
			GUI.EndGroup();
		}
	}
	aShip makeShip(int type){
		aShip battleship = new aShip();

		if(type == 0){
			battleship = new aShip("Frigate", 30, 30, 25, 25, 6);
		}
		if(type == 1){
			battleship = new aShip("Cruiser", 60, 60, 5, 5, 4);
		}
		if(type == 2){
			battleship = new aShip("Destroyer", 40, 40, 10, 10, 7);
		}
		if(type == 3){
			battleship = new aShip("Battleship", 200, 200, 20, 20, 20);
		}
		if(type == 4){
			battleship = new aShip("type"+type, 100, 100, 6, 6, 15);
		}
		if(type == 5){
			battleship = new aShip("type"+type, 120, 120, 25, 25, 5);
		}
		return battleship;
	}
}
