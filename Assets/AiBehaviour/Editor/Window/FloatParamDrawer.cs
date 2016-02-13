using UnityEditorInternal;
using AiBehaviour;

public class FloatParamDrawer : AParamDrawer {

    public FloatParamDrawer(AiBlackboard blackboard) {
        _mainPropertyName = "_floatParameters";
        _keyName = "Float Parameter";
        InitParamList(blackboard);
    }

    #region implemented abstract members of AParamList
    public override void Add(ReorderableList list) {
        string k = _keyName;
        int i = 0;
        while (((AiBlackboard)_serializedObject.targetObject).FloatParameters.ContainsKey(k)) {
            k = _keyName + " " + (i++).ToString();
        }
        ((AiBlackboard)_serializedObject.targetObject).FloatParameters[k] = 0f;
    }

    public override void Remove(ReorderableList list) {
        ((AiBlackboard)_serializedObject.targetObject).FloatParameters.Remove(list.serializedProperty.GetArrayElementAtIndex(list.index).stringValue);
    }

    public override void ApplyModifications() {
        _serializedObject.ApplyModifiedProperties();
        ((AiBlackboard)_serializedObject.targetObject).FloatParameters.OnBeforeSerialize();
    }
    #endregion
}
