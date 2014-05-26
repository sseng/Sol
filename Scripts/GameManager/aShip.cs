using UnityEngine;
using System.Collections;

public class aShip{

	public 	GameObject 	ship;
	public 	string 		type;
	public 	float 		armor;
	public 	float 		maxArmor;
	public 	float 		ammo;
	public 	float 		maxAmmo;
	public 	float		damage;

	public aShip(){
		this.type = "Cruiser";
		this.armor = 100;
		this.maxArmor = 100;
		this.ammo = 10;
		this.maxAmmo = 10;
		this.damage = 5;
	}
	public aShip(string type, float armor, float maxArmor, float ammo, float maxAmmo, float damage){
		this.type = type;
		this.armor = armor;
		this.maxArmor = maxArmor;
		this.ammo = ammo;
		this.maxAmmo = maxAmmo;
		this.damage = damage;
	}
}
