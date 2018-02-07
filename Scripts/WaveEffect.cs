using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffect : MonoBehaviour {

	public float effectSize;
	public float growRate;

	void Update () 
	{
		if (transform.localScale.x < effectSize) 
		{
			Grow ();
		} else if (transform.localScale.x > effectSize)
		{
			Destroy(gameObject);
		}
	}

	public void Grow ()
	{
		transform.localScale += Vector3.one * growRate;
	}
}
