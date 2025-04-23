using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Line
    {
        // Properties

        public static Color Color { get; set; } = new(0.37f, 0.37f, 0.37f, 1.0f);

        public static float Thickness { get; set; } = 1.0f;


        // Fields

        static List<int> verticals = new();

        static int prevDepth;


        // Methods

        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint) return;

            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

            if (gameObject == null) return;

            var isLast = IsLast(gameObject.transform);

            // Vertical

            var depth = (int)(selectionRect.x - 60) / 14;

            if (depth > 0)
            {
                selectionRect.width = Thickness;

                for (int i = 0; i < depth; i++)
                {
                    if (verticals.Contains(i)) continue;

                    selectionRect.x = 53 + (i * 14) - (Thickness / 2);
                    selectionRect.height = (isLast && i == depth - 1) ? 8 + (Thickness / 2) : 16;

                    EditorGUI.DrawRect(selectionRect, Color);
                }
            }


            // Horizontal

            if (gameObject.transform.parent != null)
            {
                selectionRect.y += 8 - (Thickness / 2);
                selectionRect.height = Thickness;
                selectionRect.width = gameObject.transform.childCount > 0 ? 8 : 16;

                EditorGUI.DrawRect(selectionRect, Color);
            }

            if (isLast) verticals.Add(depth - 1);

            if (depth < prevDepth) verticals.RemoveAll(i => i >= depth);

            prevDepth = depth;
        }


        static bool IsLast(Transform transform)
        {
            if (transform != null && transform.parent != null)
            {
                var index = transform.parent.childCount - 1;
                var result = transform.parent.GetChild(index);

                return result == transform;
            }

            return false;
        }
    }
}