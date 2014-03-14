using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ColliderBuilder))]
public class ColliderBuilderEditor : Editor {

	int selectedLayer;

	public override void OnInspectorGUI(){

		DrawDefaultInspector();

		selectedLayer = EditorGUILayout.LayerField("Layer for Colliders", selectedLayer);

		ColliderBuilder colliderBuilder = (ColliderBuilder)target;

		if(GUILayout.Button("Create Colliders")){
			colliderBuilder.SetLayerForColliders( selectedLayer );
			colliderBuilder.CreateColliders();
		}

		if(GUILayout.Button("Remove Colliders")){
			colliderBuilder.RemoveColliders();
		}

	}

}
