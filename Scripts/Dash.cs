using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	public float maxDashTime = 1.0f;
	public float dashSpeed = 1.0f;
	public float dashStopSpeed = 0.1f;

	[Range(0, 10)]
	public float shakeAmount;
	[Range(0, 10)]
	public float shakeDuration;

	public Transform groundDashEffect;
	public Transform airDashEffect;

	private PlayerController controller;
	private Animator anim;

	private float currentDashTime;
	private bool dashReset = false;
	private float xOffset = -0.67f;
	private float yOffset = -0.218f;

	void Awake () 
	{
		controller = GetComponent<PlayerController> ();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (controller.grounded) 
		{
			dashReset = true;
		}
	}
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 moveDir = Vector3.zero;

		if (Input.GetKeyDown (KeyCode.LeftShift) && dashReset && h != 0)
		{
			dashReset = false;
			anim.SetTrigger ("Dash");
			currentDashTime = 0.0f;
			DashEffects ();
		}

		if (currentDashTime < maxDashTime)
		{
			moveDir = new Vector3 (h, v, 0) * dashSpeed;
			currentDashTime += dashStopSpeed;
		}

		controller.Move (moveDir);
	}

	void DashEffects ()
	{
		Camera.main.GetComponent<CameraShake> ().shakeAmount = shakeAmount;
		Camera.main.GetComponent<CameraShake> ().shakeDuration = shakeDuration;

		float h = Input.GetAxis ("Horizontal");

		if (h < 0) {
			groundDashEffect.GetComponent<SpriteRenderer> ().flipX = true;
			airDashEffect.GetComponent<SpriteRenderer> ().flipX = true;
		} else if (h > 0) {
			groundDashEffect.GetComponent<SpriteRenderer>().flipX = false;
			airDashEffect.GetComponent<SpriteRenderer> ().flipX = false;
		}

		Vector3 newTransform = new Vector3(transform.position.x + xOffset * h, transform.position.y + yOffset, transform.position.z);
		if (controller.grounded)
			Instantiate (groundDashEffect, newTransform, Quaternion.identity);
		else {
			newTransform.y -= 0.2f;
			Instantiate (airDashEffect, newTransform, Quaternion.identity);
		}
	}
}
