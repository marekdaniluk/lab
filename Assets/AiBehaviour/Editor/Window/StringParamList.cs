using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AiBehaviour;
using System.Collections;

public class StringParamList : AParamList {

	public StringParamList(AiBlackboard blackboard) {
		_mainPropertyName = "_stringParameters";
		_keyName = "String Parameter";
		InitParamList(blackboard);
	}
	
	#region implemented abstract members of AParamList
	public override void Add(ReorderableList list) {
		string k = _keyName;
		int i = 0;
		while (((AiBlackboard)_serializedObject.targetObject).StringParameters.ContainsKey(k)) {
			k = _keyName + " " + (i++).ToString();
		}
		((AiBlackboard)_serializedObject.targetObject).StringParameters[k] = "";
	}
	
	public override void Remove(ReorderableList list) {
		((AiBlackboard)_serializedObject.targetObject).StringParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
	}
	
	public override void Serialize() {
		((AiBlackboard)_serializedObject.targetObject).StringParameters.OnBeforeSerialize();
	}
	#endregion
}
