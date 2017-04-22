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
}
