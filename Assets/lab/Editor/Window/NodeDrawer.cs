using UnityEngine;
using UnityEditor;
using System;

namespace lab.EditorView {
    public class NodeDrawer {

        public delegate void NodeDrawerHandler(ANode node);
        public static NodeDrawerHandler OnRightClicked = delegate { };
        public static NodeDrawerHandler OnLeftClicked = delegate { };
        public static Action<ANode> OnDuplicate = delegate { };
        public static Action<ANode> OnDelete = delegate { };

        public static readonly Vector2 gSize = new Vector2(144f, 32f);

        private static int gNodeDrawerIdIncremental = 0;

        private readonly string _rootStyle = "flow node 5";
        private readonly string _rootStyleOn = "flow node 5 on";
        private readonly string _defaultStyle = "flow node 0";
        private readonly string _defaultStyleOn = "flow node 0 on";
        private readonly string _defaultIconPath = "Assets/lab/Icons/16x16/triangle_black.png";
        private readonly string _passIconPath = "Assets/lab/Icons/16x16/triangle_green.png";
        private readonly string _failIconPath = "Assets/lab/Icons/16x16/triangle_red.png";

        private ANode _node;
        private int _id;
        private Rect _rect;
        private bool _isRoot = false;
        private string _currentIcon;

        public ANode Node {
            get { return _node; }
        }

        public bool IsRoot {
            get { return _isRoot; }
            set { _isRoot = value; }
        }

        public void SetOffset(Vector2 offset) {
            _node.Position += offset;
        }

        public NodeDrawer(ANode node, bool isRoot = false) {
            _id = gNodeDrawerIdIncremental++;
            _node = node;
            _isRoot = isRoot;
            _rect = new Rect(0f, 0f, gSize.x, gSize.y);
            _currentIcon = _defaultIconPath;
        }

        public void ResetDebugInfo() {
            _currentIcon = _defaultIconPath;
        }

        public void SetDebugInfo(bool result) {
            _currentIcon = result ? _passIconPath : _failIconPath;
        }

        public void DrawNode() {
            _rect.x = _node.Position.x;
            _rect.y = _node.Position.y;
            _rect = GUI.Window(_id, _rect, new GUI.WindowFunction(DrawNodeWindow), "", (Selection.activeObject != _node) ? (_isRoot ? _rootStyle : _defaultStyle) : (_isRoot ? _rootStyleOn : _defaultStyleOn));
            _node.Position.x = _rect.x;
            _node.Position.y = _rect.y;
        }

        private void DrawNodeWindow(int id) {
            Event e = Event.current;
            if (_node == null) {
                return;
            }
            GUI.Label(new Rect(0, gSize.y / 2f - 12, _rect.width, 24), new GUIContent(_node.ToString(), EditorGUIUtility.ObjectContent(_node, _node.GetType()).image));
            GUI.Label(new Rect(gSize.x - 14f, -1f, 16f, 16f), new GUIContent((Texture2D)EditorGUIUtility.Load(_currentIcon)));
            if (e.type == EventType.MouseUp && e.button == 1) {
                OnRightClicked(_node);
                e.Use();
            }
            if (e.type == EventType.MouseDown && e.button == 0) {
                OnLeftClicked(_node);
            }
            if(_node != null) {
                EditorUtility.SetDirty(_node);
            }
            GUI.DragWindow();
            Shortcuts(e);
        }

        private void Shortcuts(Event e) {
            if(e.rawType == EventType.keyUp) {
                if(e.control && e.keyCode == KeyCode.D) {
                    OnDuplicate(_node);   
                } else if(e.keyCode == KeyCode.Delete) {
                    OnDelete(_node);
                }
            }
        }
    }
}
