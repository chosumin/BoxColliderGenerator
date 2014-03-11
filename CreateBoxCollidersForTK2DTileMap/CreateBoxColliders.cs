using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBoxColliders : MonoBehaviour {

	public tk2dTileMap tileMap;
	private tk2dTileMapData mapData;
	private Vector2 tileSize;
	private GameObject colliders;
	private LayerMask layer;
	private List<int> collidableTiles;

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

		// Map Layers
		List<tk2dRuntime.TileMap.LayerInfo> tileMapLayers = mapData.tileMapLayers;

		// Get tilesize, this will be the box collider size
		tileSize = mapData.tileSize;

		// Get map size
		Vector2 mapSize = new Vector2 (tileMap.width, tileMap.height);

		// Loop tiles
		for(int tileMapLayer = 0; tileMapLayer < tileMapLayers.Count; tileMapLayer++){
			for (int tileMapLayerColumn = 0; tileMapLayerColumn < mapSize.y; tileMapLayerColumn++){
				for (int tileMapLayerRow = 0; tileMapLayerRow < mapSize.x; tileMapLayerRow++){

					// Find the current tile
					Vector3 currentTilePosition = new Vector3(tileMapLayerColumn * tileSize.x, tileMapLayerRow * tileSize.y, tileMapLayer);
					int currentTileID = tileMap.GetTileIdAtPosition(currentTilePosition, 0);

					if( TileHasCollider(currentTileID) ){
						BuildColliderAtPosition( tileMapLayerColumn * tileSize.x, tileMapLayerRow * tileSize.y);
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
		o.layer = layer;
		o.transform.position = new Vector2(x, y);
		o.transform.localScale = tileSize;
		o.AddComponent<BoxCollider2D>();
		
		BoxCollider2D b = o.GetComponent<BoxCollider2D>();
		b.size = new Vector2(1,1);

		o.transform.parent = colliders.transform;

	}
}
