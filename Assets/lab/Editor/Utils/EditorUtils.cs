using UnityEngine;
using lab;
using System.Collections.Generic;

namespace UnityEditor {
    public static class EditorUtils {

        public static string[] ToArray<T>(this Dictionary<string, T>.KeyCollection collection) {
            string[] keys = new string[collection.Count];
            collection.CopyTo(keys, 0);
            return keys;
        }

        public static string[] ToArray(this IList<AiTree> collection) {
            string[] keys = new string[collection.Count];
            for(int i = 0; i < collection.Count; ++i) {
                keys[i] = string.Format("Tree {0}", i);
            }
            return keys;
        }

        public static GUIContent[] TreesToNames(IList<AiTree> trees) {
            List<GUIContent> names = new List<GUIContent>();
            for (int i = 0; i < trees.Count; ++i) {
                names.Add(new GUIContent(string.Format("Tree {0}", i), (Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/treenode.png")));
            }
            return names.ToArray();
        }

        public static void DrawGrid(Rect position) {
            float num = 0f;
            float num2 = 0f;
            float num3 = num + position.width;
            float num4 = num2 + position.height;
			DrawGridLines(12f, new Color(0.5f, 0.5f, 0.5f, 0.35f), new Vector2(num, num2), new Vector2(num3, num4));
            DrawGridLines(120f, Color.gray, new Vector2(num, num2), new Vector2(num3, num4));
        }

        public static void DrawGridLines(float gridSize, Color color, Vector2 min, Vector2 max) {
            for (float num = min.x - min.x % gridSize; num < max.x; num += gridSize) {
				var start = new Vector2(num, min.y);
				var end = new Vector2(num, max.y);
				DrawLine(start, end, color);
            }
			for (float num2 = min.y - min.y % gridSize; num2 < max.y; num2 += gridSize) {
				var start = new Vector2(min.x, num2);
				var end = new Vector2(max.x, num2);
				DrawLine(start, end, color);
            }
        }

        public static void DrawNodeCurve(Rect start, Rect end) {
            Vector3 startPos = new Vector3(start.x + start.width / 2f, start.y + start.height / 2f, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2f, end.y + end.height / 2f, 0);
			DrawLine(startPos, endPos, Color.white);
			DrawArrow(startPos, endPos, Color.white);
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color) {
            Color shadowCol = color;
			shadowCol.a = 0.35f;
			Handles.color = shadowCol;
			Handles.DrawAAPolyLine(4, new Vector3[] {start, end});
			Handles.color = color;
			Handles.DrawAAPolyLine(2, new Vector3[] {start, end});
        }

		public static void DrawArrow(Vector3 start, Vector3 end, Color color) {
			var arrowHead = new Vector3[3];
			var h = 10f;
			var w = h / 2f;
			var forward = (end - start).normalized;
			var right = Vector3.Cross(Vector3.forward, forward).normalized;

			arrowHead[0] = start + forward * Vector3.Distance(start, end) / 2f + forward * h / 3f * 2f;
			arrowHead[1] = start + forward * Vector3.Distance(start, end) / 2f - forward * h / 3f + right * w;
			arrowHead[2] = start + forward * Vector3.Distance(start, end) / 2f - forward * h / 3f - right * w;

			Handles.color = color;
			Handles.DrawAAConvexPolygon(arrowHead);
		}

        public static void DrawLabel(Vector3 position, string text) {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            Handles.Label(position, text, style);
        }

		public static void ClearConsole() {
			var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
			var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			clearMethod.Invoke(null,null);
		}
    }
}
