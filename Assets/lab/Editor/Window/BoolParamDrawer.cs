using UnityEditorInternal;
using lab;
using UnityEditor;

public class BoolParamDrawer : AParamDrawer {

    public BoolParamDrawer(AiBehaviour blackboard) {
        _mainPropertyName = "_boolParameters";
        _keyName = "Bool Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBehaviour)_serializedObject.targetObject).Blackboard.BoolParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.BoolParameters[k] = false;
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.BoolParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.BoolParameters.OnBeforeSerialize();
    }
    #endregion
}
