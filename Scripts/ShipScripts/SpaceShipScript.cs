using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShipScript : MonoBehaviour {

	public string shipName;

	//BASE ATTRIBUTES
	public float damage;
	[HideInInspector]
	public float armor;
	public float speed;
	[HideInInspector]
	public float ammo;
	public float maxArmor;
	public float maxAmmo;

	[HideInInspector]
	public bool disabled = false;


	public bool isEnemy;

	//List of Character Moves
	public string[] characterAttacks;
	public int[]characterAttackAmmoCost;

	// Use this for initialization
	void Start () {
		ammo = maxAmmo;
		armor = maxArmor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void disable()
	{
		this.disabled = true;
		armor = 0;
		ammo = 0;
		this.renderer.enabled = false;
	}
}
