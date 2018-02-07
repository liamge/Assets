using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFire : MonoBehaviour {

	[Range(0, 10)]
	public float fireRate;
	[Range(0, 10)]
	public float shakeAmount;
	[Range(0, 10)]
	public float shakeDuration;
	[Range(0, 10)]
	public float waveSize;
	[Range(0, 10)]
	public float waveGrowRate;

	public WaveEffect waveEffect;

	private Animator anim;
	private PlayerController controller;

	private float lastShot;

	void Awake () 
	{
		anim = GetComponent<Animator> ();
		controller = GetComponent<PlayerController> ();
	}
	
	void FixedUpdate () 
	{
		if (Input.GetButton ("Fire1") && !anim.GetBool("Crouching") && controller.grounded) 
		{
			anim.SetBool ("Firing", true);
			Camera.main.GetComponent<CameraShake> ().shakeAmount = 0.03f;
			Camera.main.GetComponent<CameraShake> ().shakeDuration = 999f;
		}
		if (Input.GetButtonUp ("Fire1") && !anim.GetBool("Crouching") && Time.time > fireRate + lastShot && controller.grounded) 
		{
			lastShot = Time.time;
			anim.SetBool ("Firing", false);
			FireEffects ();
		}
	}

	void FireEffects ()
	{
		WaveEffect wEffect = Instantiate (waveEffect, transform.position, Quaternion.identity);
		wEffect.effectSize = waveSize;
		wEffect.growRate = waveGrowRate;
		Camera.main.GetComponent<CameraShake> ().shakeAmount = shakeAmount;
		Camera.main.GetComponent<CameraShake> ().shakeDuration = shakeDuration;
	}
}
