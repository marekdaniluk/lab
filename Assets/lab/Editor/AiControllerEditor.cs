using UnityEditor;
using lab;

[CustomEditor(typeof(AiController))]
public class AiControllerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var aiController = (AiController)target;
        if (aiController.Behaviour == null) {
            EditorGUILayout.HelpBox("Not initialized", MessageType.Info);
        }
    }
}
