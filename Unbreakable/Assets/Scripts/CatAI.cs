using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
	public const float PATROL_SPEED = 10;
	public enum Behaviour {
		Patrolling,
		Hunting,
		Attacking,
		Returning
	}

	public Behaviour currentBehaviour;
	public Vector2 initialLocation;
	public float huntingRange = 20;
	public Vector2 startDirection;
	
	public void Start() {
		currentBehaviour = Behaviour.Patrolling;
		initialLocation = transform.position;

		SetPatrol();
	}

	public void FixedUpdate() {
		transform.rotation = Sprites.rotateFacing(GetComponent<Rigidbody2D>().velocity, -90f);

		switch (currentBehaviour) {
			case Behaviour.Attacking:
				// The player is right next to the AI, attack!
				// Some sort of animation change? Probably an attack cooldown.
				break;
			case Behaviour.Hunting:
				// The player is nearby, search them out
				break;
			case Behaviour.Patrolling:
				// The player is nowhere near, just do your thing.
				var playerPosition = GameObject.Find("Mouse").transform.position;
				var distance = Vector2.Distance(playerPosition, transform.position);

				if (distance < huntingRange)
				{
					currentBehaviour = Behaviour.Hunting;
					return;
				}

				var velocity = GetComponent<Rigidbody2D>().velocity;

				if (velocity.magnitude < PATROL_SPEED)
				{
					velocity = velocity.normalized * PATROL_SPEED;
				}
				else if (velocity.magnitude > PATROL_SPEED * 1.5)
				{
					velocity = velocity.normalized * PATROL_SPEED;
				}

				break;
			case Behaviour.Returning:
				// You're not where you started, and there's no reason not to be
				break;
			default:
				break;
		}

		Debug.Log(currentBehaviour);
	}

	public void OnCollisionEnter2D(Collision2D col) {
		var rb = GetComponent<Rigidbody2D>();

		switch (currentBehaviour) {
			case Behaviour.Patrolling:
				break;
            default:
				break;
		}
	}

	private void SetPatrol() {
		GetComponent<Rigidbody2D>().velocity = (startDirection.x != 0 && startDirection.y != 0
			? startDirection
			: Vector2.right)
			* PATROL_SPEED;
	}
}
