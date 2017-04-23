using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
	public const float PATROL_SPEED = 10;
	public const float HUNT_SPEED = 15;
	public const float RETURN_SPEED = 5;
	public enum Behaviour {
		Patrolling,
		Hunting,
		Attacking,
		Returning
	}

	public Behaviour currentBehaviour;
	public Vector3 initialLocation;
	public Vector3 currentLocation;
	public Vector3 previousLocation;
	public float huntingRange = 20;
	public Vector2 startDirection;
	
	public void Start() {
		currentBehaviour = Behaviour.Patrolling;
		initialLocation = transform.position;

		currentLocation = initialLocation;
		previousLocation = initialLocation;

		SetPatrol();
	}

	public void FixedUpdate() {
		transform.rotation = Sprites.rotateFacing(GetComponent<Rigidbody2D>().velocity, -90f);

		var playerPosition = GameObject.Find("MousePlayer").transform.position;
		var distance = Vector2.Distance(playerPosition, transform.position);

		previousLocation = currentLocation;
		currentLocation = transform.position;

		var velocity = GetComponent<Rigidbody2D>().velocity;

		switch (currentBehaviour) {
			case Behaviour.Attacking:
				// The player is right next to the AI, attack!
				// Some sort of animation change? Probably an attack cooldown.
				break;
			case Behaviour.Hunting:
				// The player is nearby, search them out
				if (distance > huntingRange)
				{
					currentBehaviour = Behaviour.Returning;
				}

				velocity = (Vector2)(playerPosition - currentLocation).normalized * HUNT_SPEED;

				break;
			case Behaviour.Patrolling:
				// The player is nowhere near, just do your thing.
				if (distance < huntingRange)
				{
					currentBehaviour = Behaviour.Hunting;
					break;
				}

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
				if (distance < huntingRange)
				{
					currentBehaviour = Behaviour.Hunting;
					break;
				}

				if (Vector2.Distance(currentLocation, initialLocation) < 1)
				{
					currentBehaviour = Behaviour.Patrolling;
					velocity = startDirection;
					break;
				}

				// If we're not making any progress, just patrol here...
				velocity = (Vector2)(initialLocation - currentLocation).normalized * RETURN_SPEED;

				break;
			default:
				break;
		}

		GetComponent<Rigidbody2D>().velocity = velocity;
		Debug.Log($"Currently: {currentBehaviour}");
	}

	public void OnCollisionEnter2D(Collision2D col) {
		switch (currentBehaviour) {
			case Behaviour.Patrolling:
				GetComponent<Rigidbody2D>().velocity = col.relativeVelocity * -1;
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
