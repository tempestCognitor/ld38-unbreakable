using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float speed;
	public Vector2 facing;

	// Use this for initialization
	void Start () {
		speed = 20;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var verticalInput = Input.GetAxisRaw("Vertical");
		var horizInput = Input.GetAxisRaw("Horizontal");
		facing = new Vector2(horizInput, verticalInput);

		foreach(Transform child in transform)
		{
			if(facing.magnitude == 0)
			{
				break;
			}

			if(child.name == transform.name)
			{
				continue;
			}

			child.rotation = Sprites.rotateFacing(child.rotation, facing, -90f, 50);
		}

		GetComponent<Rigidbody2D>().velocity = facing * speed;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "KeyTrigger") {
			var key = GameObject.Find("Key");
			key.transform.parent = transform;
			key.transform.position = transform.position + Vector3.down;
			col.GetComponent<Collider2D>().enabled = false;
		}
	}
}
