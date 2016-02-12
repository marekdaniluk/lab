using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AiBehaviour;
using System.Collections;

public class BoolParamList : AParamList {
	
	public BoolParamList(AiBlackboard blackboard) {
		_mainPropertyName = "_boolParameters";
		_keyName = "Bool Parameter";
		InitParamList(blackboard);
	}
	
	#region implemented abstract members of AParamList
	public override void Add(ReorderableList list) {
		string k = _keyName;
		int i = 0;
		while (((AiBlackboard)_serializedObject.targetObject).BoolParameters.ContainsKey(k)) {
			k = _keyName + " " + (i++).ToString();
		}
		((AiBlackboard)_serializedObject.targetObject).BoolParameters[k] = false;
	}
	
	public override void Remove(ReorderableList list) {
		((AiBlackboard)_serializedObject.targetObject).BoolParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
	}
	
	public override void Serialize() {
		((AiBlackboard)_serializedObject.targetObject).BoolParameters.OnBeforeSerialize();
	}
	#endregion
}
