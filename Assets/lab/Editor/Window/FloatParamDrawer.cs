using UnityEditorInternal;
using lab;
using lab.EditorView;
using UnityEditor;

public class FloatParamDrawer : AParamDrawer {

    public FloatParamDrawer(AiBehaviour blackboard) {
        _mainPropertyName = "_floatParameters";
        _keyName = "Float Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBehaviour)_serializedObject.targetObject).Blackboard.FloatParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.FloatParameters[k] = 0f;
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBehaviour)_serializedObject.targetObject).Blackboard.FloatParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        if(_serializedObject.ApplyModifiedProperties()) {
            ((AiBehaviour)_serializedObject.targetObject).Blackboard.FloatParameters.OnBeforeSerialize();   
        }
    }
    #endregion
}
