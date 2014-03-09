using UnityEngine;
using System.Collections;

public class CreateBoxColliders : MonoBehaviour {

	public tk2dTileMap tileMap;
	private tk2dTileMapData mapData;
	private Vector2 tileSize;
	private GameObject colliders;
	private LayerMask layer;

	public void Remove(){

		// clear map first
		DestroyImmediate( colliders.gameObject );

	}
	
	// Use this for initialization
	public void Create () {

		Remove ();

		layer = LayerMask.NameToLayer("Pathfinding Grid");
		gameObject.layer = layer;

		// Create a child object for the colliders
		GameObject o = new GameObject();
		o.name = "Colliders";
		o.layer = layer;
		o.transform.parent = gameObject.transform;
		colliders = o;

		// Get our tile map data
		mapData = tileMap.data;

		// Get tilesize, this will be the box collider size
		tileSize = mapData.tileSize;

		// Get map size
		Vector2 mapSize = new Vector2 (tileMap.width, tileMap.height);

		// Loop tiles
		for (int i = 0; i < mapSize.y; i++)
		{
			for (int j = 0; j < mapSize.x; j++)
			{

				// Find the current tile
				Vector3 currentTilePosition = new Vector3(i * tileSize.x, j * tileSize.y, 0);
				int currentTileID = tileMap.GetTileIdAtPosition(currentTilePosition, 0);

				if( currentTileID >= 0 )
				{
					tk2dSpriteDefinition def = tileMap.SpriteCollectionInst.spriteDefinitions[currentTileID];

					if( def.colliderType != tk2dSpriteDefinition.ColliderType.Unset )
					{
						BuildColliderAtPosition( i * tileSize.x, j * tileSize.y);
					}
				}

			}

		}


	
	}

	void BuildColliderAtPosition(float x, float y){

		GameObject o = new GameObject();
		o.name = "Collider @ " + x + ", " + y;
		o.layer = layer;
		o.transform.position = new Vector2(x, y);
		o.transform.localScale = tileSize;
		o.AddComponent<BoxCollider2D>();
		
		BoxCollider2D b = o.GetComponent<BoxCollider2D>();
		b.size = new Vector2(1,1);

		o.transform.parent = colliders.transform;

	}
}
