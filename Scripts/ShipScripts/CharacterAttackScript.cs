using UnityEngine;
using System.Collections;

public class CharacterAttackScript:MonoBehaviour {
	//REFERENCES OF GAME OBJECTS
	public GameObject attackProjectile;
	public GameObject hammerStrikeProjectile;
	public GameObject chargeParticle;
	//INSTANCES OF GAMEOBJECTS
	GameObject _attackProjectile_1;
	GameObject _attackProjectile_2;
	GameObject _attackProjectile_3;
	GameObject _chargeParticle;
	public GameObject battleManger;
	BattleManagerScript bManagerScript;
	public GameObject explosionParticle;

	public AudioClip[] _audioClips;

	Quaternion originalRotation_1;

	void Start()
	{
		bManagerScript = battleManger.GetComponent<BattleManagerScript>();
	}

	public void Broadside(Transform player, Transform target)
	{
		Quaternion lookTarget = Quaternion.LookRotation(target.position, player.position);
		originalRotation_1 = player.transform.rotation;
		StartCoroutine(checkBroadsideProjectile(player, target,lookTarget));
	}

	public void HammerStrike(Transform player, Transform target)
	{
		_chargeParticle = Instantiate(chargeParticle, player.position, Quaternion.identity) as GameObject;
		StartCoroutine(checkHammerStrikeProjectile(player, _chargeParticle, target));
	}

	public void Attack(Transform player, Transform target)
	{
		_attackProjectile_1 = Instantiate(attackProjectile, player.position,Quaternion.identity) as GameObject;
		audio.clip = _audioClips[2];
		audio.Play();
		//SPEED OF PROJECTILE SET HERE
		_attackProjectile_1.rigidbody.AddForce((target.position - player.position)* 80);
		StartCoroutine(checkAttackProjectile(_attackProjectile_1, target));
	}

	public void Volley(Transform player, Transform target)
	{
		_attackProjectile_1 = Instantiate(attackProjectile, player.position,Quaternion.identity) as GameObject;
		_attackProjectile_2 = Instantiate(attackProjectile, player.position,Quaternion.identity) as GameObject;
		_attackProjectile_3 = Instantiate(attackProjectile, player.position,Quaternion.identity) as GameObject;
		//SPEED OF PROJECTILE SET HERE
		_attackProjectile_1.rigidbody.AddForce((target.position - player.position)* 80);
		audio.clip = _audioClips[2];
		audio.Play();
		_attackProjectile_2.rigidbody.AddForce((target.position - player.position)* 60);
		_attackProjectile_3.rigidbody.AddForce((target.position - player.position)* 40);audio.clip = _audioClips[2];
		StartCoroutine(checkVolleyProjectiles(_attackProjectile_1,_attackProjectile_2, _attackProjectile_3, target));
	}

	IEnumerator checkBroadsideProjectile(Transform _player, Transform _target, Quaternion _targetLookAt)
	{
		float timer = 0;
		while(timer <= 2f)
		{
			timer += Time.deltaTime;
			_player.transform.rotation = Quaternion.Lerp(_player.transform.rotation,new Quaternion(_player.rotation.x,_targetLookAt.y, _player.rotation.z, _player.rotation.w),Time.deltaTime);
			yield return null;
		}
		StartCoroutine(checkBroadsideProjectile_2(_player,_target,_targetLookAt));

	}

	IEnumerator checkBroadsideProjectile_2(Transform _player, Transform _target, Quaternion _targetLookAt)
	{
		_attackProjectile_1 = Instantiate(attackProjectile, _player.position,Quaternion.identity) as GameObject;
		audio.clip = _audioClips[2];
		audio.Play();
		//SPEED OF PROJECTILE SET HERE
		_attackProjectile_1.rigidbody.AddForce((_target.position - _player.position)* 80);
		StartCoroutine(checkBroadsideProjectile_3(_attackProjectile_1,_target,_player,_targetLookAt));
		yield return null;
	}
	IEnumerator checkBroadsideProjectile_3(GameObject projectile, Transform targetDestination, Transform _player, Quaternion _targetLookAt)
	{
		while(Vector3.Distance(projectile.transform.position, targetDestination.position) > 0.5f)
		{
			yield return null;
		}
		shipHit(projectile.transform);
		GameObject.DestroyObject(projectile);
		bManagerScript.shipTakeDamage(3);
		StartCoroutine(checkBroadsideProjectile_4(_player,_targetLookAt));

	}

	IEnumerator checkBroadsideProjectile_4(Transform _player, Quaternion _targetLookAt)
	{
		float timer = 0;
		//Debug.Log(Vector3.Distance(projectile.transform.position, targetDestination.position));
		while(timer <= 2f)
		{
			timer += Time.deltaTime;
			_player.transform.rotation = Quaternion.Lerp(_player.transform.rotation,new Quaternion(_player.rotation.x,originalRotation_1.y, _player.rotation.z, _player.rotation.w),Time.deltaTime);
			yield return null;
		}
		bManagerScript.nextCharacterTurn();
		
	}

	IEnumerator checkHammerStrikeProjectile(Transform _player, GameObject _particle, Transform _target)
	{
		yield return new WaitForSeconds(1.0f);
		GameObject.DestroyObject(_particle);
		_attackProjectile_1 = Instantiate(hammerStrikeProjectile, _player.position,Quaternion.identity) as GameObject;
		audio.clip = _audioClips[0];
		audio.Play();
		_attackProjectile_1.rigidbody.AddForce((_target.position - _player.position)* 80);
		StartCoroutine(checkHammerStrikeProjectile_2(_attackProjectile_1,_target));

	}

	IEnumerator checkHammerStrikeProjectile_2(GameObject _projectile, Transform _target)
	{
			while(Vector3.Distance(_projectile.transform.position, _target.position) > 0.5f)
		{
			yield return null;
		}
		shipHit(_projectile.transform);
		GameObject.DestroyObject(_projectile);
		bManagerScript.shipTakeDamage(4);
		bManagerScript.nextCharacterTurn();
	}

	IEnumerator checkAttackProjectile(GameObject projectile, Transform targetDestination)
	{
		while(Vector3.Distance(projectile.transform.position, targetDestination.position) > 0.5f)
		{
			yield return null;
		}
		shipHit(projectile.transform);
		GameObject.DestroyObject(projectile);
		bManagerScript.shipTakeDamage(2);
		bManagerScript.nextCharacterTurn();
	}

	IEnumerator checkVolleyProjectiles(GameObject projectile1, GameObject projectile2, GameObject projectile3, Transform targetDestination)
	{
		while(Vector3.Distance(projectile3.transform.position, targetDestination.position) > 0.5f)
		{
			if(Vector3.Distance(projectile2.transform.position,targetDestination.position) < 0.5f)
			{
				projectile2.renderer.enabled = false;
				shipHit(projectile2.transform);
			}
			if(Vector3.Distance(projectile1.transform.position,targetDestination.position) < 0.5f)
			{
				projectile1.renderer.enabled = false;
				shipHit(projectile1.transform);
			}
			yield return null;
		}
		shipHit(projectile3.transform);
		GameObject.DestroyObject(projectile1);
		GameObject.DestroyObject(projectile2);
		GameObject.DestroyObject(projectile3);
		bManagerScript.shipTakeDamage(3);
		bManagerScript.nextCharacterTurn();
	}

	void shipHit(Transform loc)
	{
		Instantiate(explosionParticle, loc.position,Quaternion.identity);
		audio.clip = _audioClips[1];
		audio.Play();

	}
}
