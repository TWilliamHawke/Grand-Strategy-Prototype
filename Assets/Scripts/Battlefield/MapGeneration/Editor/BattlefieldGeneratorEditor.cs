using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Battlefield.Generator
{
	[CustomEditor(typeof(BattlefieldGenerator))]
	public class BattlefieldGeneratorEditor : Editor
	{
	    public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if(GUILayout.Button("ChangeSeed"))
			{
				var mapConfigProp = serializedObject.FindProperty("_mapConfig");
				(mapConfigProp.objectReferenceValue as MapConfig)?.GenerateNewSeed();
			}
		}
	}
}