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
	public float huntingRange = 10;
	public Vector2 startDirection;

    private Vector2 facing;
	
	public void Start() {
		currentBehaviour = Behaviour.Patrolling;
		initialLocation = transform.position;

		currentLocation = initialLocation;
		previousLocation = initialLocation;

		startDirection = startDirection.normalized;

		GetComponent<Rigidbody2D>().velocity = SetPatrol();
	}

	public void FixedUpdate()
    {	
        UpdateFacing();

        var playerPosition = GameObject.Find("MousePlayer").transform.position;

        previousLocation = currentLocation;
        currentLocation = transform.position;

        var velocity = GetComponent<Rigidbody2D>().velocity;

        switch (currentBehaviour)
        {
            case Behaviour.Attacking:
                // The player is right next to the AI, attack!
                // Some sort of animation change? Probably an attack cooldown.
                break;
            case Behaviour.Hunting:
                // The player is nearby, search them out
                if (!SearchForPlayer(playerPosition, huntingRange * 3/4))
                {
                    currentBehaviour = Behaviour.Returning;
                }

                velocity = (Vector2)(playerPosition - currentLocation).normalized * HUNT_SPEED;

                break;
            case Behaviour.Patrolling:
                // The player is nowhere near, just do your thing.
                if (SearchForPlayer(playerPosition, huntingRange))
                {
                    initialLocation = currentLocation;
                    startDirection = Vector2.Angle(velocity, startDirection) < 90
						? startDirection
						: startDirection * -1;
                    currentBehaviour = Behaviour.Hunting;
                    break;
                }

                if (velocity.magnitude < PATROL_SPEED)
                {
                    if (velocity.magnitude == 0)
                    {
                        velocity = startDirection.normalized * PATROL_SPEED;
                    }
                    else
                    {
                        velocity = velocity.normalized * PATROL_SPEED;
                    }
                }
                else if (velocity.magnitude > PATROL_SPEED * 1.5)
                {
                    velocity = velocity.normalized * PATROL_SPEED;
                }

                break;
            case Behaviour.Returning:
                // You're not where you started, and there's no reason not to be
                if (SearchForPlayer(playerPosition, huntingRange))
                {
                    currentBehaviour = Behaviour.Hunting;
                    break;
                }

                if (Vector2.Distance(currentLocation, initialLocation) < 1)
                {
                    currentBehaviour = Behaviour.Patrolling;
					velocity = SetPatrol();
                    break;
                }

                // If we're not making any progress, just patrol here...
                velocity = (Vector2)(initialLocation - currentLocation).normalized * RETURN_SPEED;

                break;
            default:
                break;
        }

        Debug.Log($"State: {currentBehaviour}, Vel: {velocity}");

        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void UpdateFacing() {
        foreach(Transform child in transform)
        {
            if(child.name == transform.name)
            {
                continue;
            }

            child.rotation = Sprites.rotateFacing(child.rotation, GetComponent<Rigidbody2D>().velocity, -90f, 10);
        }
    }

    public void OnCollisionEnter2D(Collision2D col) {
		switch (currentBehaviour) {
			case Behaviour.Patrolling:
            case Behaviour.Returning:
				if (col.gameObject.name != "MousePlayer")
				{
					GetComponent<Rigidbody2D>().velocity = (col.transform.position - transform.position) * -1;
				}
				break;
			case Behaviour.Hunting:
				if (col.gameObject.name == "MousePlayer")
				{
					// Kill the player
				}
				break;
            default:
				break;
		}
	}

    private bool SearchForPlayer(Vector3 playerPosition, float range)
    {
		var playerDirection = playerPosition - transform.position;

		if(Vector2.Angle(GetComponent<Rigidbody2D>().velocity, playerDirection) > 60) {
			return false;
		}

        var rayHit = Physics2D.Raycast(transform.position, playerDirection.normalized, range);
        var playerNearby = false;

        if (rayHit.collider != null && rayHit.collider.gameObject.name == "MousePlayer")
        {
            playerNearby = true;
        }

        return playerNearby;
    }

	private Vector2 SetPatrol() {
		var move = (startDirection.x != 0 && startDirection.y != 0
			? startDirection
			: Vector2.right)
			* PATROL_SPEED;

		return move;
	}
}
