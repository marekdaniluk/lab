using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using lab;
using lab.EditorView;

internal class LabEndNameEdit : EndNameEditAction {

    #region implemented abstract members of EndNameEditAction
    public override void Action(int instanceId, string pathName, string resourceFile) {
        AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId), AssetDatabase.GenerateUniqueAssetPath(pathName));
        AssetDatabase.SaveAssets();
    }
    #endregion
}

[CustomEditor(typeof(AiBehaviour))]
public class AiBehaviourEditor : Editor {
    
    private IntParamDrawer _intParamList;
    private FloatParamDrawer _floatParamList;
    private BoolParamDrawer _boolParamList;
    private StringParamDrawer _stringParamList;
    private TaskParamDrawer _taskParamList;

    private void OnEnable() {
        var behaviour = target as AiBehaviour;
        _intParamList = new IntParamDrawer(behaviour);
        _floatParamList = new FloatParamDrawer(behaviour);
        _boolParamList = new BoolParamDrawer(behaviour);
        _stringParamList = new StringParamDrawer(behaviour);
        _taskParamList = new TaskParamDrawer(behaviour);
    }

    [MenuItem("Assets/Create/AiBehaviour")]
    private static void CreateAiBehaviourMenu() {
        var asset = ScriptableObject.CreateInstance<AiBehaviour>();
        asset.AddTree(new AiTree());
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            asset.GetInstanceID(),
            ScriptableObject.CreateInstance<LabEndNameEdit>(),
            string.Format("{0}.asset", asset.GetType().Name),
            AssetPreview.GetMiniThumbnail(asset),
            null);
    }

    private static void CreateAiBehaviour() {
        string path = EditorUtility.SaveFilePanel("Create New AiBehaviour", "Assets/", "AiBehaviour.asset", "asset");
        if (string.IsNullOrEmpty(path)) {
            return;
        }
        path = FileUtil.GetProjectRelativePath(path);
        var asset = ScriptableObject.CreateInstance<AiBehaviour>();
        asset.AddTree(new AiTree());
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
    }

    public override void OnInspectorGUI() {
        var behaviour = target as AiBehaviour;
        if (behaviour.Trees == null || behaviour.Trees.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Behaviour \"{0}\" has no ai trees. Add at least one ai tree.", behaviour.name), MessageType.Info);
        }
        _intParamList.DrawParamList();
        _floatParamList.DrawParamList();
        _boolParamList.DrawParamList();
        _stringParamList.DrawParamList();
        _taskParamList.DrawParamList();
    }
}
