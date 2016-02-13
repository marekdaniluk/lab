using UnityEditor;
using AiBehaviour;

public class ParamPanelDrawer {

    private AiBlackboard _blackboard;
    private IntParamDrawer _intParamList;
    private FloatParamDrawer _floatParamList;
    private BoolParamDrawer _boolParamList;
    private StringParamDrawer _stringParamList;

    public AiBlackboard Blackboard {
        set {
            _blackboard = value;
            if (_blackboard != null) {
                _intParamList = new IntParamDrawer(_blackboard);
                _floatParamList = new FloatParamDrawer(_blackboard);
                _boolParamList = new BoolParamDrawer(_blackboard);
                _stringParamList = new StringParamDrawer(_blackboard);
            }
        }
    }

    public void DrawPanel() {
        if (_blackboard != null) {
            _intParamList.DrawParamList();
            _floatParamList.DrawParamList();
            _boolParamList.DrawParamList();
            _stringParamList.DrawParamList();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
    }
}
