using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour {

	Animator anim;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		Destroy (gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
	}
}
