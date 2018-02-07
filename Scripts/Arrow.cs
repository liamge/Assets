using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Vector3 fv;

	void Start()
	{
		GetComponent<Rigidbody2D>().AddForce(fv, ForceMode2D.Impulse);
	}

	void FixedUpdate()
	{
		Vector3 dir = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
