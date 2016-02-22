using UnityEngine;
using UnityEditor;
using AiBehaviour;

public class NodeFactory {

    public class NodeCallbackData {
        public Vector2 position;
        public Nodes nodeType;

        public NodeCallbackData(Vector2 p, Nodes n) {
            position = p;
            nodeType = n;
        }
    };

    public enum Nodes {
        IntNode,
        FloatNode,
        BoolNode,
        StringNode,
        InverterNode,
        RepeaterNode,
        SelectorNode,
        SequenceNode,
        TaskNode
    };

    public static ANode CreateNode(Nodes node, AiBlackboard blackboard) {
        ANode n = null;
        switch (node) {
            case Nodes.IntNode:
                n = ScriptableObject.CreateInstance<IntParameterNode>();
                ((IntParameterNode)n).Blackboard = blackboard;
                break;
            case Nodes.FloatNode:
                n = ScriptableObject.CreateInstance<FloatParameterNode>();
                ((FloatParameterNode)n).Blackboard = blackboard;
                break;
            case Nodes.BoolNode:
                n = ScriptableObject.CreateInstance<BoolParameterNode>();
                ((BoolParameterNode)n).Blackboard = blackboard;
                break;
            case Nodes.StringNode:
                n = ScriptableObject.CreateInstance<StringParameterNode>();
                ((StringParameterNode)n).Blackboard = blackboard;
                break;
            case Nodes.InverterNode:
                n = ScriptableObject.CreateInstance<InverterNode>();
                break;
            case Nodes.RepeaterNode:
                n = ScriptableObject.CreateInstance<RepeaterNode>();
                break;
            case Nodes.SelectorNode:
                n = ScriptableObject.CreateInstance<SelectorNode>();
                break;
            case Nodes.SequenceNode:
                n = ScriptableObject.CreateInstance<SequenceNode>();
                break;
            case Nodes.TaskNode:
                n = ScriptableObject.CreateInstance<TaskNode>();
                break;
            default:
                return null;
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
        foreach (Nodes node in System.Enum.GetValues(typeof(Nodes))) {
            menu.AddItem(new GUIContent(node.ToString()), false, MenuCallback, new NodeCallbackData(position, node));
        }
        menu.ShowAsContext();
    }
}
