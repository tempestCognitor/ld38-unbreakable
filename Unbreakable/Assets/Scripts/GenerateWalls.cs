using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWalls : MonoBehaviour {

	public BoxCollider2D region;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		CreateWalls(region);		
	}
	
    public void CreateWalls(BoxCollider2D background)
    {
		var random = new System.Random();

        var backgroundSize = background.bounds.size;
        var tileSize = wall.GetComponent<Renderer>().bounds.size;

		var tilesX = backgroundSize.x / tileSize.x;
		var tilesY = backgroundSize.y / tileSize.y;

        // start placing tiles from the bottom left
        var bottomLeft = new Vector2(
			background.transform.position.x - (backgroundSize.x / 2),
			background.transform.position.y - (backgroundSize.y / 2)
		);

        for (int i = 0; i <= tilesX; i++)
        {
            for (int j = 0; j <= tilesY; j++)
            {
                if (i == 0 || j == 0 || i == tilesX || j == tilesY) {
					var newTilePos = new Vector2(bottomLeft.x + i * tileSize.x, bottomLeft.y + tileSize.y * j);
                    var outerWallInstance = Instantiate(wall, newTilePos, Quaternion.identity) as GameObject;
                    //outerWallInstance.transform.SetParent(transform);
                    outerWallInstance.layer = LayerMask.NameToLayer("Default");

                    if((i == 0 || i == tilesX) && j % 2 == 0)
                    {
                        outerWallInstance.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if((j == 0 || j == tilesY) && i % 2 == 0)
                    {
                        outerWallInstance.GetComponent<SpriteRenderer>().color = Color.blue;
                    }

                    Debug.Log(outerWallInstance.layer);
                }
            }
        }
    }
}
