using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	[Range(0, 10)]
	public float jumpForce = 1f;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	public float downButtonFallMultiplier = 1.5f;

	public Transform dustEffect;

	private float yOffset = -0.278f;

	private Animator anim;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private PlayerController controller;

	void Awake () 
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		controller = GetComponent<PlayerController> ();
	}

	void FixedUpdate () 
	{
		float v = rb.velocity.y;

		anim.SetFloat ("vSpeed", v);

		if (v < 0 && !Input.GetKey(KeyCode.DownArrow))
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		else if (v < 0 && Input.GetKey(KeyCode.DownArrow))
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * downButtonFallMultiplier * Time.deltaTime;
		else if (v > 0 && !Input.GetButton("Jump"))
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Space) && controller.grounded) {
			rb.velocity = Vector2.up * jumpForce;
			JumpEffect ();
		}
	}

	void JumpEffect ()
	{
		Vector3 newTransform = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
		Instantiate (dustEffect, newTransform, Quaternion.identity);
	}
}
