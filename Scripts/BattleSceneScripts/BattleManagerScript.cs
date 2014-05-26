using UnityEngine;
using System.Collections;

public class BattleManagerScript : MonoBehaviour {

	//TEMPORARY DATA START
	GameObject[] playerParty = new GameObject[3];
	//TEMPORARY DATA END

	public GameObject[]enemyPartyOptions;
	public Transform[]characterLocations;

	private int difficultyModifier;

	//CHARACTERS PLACED IN A BATTLE ORDER
	private GameObject[]battleOrder = new GameObject[6];
	private int battleOrderIndex = 0;

	//GUI ELEMENTS 
	public Texture2D backgroundUI;
	public Texture2D buttonImageUI;
	public Texture2D enemyBackgroundUI;
	public GUIStyle buttonStyle;
	public int buttonBuffer;

	//TRACK SPACESHIP MOVE SET
	SpaceShipScript shipScript;
	public GameObject attackListGameObject;
	CharacterAttackScript attackScript;

	//TARGET LOCATIONS STUFF
	public GameObject activeGameObjectParticle;
	public GameObject targetParticle;
	GameObject targetParticleOBJ;
	bool chooseTargetBool = false;
	int targetIndex = 1;
	int activeCharacterIndex;

	//ENEMY AI/TURN STUFF HERE
	string enemyAttackChoice;
	bool enemyIsActing;

	// Use this for initialization
	void Start () {

		loadCharacterShips();

		GameObject ft = GameObject.FindGameObjectWithTag("FacingTargetE");
		//LOAD ENEMY GAMEOBJECTS
		for (int i = 0; i < 3;i++)
		{
			GameObject g = Instantiate(enemyPartyOptions[Random.Range(0,5)],characterLocations[i+3].position,Quaternion.identity) as GameObject;
			g.transform.LookAt(ft.transform.position);
			battleOrder[i+3] = g;
			sendReferenceShipToGUI(g);
		}
		//SET DIFFICULTY MODIFIER BASED ON GLOBAL MANAGER SCRIPT, THEN ATTACH MODIFIER TO ENEMY
		difficultyModifier = 0;

		shipScript = battleOrder[battleOrderIndex].GetComponent<SpaceShipScript>();

		activeGameObjectParticle = Instantiate(activeGameObjectParticle) as GameObject;

		attackScript = attackListGameObject.GetComponent<CharacterAttackScript>();


	}
	
	// Update is called once per frame
	void Update () {

		activeGameObjectParticle.transform.position = battleOrder[battleOrderIndex].transform.position;
		if(chooseTargetBool)
		{

			//targetParticleOBJ.SetActive(true);
			targetParticleOBJ.transform.position = battleOrder[targetIndex].transform.position;
			checkMouseOverShip();
			/*
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if(targetIndex == 0)
					targetIndex = 5;
				else
					targetIndex--;
			}
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				if(targetIndex == 5)
					targetIndex = 0;
				else
					targetIndex++;
			}
			if(Input.GetKeyDown(KeyCode.Return))
			{
				if(!battleOrder[targetIndex].GetComponent<SpaceShipScript>().disabled)
				{
					//ATTACK IS SENT OUT FROM HERE
					callAttack(shipScript.characterAttacks[activeCharacterIndex],shipScript.characterAttackAmmoCost[activeCharacterIndex],targetIndex);
					DestroyObject(targetParticleOBJ);
					chooseTargetBool = false;
				}
			}
			*/
		}

		//ENEMY AI HANDLED HERE
		if(shipScript.isEnemy && !enemyIsActing)
		{
			StartCoroutine("enemyAttackStageOne");
			enemyIsActing = true;
		}
		
	}

	void OnGUI()
	{
		if(shipScript.isEnemy == false)
		{
			GUI.Label(new Rect(0,Screen.height - Screen.height/6,Screen.width,Screen.height/5),backgroundUI);
			Rect buttonPosition = new Rect(Screen.width/5, Screen.height -Screen.height/6.5f,Screen.width/5,Screen.width/16);
			Rect stringPosition = new Rect(Screen.width/4.5f, Screen.height -Screen.height/7f,Screen.width/5,Screen.width/16);
			if(!chooseTargetBool)
			{
				switch (battleOrderIndex)
				{
				case 0:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				case 1:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				case 2:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				case 3:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				case 4:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				case 5:
					for (int i = 0; i < shipScript.characterAttacks.Length; i++)
					{
						if(GUI.Button(buttonPosition,buttonImageUI,buttonStyle))
						{
							if(shipScript.ammo - shipScript.characterAttackAmmoCost[i] >= 0)
							{
								chooseTargetBool = true;
								DestroyObject(targetParticleOBJ);
								targetParticleOBJ = Instantiate(targetParticle) as GameObject;
								activeCharacterIndex = i;
							}
						}
						GUI.Label(stringPosition,shipScript.characterAttacks[i]+ " -- " + shipScript.characterAttackAmmoCost[i].ToString());
						
						if(i==1)
						{
							buttonPosition.x =  buttonPosition.x - buttonPosition.width - buttonBuffer;
							stringPosition.x =  stringPosition.x - stringPosition.width - buttonBuffer;
							buttonPosition.y =  buttonPosition.y + buttonPosition.height;
							stringPosition.y =  stringPosition.y + stringPosition.height;
						}
						else
						{
							buttonPosition.x =  buttonPosition.x + buttonPosition.width + buttonBuffer;
							stringPosition.x =  stringPosition.x + stringPosition.width + buttonBuffer;
						}
					}
					GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
					break;
				default:
					print("Default case");
					break;
				}
			}
			else
			{
				Rect targetRect = new Rect(Screen.width/3, Screen.height -Screen.height/8,Screen.width/2,Screen.width/16);
				GUI.Label(targetRect,battleOrder[targetIndex].GetComponent<SpaceShipScript>().shipName + " - " + battleOrder[targetIndex].GetComponent<SpaceShipScript>().armor + " Armor");
			}
			GUI.Label(new Rect(Screen.width - Screen.width/12, Screen.height -Screen.height/18f,Screen.width/5,Screen.width/16), shipScript.ammo.ToString());
		}
////ENEMY UI STARTS HERE/////////////
		else
		{
			GUI.Label(new Rect(0,Screen.height - Screen.height/6,Screen.width,Screen.height/5),enemyBackgroundUI);
			Rect stringPosition = new Rect(Screen.width/2.1f, Screen.height -Screen.height/16f,Screen.width/5,Screen.width/16);
			GUI.Label(stringPosition, enemyAttackChoice);
		}
////ENEMY UI ENDS HERE/////////////
	}

	void callAttack(string attackName, int attackCost, int target)
	{
		if(attackName == "Attack")
		{
			attackScript.Attack(battleOrder[battleOrderIndex].transform, battleOrder[target].transform);

		}
		else if(attackName == "Volley")
		{
			attackScript.Volley(battleOrder[battleOrderIndex].transform, battleOrder[target].transform);

		}
		else if(attackName == "Hammer Strike")
		{
			attackScript.HammerStrike(battleOrder[battleOrderIndex].transform, battleOrder[target].transform);
		}
		else if(attackName == "Broadside")
		{
			attackScript.Broadside(battleOrder[battleOrderIndex].transform, battleOrder[target].transform);
		}
		shipScript.ammo -= attackCost;

		
		
	}


	public void nextCharacterTurn()
	{
		if(battleOrderIndex < 5)
			battleOrderIndex++;
		else
			battleOrderIndex =0;
		shipScript = battleOrder[battleOrderIndex].GetComponent<SpaceShipScript>();
		if(shipScript.disabled)
			nextCharacterTurn();
		enemyIsActing = false;



	}

	//ENEMY ATTACK TRANSITIONS HANDLED HERE

	IEnumerator enemyAttackStageOne()
	{
		//SELECT ATTACK OPTION
		yield return new WaitForSeconds(2);
		int attackChoice;
		if(shipScript.ammo > 0)
			attackChoice = Random.Range(0,shipScript.characterAttacks.Length);
		else
			attackChoice = 0;
		enemyAttackChoice = shipScript.characterAttacks[attackChoice];
		activeCharacterIndex = attackChoice;
		StartCoroutine(enemyAttackStageTwo(enemyAttackChoice));
	}
	IEnumerator enemyAttackStageTwo(string _attackName)
	{
		yield return new WaitForSeconds(1);
		//SELECT TARGET
		targetIndex = Random.Range(0, battleOrder.Length);
		while(battleOrder[targetIndex].GetComponent<SpaceShipScript>().isEnemy == true || battleOrder[targetIndex].GetComponent<SpaceShipScript>().disabled == true)
		{
			targetIndex = Random.Range(0, battleOrder.Length);
		}
		callAttack(_attackName,shipScript.characterAttackAmmoCost[activeCharacterIndex], targetIndex);
		enemyAttackChoice = "";
	}

	void sendReferenceShipToGUI(GameObject x)
	{
		GameObject g = GameObject.FindGameObjectWithTag("GUIlayer");
		g.GetComponent<Gui_Battle>().setSpaceShips(x);
	}

	public void shipTakeDamage(int mod)
	{
		battleOrder[targetIndex].GetComponent<SpaceShipScript>().armor -= shipScript.damage * mod;
		if(battleOrder[targetIndex].GetComponent<SpaceShipScript>().armor <= 0)
			battleOrder[targetIndex].GetComponent<SpaceShipScript>().disable();
		//CHECK HERE IF GAME IS OVER
		checkEndGame();
	}

	public void loadCharacterShips()
	{

		//THIS GAMEOBJECT HELPS FACE MODELS TO CENTER OF BATTLE
		GameObject ft = GameObject.FindGameObjectWithTag("FacingTargetM");
		//WILL NEED TO FIND WAY TO LOAD PLAYER PARTY
		for(int i = 0; i < 3;i++)
		{
			aShip ships = GameManagerScript.battleShips[i];
			if(ships.type == "Frigate")
			{
				playerParty[i] = Instantiate(Resources.Load("Frigate_Player"),characterLocations[i].position,Quaternion.identity) as GameObject;
			}
			else if(ships.type == "Destroyer")
			{
				playerParty[i] = Instantiate(Resources.Load("Destroyer_Player"),characterLocations[i].position,Quaternion.identity) as GameObject;
			}
			else if(ships.type == "Cruiser")
			{
				playerParty[i] = Instantiate(Resources.Load("Cruiser_Player"),characterLocations[i].position,Quaternion.identity) as GameObject;
			}
			SpaceShipScript myShip = playerParty[i].GetComponent<SpaceShipScript>();
			myShip.ammo = ships.ammo;
			myShip.maxAmmo = ships.maxAmmo;
			myShip.armor = ships.armor;
			myShip.maxArmor = ships.maxArmor;
			myShip.damage = ships.damage;
			playerParty[i].transform.LookAt(ft.transform.position);
			battleOrder[i] = playerParty[i];
			sendReferenceShipToGUI(playerParty[i]);
		}




	}

	void checkEndGame()
	{
		//CHECK TO SEE IF PLAYER SHIPS ARE ALL DISABLED
		if(battleOrder[0].GetComponent<SpaceShipScript>().disabled == true && battleOrder[1].GetComponent<SpaceShipScript>().disabled == true && battleOrder[2].GetComponent<SpaceShipScript>().disabled == true)
		{
			//ENEMY WINS
			GameManagerScript.gold += 10;
			Application.LoadLevel("WorldScene");
		}
		else if(battleOrder[3].GetComponent<SpaceShipScript>().disabled == true && battleOrder[4].GetComponent<SpaceShipScript>().disabled == true && battleOrder[5].GetComponent<SpaceShipScript>().disabled == true)
		{
			//PLAYER WINS
			GameManagerScript.gold += 30;
			Application.LoadLevel("WorldScene");
		}
	}

	void checkMouseOverShip(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;		
		if(Physics.Raycast(ray, out hit) && hit.collider.tag == "ship")
		{
			GameObject g = hit.collider.gameObject;
			for (int i = 0; i < battleOrder.Length; i++)
			{
				if( battleOrder[i].Equals(g))
				{
					targetIndex = i;
				}
			}
			if(Input.GetMouseButtonDown(0))
			{
				if(!battleOrder[targetIndex].GetComponent<SpaceShipScript>().disabled)
				{
					//ATTACK IS SENT OUT FROM HERE
					callAttack(shipScript.characterAttacks[activeCharacterIndex],shipScript.characterAttackAmmoCost[activeCharacterIndex],targetIndex);
					DestroyObject(targetParticleOBJ);
					chooseTargetBool = false;
				}
			}
			//Debug.Log(hit.collider.gameObject.name);
		}
	}
}
