using UnityEditorInternal;
using lab;
using UnityEditor;

public class StringParamDrawer : AParamDrawer {

    public StringParamDrawer(AiBehaviour blackboard) {
        _mainPropertyName = "_stringParameters";
        _keyName = "String Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBehaviour)_serializedObject.targetObject).Blackboard.StringParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.StringParameters[k] = "";
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.StringParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.StringParameters.OnBeforeSerialize();
    }
    #endregion
}
