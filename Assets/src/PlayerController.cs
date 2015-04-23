using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 6f;
	public float jumpForce = 14f;

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	private bool grounded = false;
	public Transform groundCheck;

	private Animator animator;
	private Rigidbody2D body;

	void Awake () {
		animator = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		int layer = LayerMask.NameToLayer ("Ground");
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << layer);
		
		if (Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate () {
		float move = Input.GetAxis("Horizontal");

		animator.SetFloat("speed", Mathf.Abs(move));

		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);

		if (move > 0 && !facingRight)
			Flip();
		else if (move <= 0 && facingRight)
			Flip();

		if (jump) {
			jump = false;
			animator.SetTrigger("Jump");
			body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
