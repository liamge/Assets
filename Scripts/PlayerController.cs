using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;

	public bool grounded = false;

	public Transform dustEffect;

	private Animator anim;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;

	private float prevH = 0f;
	private float xOffset = -0.209f;
	private float yOffset = -0.216f;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate () 
	{				
		float h = Input.GetAxisRaw ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (h));
		anim.SetBool ("Grounded", grounded);

		if (h < 0)
			spriteRenderer.flipX = true;
		else if (h > 0)
			spriteRenderer.flipX = false;

		if (Input.GetKey (KeyCode.DownArrow)) {
			anim.SetBool ("Crouching", true);
			return;
		} else {
			anim.SetBool ("Crouching", false);
		}

		var moveDir = new Vector3 (h, 0f, 0f); 
		Move (moveDir);

		if (Mathf.Abs (h - prevH) > 1f && grounded) {
			SpawnDust (h);
		}

		prevH = h;
	}

	public void Move (Vector3 moveDir)
	{
		transform.position += moveDir * speed * Time.deltaTime;
	}

	void SpawnDust(float h)
	{
		if (h < 0)
			dustEffect.GetComponent<SpriteRenderer>().flipX = true;
		else if (h > 0)
			dustEffect.GetComponent<SpriteRenderer>().flipX = false;

		Vector3 newTransform = new Vector3(transform.position.x + xOffset * h, transform.position.y + yOffset, transform.position.z);
		Instantiate (dustEffect, newTransform, Quaternion.identity);
	}
}
