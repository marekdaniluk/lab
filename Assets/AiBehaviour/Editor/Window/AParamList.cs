using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AiBehaviour;
using System.Collections;

public abstract class AParamList {
	
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

	protected void InitParamList(AiBlackboard blackboard) {
		_serializedObject = new SerializedObject(blackboard);
		var p = _serializedObject.FindProperty(_mainPropertyName);
		_list = new ReorderableList(_serializedObject, p.FindPropertyRelative(_keysPropertyName), true, true, true, true);
		_list.drawHeaderCallback += DrawHeader;
		_list.onAddCallback += Add;
		_list.onRemoveCallback += Remove;
		_list.drawElementCallback += DrawElement;
	}
	
	public void DrawHeader(Rect rect) {
		var p = _serializedObject.FindProperty(_mainPropertyName);
		if(GUI.Button(rect, p.displayName, "LockedHeaderLabel")) {
			_hidden = !_hidden;
			if(_hidden) {
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
		if(!_hidden) {
			var p = _serializedObject.FindProperty(_mainPropertyName);
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 35, rect.height), p.FindPropertyRelative(_keysPropertyName).GetArrayElementAtIndex(index), GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + rect.width - 35, rect.y, 35, rect.height), p.FindPropertyRelative(_valuesPropertyName).GetArrayElementAtIndex(index), GUIContent.none);
		}
	}

	public abstract void Add(ReorderableList list);

	public abstract void Remove(ReorderableList list);

	public abstract void ApplyModifications();
}
