using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Line
    {
        // Fields

        private static int PrevRowDepth;

        private static bool IsFirstRowDrawn;

        private static readonly List<int> VerticalGaps = new();


        // Methods

        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Preference.HierarchyLineFlag == false) return;

            if (Event.current.type == EventType.Repaint)
            {
                var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

                if (gameObject == null) return;

                Draw(gameObject, selectionRect);
            }
            else
            {
                IsFirstRowDrawn = false;
            }
        }

        public static void Draw(GameObject gameObject, Rect selectionRect)
        {
            var lineThickness = 1f;

            var color = new Color(
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f);

            var isLast = IsLastChild(gameObject.transform);
            var hasChilren = HasChilren(gameObject.transform);

            var depth = Mathf.RoundToInt((selectionRect.x - 60) / 14);


            // Before

            if (IsFirstRowDrawn == false)
            {
                VerticalGaps.Clear();

                var curTransform = gameObject.transform.parent;
                var curDepth = depth - 1;

                while (curTransform != null && curTransform.parent != null)
                {
                    if (isLast) VerticalGaps.Add(curDepth - 1);

                    curTransform = curTransform.parent;
                    curDepth--;
                }
            }


            // Vertical            

            for (int i = 0; i < depth; i++)
            {
                if (VerticalGaps.Contains(i)) continue;

                var rect = selectionRect;

                rect.x = 53 + (i * 14) - (lineThickness / 2);
                rect.width = lineThickness;
                rect.height = (isLast && i == depth - 1) ? 8 + (lineThickness / 2) : 16;

                EditorGUI.DrawRect(rect, color);
            }


            // Horizontal

            if (depth > 0)
            {
                var rect = selectionRect;

                rect.x -= 21;
                rect.y += rect.height / 2;
                rect.height = lineThickness;
                rect.y -= rect.height / 2;
                rect.width = hasChilren ? 7 : 17;

                EditorGUI.DrawRect(rect, color);
            }


            // After

            if (isLast)
            {
                VerticalGaps.Add(depth - 1);
            }

            if (depth < PrevRowDepth)
            {
                VerticalGaps.RemoveAll(i => i >= depth);
            }

            PrevRowDepth = depth;
            IsFirstRowDrawn = true;
        }


        private static bool IsLastChild(Transform transform)
        {
            if (transform != null && transform.parent != null)
            {
                var index = transform.parent.childCount - 1;
                var result = transform.parent.GetChild(index);

                return result == transform;
            }

            return false;
        }

        private static bool HasChilren(Transform transform)
        {
            if (transform != null)
            {
                return transform.childCount > 0;
            }

            return false;
        }
    }
}