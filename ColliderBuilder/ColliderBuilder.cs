using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderBuilder : MonoBehaviour {

	public tk2dTileMap tileMap;

	private tk2dTileMapData mapData;
	private Vector2 tileSize;
	private GameObject colliders;
	private int colliderLayer;

	public void SetLayerForColliders( int layer ){
		colliderLayer = layer;
	}

	public void Remove(){
		DestroyImmediate( colliders.gameObject );
	}
	
	public void Create () {

		Remove ();

		gameObject.layer = colliderLayer;

		// Create a child object for the colliders (makes it easier to delete all the created colliders).
		GameObject o = new GameObject();
		o.name = "Colliders";
		o.layer = colliderLayer;
		o.transform.parent = gameObject.transform;
		colliders = o;

		// Get our tile map data
		mapData = tileMap.data;

		// Get map Layers
		List<tk2dRuntime.TileMap.LayerInfo> tileMapLayers = mapData.tileMapLayers;

		// Get tilesize, this will be the box collider size
		tileSize = mapData.tileSize;

		// Get map size
		Vector2 mapSize = new Vector2 (tileMap.width, tileMap.height);

		// Loop tiles
		for(int tileMapLayer = 0; tileMapLayer < tileMapLayers.Count; tileMapLayer++){
			for (int tileMapColumn = 0; tileMapColumn < mapSize.y; tileMapColumn++){
				for (int tileMapRow = 0; tileMapRow < mapSize.x; tileMapRow++){

					// Find the current tile
					Vector2 currentTilePosition = new Vector2(tileMapColumn * tileSize.x, tileMapRow * tileSize.y);
					int currentTileID = tileMap.GetTileIdAtPosition(currentTilePosition, tileMapLayer);

					if( TileHasCollider(currentTileID) ){
						BuildColliderAtPosition( tileMapColumn * tileSize.x, tileMapRow * tileSize.y);
					}

				}
			}
		}


	
	}

	bool TileHasCollider( int tileID ){

		if( tileID >= 0 ){

			tk2dSpriteDefinition def = tileMap.SpriteCollectionInst.spriteDefinitions[tileID];
			
			if( def.colliderType != tk2dSpriteDefinition.ColliderType.Unset ){
				return true;
			}else{
				return false;
			}

		}else{
			return false;
		}

	}

	void BuildColliderAtPosition(float x, float y){

		GameObject o = new GameObject();
		o.name = "Collider @ " + x + ", " + y;
		o.layer = colliderLayer;
		o.transform.position = new Vector2(x, y);
		o.transform.localScale = tileSize;
		o.AddComponent<BoxCollider2D>();
		
		BoxCollider2D b = o.GetComponent<BoxCollider2D>();
		b.size = new Vector2(1,1);

		o.transform.parent = colliders.transform;

	}
}
