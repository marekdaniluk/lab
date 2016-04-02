using UnityEditorInternal;
using lab;
using UnityEditor;

public class IntParamDrawer : AParamDrawer {

    public IntParamDrawer(AiBehaviour blackboard) {
        _mainPropertyName = "_intParameters";
        _keyName = "Int Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBehaviour)_serializedObject.targetObject).IntParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBehaviour)_serializedObject.targetObject).IntParameters[k] = 0;
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBehaviour)_serializedObject.targetObject).IntParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBehaviour)_serializedObject.targetObject).IntParameters.OnBeforeSerialize();
    }
    #endregion
}
