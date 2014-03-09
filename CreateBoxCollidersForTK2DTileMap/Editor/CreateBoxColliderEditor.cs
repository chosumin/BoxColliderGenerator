using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateBoxColliders))]
public class CreateBoxColliderEditor : Editor {

	public override void OnInspectorGUI(){

		DrawDefaultInspector();

		CreateBoxColliders createCollidersScript = (CreateBoxColliders)target;

		if(GUILayout.Button("Create Colliders")){
			createCollidersScript.Create();
		}

		if(GUILayout.Button("Remove Colliders")){
			createCollidersScript.Remove();
		}

	}

}
