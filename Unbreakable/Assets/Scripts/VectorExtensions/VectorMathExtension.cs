using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorMathExtensions {
	public static Vector2 AddConstant(this Vector2 a, float f) {
		return new Vector2(a.x + f, a.y + f);
	}

	public static Vector2 AddConstant(this Vector2 a, int i) {
		return new Vector2(a.x + i, a.y + i);
	}

    private static Vector2[] cardinals = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};

	public static Vector2 ClampCardinal(this Vector2 angle) {
        float minAngle = 90;
        Vector2 minCard = cardinals[0];

        foreach(var cardinal in cardinals) {
            var currAngle = Vector2.Angle(angle, cardinal);

            if (currAngle < minAngle) {
                minAngle = currAngle;
                minCard = cardinal;
            }
        }
        
        return minCard;
    }
}
