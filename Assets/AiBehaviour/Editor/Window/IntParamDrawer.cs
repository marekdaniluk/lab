using UnityEditorInternal;
using AiBehaviour;
using UnityEditor;

public class IntParamDrawer : AParamDrawer {

    public IntParamDrawer(AiBlackboard blackboard) {
        _mainPropertyName = "_intParameters";
        _keyName = "Int Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBlackboard)_serializedObject.targetObject).IntParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBlackboard)_serializedObject.targetObject).IntParameters[k] = 0;
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBlackboard)_serializedObject.targetObject).IntParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBlackboard)_serializedObject.targetObject).IntParameters.OnBeforeSerialize();
    }
    #endregion
}
