using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public const float ATTACK_POWER = 50000;
	public float speed;
	public Vector2 facing;
	public GameObject trigger;
	private float timeLeft;

	// Use this for initialization
	public void Start () {
		speed = 20;
	}

	public void Update() {
		timeLeft -= Time.deltaTime;
  
        if(timeLeft <= 0)
        {
        }
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
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

		GetComponent<Rigidbody2D>().velocity = (facing * speed);
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if (new Regex(@"KeyTrigger(.*)?").Match(col.gameObject.name).Success)
		{
			var key = GameObject.Find("Key");
			key.transform.parent = transform;
			key.transform.position = transform.position + Vector3.down * 1.5f;

			col.GetComponent<Collider2D>().enabled = false;
		}
		else if (col.gameObject.name == "EndTrigger") {
			foreach(Transform t in transform) {
				if(t.name == "Key"){
					SwitchPlayerBody(col);
					return;
				}
			}

			var pushBack = col.transform.position - transform.position;
			pushBack = pushBack.normalized * -ATTACK_POWER;
			GetComponent<Rigidbody2D>().AddForce(pushBack);
			GetComponent<AudioSource>().Play();
		}
		else if (col.gameObject.name == "Door-Open") {
			foreach(Transform t in transform) {
				if(t.name == "EndTrigger"){
					GameObject.Find("WinningSplash").GetComponent<SpriteRenderer>().enabled = true;
					return;
				}
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {
		var match = new Regex(@"CatBaddie(.*)?").Match(col.gameObject.name);
		
		if (match.Success) {
			var pushBack = col.contacts[0].point - (Vector2)transform.position;
			pushBack = pushBack.normalized * -ATTACK_POWER;
			GetComponent<Rigidbody2D>().AddForce(pushBack);

			DropKey(col.transform.position);

			GetComponent<AudioSource>().Play();
		}
	}

	private void SwitchPlayerBody(Collider2D col) {
		var mouse = GameObject.Find("Mouse");

		mouse.transform.parent = null;
		col.transform.parent = transform;

		GetComponent<CircleCollider2D>().radius = 3;
		GetComponent<CircleCollider2D>().offset = new Vector2(2,2);
	}

	private void DropKey(Vector3 location) {
		foreach(Transform child in transform) {
			if(child.gameObject.name == "Key") {
				child.parent = null;
				child.position = location;

				var keyTrigger = Instantiate(trigger, location, Quaternion.identity) as GameObject;
				keyTrigger.GetComponent<Collider2D>().enabled = true;
			}
		}
	}
}
