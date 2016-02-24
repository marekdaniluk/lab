using UnityEngine;
using UnityEditor;
using AiBehaviour;
using System;
using System.Linq;
using System.Reflection;

public class NodeFactory {

    public class NodeCallbackData {
        public Vector2 position;
        public Type nodeType;

		public NodeCallbackData(Vector2 p, Type n) {
            position = p;
            nodeType = n;
        }
    };

	public static ANode CreateNode(Type nodeType, AiBlackboard blackboard) {
		ANode n = ScriptableObject.CreateInstance(nodeType) as ANode;
		if(n.GetType().IsSubclassOfRawGeneric(typeof(AParameterNode<>))) {
			n.GetType().GetProperty("Blackboard").SetValue(n, blackboard, null);
		}
        n.name = n.GetType().Name;
        n.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(n, blackboard);
        EditorUtility.SetDirty(n);
        AssetDatabase.SaveAssets();
        return n;
    }

    public static void CreateNodeMenu(Vector2 position, GenericMenu.MenuFunction2 MenuCallback) {
        GenericMenu menu = new GenericMenu();
		
		var assembly = Assembly.Load(new AssemblyName("Assembly-CSharp"));
		var paramTypes = (from t in assembly.GetTypes() where t.IsSubclassOfRawGeneric(typeof(AParameterNode<>)) && !t.IsAbstract select t).ToArray();
		var flowTypes = (from t in assembly.GetTypes() where t.IsSubclassOfRawGeneric(typeof(AFlowNode)) && !t.IsAbstract select t).ToArray();
		foreach(System.Type t in paramTypes) {
			menu.AddItem(new GUIContent(string.Format("Parameter Nodes/{0}", t.Name)), false, MenuCallback, new NodeCallbackData(position, t));
		}
		foreach(System.Type t in flowTypes) {
			menu.AddItem(new GUIContent(string.Format("Flow Nodes/{0}", t.Name)), false, MenuCallback, new NodeCallbackData(position, t));
		}
		menu.AddItem(new GUIContent("TaskNode"), false, MenuCallback, new NodeCallbackData(position, typeof(TaskNode)));
        menu.ShowAsContext();
    }
}
