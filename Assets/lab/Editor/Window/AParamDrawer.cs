using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using lab;

public abstract class AParamDrawer {

    private readonly string _keysPropertyName = "_keys";
    private readonly string _valuesPropertyName = "_values";

    protected SerializedObject _serializedObject;
    protected string _mainPropertyName;
    protected string _keyName;

    private ReorderableList _list;
    private float _elementHeight;
    private bool _hidden = false;

    public void DrawParamList() {
        _serializedObject.Update();
        _list.DoLayoutList();
        ApplyModifications();
    }

    protected void InitParamList(AiBehaviour blackboard) {
        _serializedObject = new SerializedObject(blackboard);
        var p = _serializedObject.FindProperty("_parameters").FindPropertyRelative(_mainPropertyName);
        _list = new ReorderableList(_serializedObject, p.FindPropertyRelative(_keysPropertyName), true, true, true, true);
        _list.drawHeaderCallback += DrawHeader;
        _list.onAddCallback += Add;
        _list.onRemoveCallback += Remove;
        _list.drawElementCallback += DrawElement;
    }

    public void DrawHeader(Rect rect) {
        var p = _serializedObject.FindProperty("_parameters").FindPropertyRelative(_mainPropertyName);
        if (GUI.Button(rect, p.displayName, "LockedHeaderLabel")) {
            _hidden = !_hidden;
            if (_hidden) {
                _elementHeight = _list.elementHeight;
                _list.elementHeight = 0f;
            } else {
                _list.elementHeight = _elementHeight;
            }
            _list.displayAdd = !_hidden;
            _list.displayRemove = !_hidden;
            _list.draggable = !_hidden;
        }
    }

    public void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        if (!_hidden) {
            var p = _serializedObject.FindProperty("_parameters").FindPropertyRelative(_mainPropertyName);
            var l = p.FindPropertyRelative(_keysPropertyName);
            var k = l.GetArrayElementAtIndex(index);
            var c = GetPropertyValue(k);
            //checking key duplicates, thanks to Krzysztof Butkiewicz
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 35, rect.height), k, GUIContent.none);
            if (EditorGUI.EndChangeCheck()) {
                if (HasDuplicateValues(l, index, k)) {
                    SetPropertyValue(k, c);
                }
            }
            EditorGUI.PropertyField(new Rect(rect.x + rect.width - 35, rect.y, 35, rect.height), p.FindPropertyRelative(_valuesPropertyName).GetArrayElementAtIndex(index), GUIContent.none);
        }
    }

    private bool HasDuplicateValues(SerializedProperty so, int indexToOmmit, SerializedProperty val) {
        var v = GetPropertyValue(val);
        for (int i = 0; i < so.arraySize; ++i) {
            if (i != indexToOmmit && v.Equals(GetPropertyValue(so.GetArrayElementAtIndex(i)))) {
                return true;
            }
        }
        return false;
    }

    private static object GetPropertyValue(SerializedProperty sp) {
        switch (sp.propertyType) {
            case SerializedPropertyType.Integer: return sp.longValue;
            case SerializedPropertyType.Boolean: return sp.boolValue;
            case SerializedPropertyType.Float: return sp.doubleValue;
            case SerializedPropertyType.String: return sp.stringValue;
            case SerializedPropertyType.Color: return sp.colorValue;
            case SerializedPropertyType.ObjectReference: return sp.objectReferenceValue;
            case SerializedPropertyType.Enum: return sp.enumValueIndex;
            case SerializedPropertyType.Vector2: return sp.vector2Value;
            case SerializedPropertyType.Vector3: return sp.vector3Value;
            case SerializedPropertyType.Rect: return sp.rectValue;
            case SerializedPropertyType.ArraySize: return sp.arraySize;
            case SerializedPropertyType.Character: return sp.intValue;
            case SerializedPropertyType.LayerMask: return sp.intValue;
            case SerializedPropertyType.AnimationCurve: return sp.animationCurveValue;
            case SerializedPropertyType.Bounds: return sp.boundsValue;
            case SerializedPropertyType.Quaternion: return sp.quaternionValue;
            default: return null;
        }
    }

    private static void SetPropertyValue(SerializedProperty sp, object value) {
        switch (sp.propertyType) {
            case SerializedPropertyType.Integer: sp.longValue = (long)value; break;
            case SerializedPropertyType.Boolean: sp.boolValue = (bool)value; break;
            case SerializedPropertyType.Float: sp.doubleValue = (double)value; break;
            case SerializedPropertyType.String: sp.stringValue = (string)value; break;
            case SerializedPropertyType.Color: sp.colorValue = (Color)value; break;
            case SerializedPropertyType.ObjectReference: sp.objectReferenceValue = (Object)value; break;
            case SerializedPropertyType.Enum: sp.enumValueIndex = (int)value; break;
            case SerializedPropertyType.Vector2: sp.vector2Value = (Vector2)value; break;
            case SerializedPropertyType.Vector3: sp.vector3Value = (Vector3)value; break;
            case SerializedPropertyType.Rect: sp.rectValue = (Rect)value; break;
            case SerializedPropertyType.ArraySize: sp.arraySize = (int)value; break;
            case SerializedPropertyType.Character: sp.intValue = (int)value; break;
            case SerializedPropertyType.LayerMask: sp.intValue = (int)value; break;
            case SerializedPropertyType.AnimationCurve: sp.animationCurveValue = (AnimationCurve)value; break;
            case SerializedPropertyType.Bounds: sp.boundsValue = (Bounds)value; break;
            case SerializedPropertyType.Quaternion: sp.quaternionValue = (Quaternion)value; break;
        }
    }

    public abstract void Add(ReorderableList list);

    public abstract void Remove(ReorderableList list);

    public abstract void ApplyModifications();
}
