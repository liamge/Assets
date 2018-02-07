using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour {

	PlayerController controller;

	void Awake () 
	{
		controller = transform.parent.GetComponent<PlayerController> ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Ground")
			controller.grounded = true;
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Ground")
			controller.grounded = true;
	}

	void OnTriggerExit2D ()
	{
		controller.grounded = false;
	}
}
