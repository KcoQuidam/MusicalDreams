using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ActionRegister))]
public class ActionRegisterInspector : Editor 
{
	public override void OnInspectorGUI()
	{
		ActionRegister ar = (ActionRegister) target;

		ar.actionName = EditorGUILayout.TextField ("Action Name", ar.actionName);
		ar.description = EditorGUILayout.TextArea (ar.description);
	}
}
