using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 20;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var velocity = GetComponent<Rigidbody2D>().velocity;

		if (velocity.magnitude != 0)
		{
			transform.rotation = Sprites.rotateFacing(velocity, -90f);
		}

		var verticalInput = Input.GetAxisRaw("Vertical");
		var horizInput = Input.GetAxisRaw("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2(horizInput, verticalInput) * speed;
	}
}
