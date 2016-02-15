using UnityEditorInternal;
using AiBehaviour;
using UnityEditor;

public class BoolParamDrawer : AParamDrawer {

    public BoolParamDrawer(AiBlackboard blackboard) {
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
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void Remove(ReorderableList list) {
        ((AiBlackboard)_serializedObject.targetObject).BoolParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
        Selection.activeObject = _serializedObject.targetObject;
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBlackboard)_serializedObject.targetObject).BoolParameters.OnBeforeSerialize();
    }
    #endregion
}
