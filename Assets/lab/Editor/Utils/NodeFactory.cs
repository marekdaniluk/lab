using UnityEngine;
using UnityEditor;
using System;

namespace lab {
    public class NodeFactory {

        public class NodeCallbackData {
            public Vector2 position;
            public Type nodeType;

            public NodeCallbackData(Vector2 p, Type n) {
                position = p;
                nodeType = n;
            }
        };

        public static ANode CreateNode(Type nodeType) {
            ANode n = ScriptableObject.CreateInstance(nodeType) as ANode;
            n.name = n.GetType().Name;
            n.hideFlags = HideFlags.HideInHierarchy;
            return n;
        }

        public static void AddNodeToTarget(ANode n, AiBehaviour blackboard) {
            AssetDatabase.AddObjectToAsset(n, blackboard);
            EditorUtility.SetDirty(n);
            AssetDatabase.SaveAssets();
        }

        public static void CreateNodeMenu(Vector2 position, GenericMenu.MenuFunction2 MenuCallback) {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Flow Nodes/Sequencer"), false, MenuCallback, new NodeCallbackData(position, typeof(SequenceNode)));
            menu.AddItem(new GUIContent("Flow Nodes/Selector"), false, MenuCallback, new NodeCallbackData(position, typeof(SelectorNode)));
            menu.AddItem(new GUIContent("Flow Nodes/Inventer"), false, MenuCallback, new NodeCallbackData(position, typeof(InverterNode)));
            menu.AddItem(new GUIContent("Flow Nodes/Repeater"), false, MenuCallback, new NodeCallbackData(position, typeof(RepeaterNode)));
            menu.AddItem(new GUIContent("Flow Nodes/Succeeder"), false, MenuCallback, new NodeCallbackData(position, typeof(SucceederNode)));
            menu.AddItem(new GUIContent("Parameter Nodes/Bool Parameter"), false, MenuCallback, new NodeCallbackData(position, typeof(BoolParameterNode)));
            menu.AddItem(new GUIContent("Parameter Nodes/Float Parameter"), false, MenuCallback, new NodeCallbackData(position, typeof(FloatParameterNode)));
            menu.AddItem(new GUIContent("Parameter Nodes/Int Parameter"), false, MenuCallback, new NodeCallbackData(position, typeof(IntParameterNode)));
            menu.AddItem(new GUIContent("Parameter Nodes/String Parameter"), false, MenuCallback, new NodeCallbackData(position, typeof(StringParameterNode)));
            menu.AddItem(new GUIContent("TaskNode"), false, MenuCallback, new NodeCallbackData(position, typeof(TaskNode)));
            menu.AddItem(new GUIContent("TreeNode"), false, MenuCallback, new NodeCallbackData(position, typeof(TreeNode)));
            menu.ShowAsContext();
        }
    }
}
