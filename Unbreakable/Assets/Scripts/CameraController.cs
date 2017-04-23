using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public GameObject background;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		var backgroundSize = background.GetComponent<BoxCollider2D>();
		var camera = GetComponent<Camera>();

		var newPos = player.transform.position + offset;

        // ortographicSize is the haldf of the height of the Camera.
        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);
 
        newPos.x = Mathf.Clamp(newPos.x, backgroundSize.bounds.min.x + cameraHalfWidth, backgroundSize.bounds.max.x - cameraHalfWidth);
        newPos.y = Mathf.Clamp(newPos.y, backgroundSize.bounds.min.y + camera.orthographicSize, backgroundSize.bounds.max.y - camera.orthographicSize);

		transform.position = newPos;
	}
}
