using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Adapted from http://unity.grogansoft.com/tiled-background/
 */
[RequireComponent(typeof(BoxCollider2D))]
public class TiledBackground : MonoBehaviour 
{
	public GameObject tile;

	private readonly List<float> rotations = new List<float>{0, 90, 180, 270};

    void Start()
    {
        TileBackground(GetComponent<BoxCollider2D>());
    }

    void TileBackground(BoxCollider2D background)
    {
		var random = new System.Random();

        var backgroundSize = background.bounds.size;
        var tileSize = tile.GetComponent<Renderer>().bounds.size;

		var tilesX = backgroundSize.x / tileSize.x;
		var  tilesY = backgroundSize.y / tileSize.y;

        // start placing tiles from the bottom left
        var bottomLeft = new Vector2(
			background.transform.position.x - (backgroundSize.x / 2),
			background.transform.position.y - (backgroundSize.y / 2)
		);

        for (int i = 0; i <= tilesX; i++)
        {
            for (int j = 0; j <= tilesY; j++)
            {
                var newTilePos = new Vector2(bottomLeft.x + i * tileSize.x, bottomLeft.y + tileSize.y * j);
                var newTile = Instantiate(tile, newTilePos, Quaternion.identity) as GameObject;

				newTile.GetComponent<SpriteRenderer>().enabled = true;
                newTile.transform.parent = transform;
				newTile.transform.Rotate(Vector3.forward * rotations[random.Next(rotations.Count)]);
            }
        }
    }
}
