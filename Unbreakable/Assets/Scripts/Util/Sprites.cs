using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites {
	public static Quaternion rotateFacing(Vector2 facing, float offset) {
		return Quaternion.AngleAxis(
			Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg + offset,
			Vector3.forward
		);

	}
}
