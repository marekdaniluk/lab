using UnityEditor;
using lab;

public class ParamPanelDrawer {

    private AiBehaviour _blackboard;
    private IntParamDrawer _intParamList;
    private FloatParamDrawer _floatParamList;
    private BoolParamDrawer _boolParamList;
    private StringParamDrawer _stringParamList;
	private TaskParamDrawer _taskParamList;

    public AiBehaviour Blackboard {
        set {
            _blackboard = value;
            if (_blackboard != null) {
                _intParamList = new IntParamDrawer(_blackboard);
                _floatParamList = new FloatParamDrawer(_blackboard);
                _boolParamList = new BoolParamDrawer(_blackboard);
                _stringParamList = new StringParamDrawer(_blackboard);
				_taskParamList = new TaskParamDrawer(_blackboard);
            }
        }
    }

    public void DrawPanel() {
        if (_blackboard != null) {
            _intParamList.DrawParamList();
            _floatParamList.DrawParamList();
            _boolParamList.DrawParamList();
            _stringParamList.DrawParamList();
			_taskParamList.DrawParamList();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
    }
}
