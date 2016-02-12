using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using AiBehaviour;

internal class EndNameEdit : EndNameEditAction {

    #region implemented abstract members of EndNameEditAction
    public override void Action(int instanceId, string pathName, string resourceFile) {
        AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId), AssetDatabase.GenerateUniqueAssetPath(pathName));
        AssetDatabase.SaveAssets();
    }
    #endregion
}

[CustomEditor(typeof(AiBlackboard))]
public class AiBlackboardEditor : Editor {

    [MenuItem("Assets/Create/AiBlackboard")]
    private static void CreateAiBlackboardMenu() {
        var asset = ScriptableObject.CreateInstance<AiBlackboard>();
        asset.AddTree(new AiTree());
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            asset.GetInstanceID(),
            ScriptableObject.CreateInstance<EndNameEdit>(),
            string.Format("{0}.asset", asset.GetType().Name),
            AssetPreview.GetMiniThumbnail(asset),
            null);
    }

    private static void CreateAiBlackboard() {
        string path = EditorUtility.SaveFilePanel("Create New AiBlackboard", "Assets/", "AiBlackboard.asset", "asset");
        if(string.IsNullOrEmpty(path)) {
            return;
        }
        path = FileUtil.GetProjectRelativePath(path);
        var asset = ScriptableObject.CreateInstance<AiBlackboard>();
        asset.AddTree(new AiTree());
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
    }

    public override void OnInspectorGUI() {
        var blackboard = (AiBlackboard)target;
        if (blackboard.Trees == null || blackboard.Trees.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no ai trees. Add at least one ai tree.", blackboard.name), MessageType.Info);
        }
    }
}
