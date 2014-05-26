using UnityEngine;
using System.Collections;

public class GizmoSphereScript : MonoBehaviour {

	public float drawRadius;
	public bool isEnemy;

	void OnDrawGizmos() {
		if(isEnemy)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, drawRadius);
	}
}
