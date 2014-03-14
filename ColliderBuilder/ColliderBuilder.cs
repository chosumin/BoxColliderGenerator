using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderBuilder : MonoBehaviour {

	public tk2dTileMap tileMap;

	private tk2dTileMapData mapData;
	private List<tk2dRuntime.TileMap.LayerInfo> tileMapLayers;

	private Vector2 tileSize, mapSize, currentTilePosition;
	private GameObject colliders;

	private int colliderLayer;

	public void SetLayerForColliders( int layer ){
		colliderLayer = layer;
		gameObject.layer = colliderLayer;
	}

	public void RemoveColliders(){
		DestroyImmediate( colliders.gameObject );
	}

	private void CreateColliderParentGameObject(){
		GameObject o = new GameObject();
		o.name = "Colliders";
		o.layer = colliderLayer;
		o.transform.parent = gameObject.transform;
		colliders = o;
	}
	
	public void CreateColliders() {

		// Clean up old colliders
		RemoveColliders();

		// Create a child object for the colliders (makes it easier to delete all the created colliders).
		CreateColliderParentGameObject();

		// Get our tile map data
		mapData = tileMap.data;

		// Get map Layers
		tileMapLayers = mapData.tileMapLayers;

		// Get tilesize, this will be the box collider size
		tileSize = mapData.tileSize;

		// Get map size
		mapSize = new Vector2 (tileMap.width, tileMap.height);

		// Loop tiles
		for(int tileMapLayer = 0; tileMapLayer < tileMapLayers.Count; tileMapLayer++){
			for (int tileMapColumn = 0; tileMapColumn < mapSize.y; tileMapColumn++){
				for (int tileMapRow = 0; tileMapRow < mapSize.x; tileMapRow++){

					// Find the current tile
					currentTilePosition = new Vector2(tileMapColumn * tileSize.x, tileMapRow * tileSize.y);
					int currentTileID = tileMap.GetTileIdAtPosition(currentTilePosition, tileMapLayer);

					if( TileHasCollider(currentTileID) ){
						BuildColliderAtPosition( currentTilePosition );
					}

				}
			}
		}
	
	}

	private bool TileHasCollider( int tileID ){

		if( tileID >= 0 ){
			tk2dSpriteDefinition def = tileMap.SpriteCollectionInst.spriteDefinitions[tileID];
			return def.colliderType != tk2dSpriteDefinition.ColliderType.Unset;
		}else{
			return false;
		}

	}

	private void BuildColliderAtPosition( Vector2 position ){

		GameObject boxColliderGameObject = new GameObject();
		boxColliderGameObject.name = "Collider @ " + position.x + ", " + position.y;
		boxColliderGameObject.layer = colliderLayer;
		boxColliderGameObject.transform.position = position;
		boxColliderGameObject.transform.localScale = tileSize;
		boxColliderGameObject.AddComponent<BoxCollider2D>();
		boxColliderGameObject.transform.parent = colliders.transform;

	}
}
