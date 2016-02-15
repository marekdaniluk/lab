using UnityEngine;
using UnityEditor;
using AiBehaviour;

public class NodeFactory {

    public enum Nodes {
        IntNode,
        FloatNode,
        BoolNode,
        StringNode,
        InverterNode,
        RepeaterNode,
        SelectorNode,
        SequenceNode,
    };

    public static ANode CreateNode(Nodes node, AiBlackboard blackboard) {
        ANode n = null;
        switch(node) {
            case Nodes.IntNode:
                {
                    n = ScriptableObject.CreateInstance<IntParameterNode>();
                    ((IntParameterNode)n).Blackboard = blackboard;
                    break;
                }
            case Nodes.FloatNode:
                {
                    n = ScriptableObject.CreateInstance<FloatParameterNode>();
                    ((FloatParameterNode)n).Blackboard = blackboard;
                    break;
                }
            case Nodes.BoolNode:
                {
                    n = ScriptableObject.CreateInstance<BoolParameterNode>();
                    ((BoolParameterNode)n).Blackboard = blackboard;
                    break;
                }
            case Nodes.StringNode:
                {
                    n = ScriptableObject.CreateInstance<StringParameterNode>();
                    ((StringParameterNode)n).Blackboard = blackboard;
                    break;
                }
            case Nodes.InverterNode:
                {
                    n = ScriptableObject.CreateInstance<InverterNode>();
                    break;
                }
            case Nodes.RepeaterNode:
                {
                    n = ScriptableObject.CreateInstance<RepeaterNode>();
                    break;
                }
            case Nodes.SelectorNode:
                {
                    n = ScriptableObject.CreateInstance<SelectorNode>();
                    break;
                }
            case Nodes.SequenceNode:
                {
                    n = ScriptableObject.CreateInstance<SequenceNode>();
                    break;
                }
            default:
                {
                    return null;
                }
        }
        n.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(n, blackboard);
        EditorUtility.SetDirty(n);
        AssetDatabase.SaveAssets();
        return n;
    }
}
