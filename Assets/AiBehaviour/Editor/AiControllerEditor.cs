using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(AiController))]
public class AiControllerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var aiController = (AiController)target;
        if (aiController.Blackboard == null) {
            EditorGUILayout.HelpBox("Not initialized", MessageType.Info);
        } else {
            if (aiController.Blackboard.Trees == null || aiController.Blackboard.Trees.Count == 0) {
                EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no Ai Trees. Add at least one Ai Tree.", aiController.Blackboard.name), MessageType.Info);
            }
        }
    }
}
