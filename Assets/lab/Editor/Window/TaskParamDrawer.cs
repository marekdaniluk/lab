using UnityEditorInternal;
using lab;
using UnityEditor;

public class TaskParamDrawer : AParamDrawer {

	public TaskParamDrawer(AiBehaviour blackboard) {
		_mainPropertyName = "_taskParameters";
		_keyName = "Task Parameter";
		InitParamList(blackboard);
	}

	#region implemented abstract members of AParamList
	public override void Add(ReorderableList list) {
		string k = _keyName;
		int i = 0;
		while (((AiBehaviour)_serializedObject.targetObject).Blackboard.TaskParameters.ContainsKey(k)) {
			k = _keyName + " " + (i++).ToString();
		}
		((AiBehaviour)_serializedObject.targetObject).Blackboard.TaskParameters[k] = "";
		Selection.activeObject = _serializedObject.targetObject;
	}

	public override void Remove(ReorderableList list) {
		((AiBehaviour)_serializedObject.targetObject).Blackboard.TaskParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
		Selection.activeObject = _serializedObject.targetObject;
	}

	public override void ApplyModifications() {
		_serializedObject.ApplyModifiedProperties();
		((AiBehaviour)_serializedObject.targetObject).Blackboard.TaskParameters.OnBeforeSerialize();
	}
	#endregion
}
