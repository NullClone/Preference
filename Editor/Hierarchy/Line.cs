using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Line
    {
        // Properties

        public static float Thickness { get; set; } = 1f;


        // Fields

        static readonly List<int> VerticalGaps = new();

        static int PrevRowDepth;

        static bool IsFirstRowDrawn;


        // Methods

        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint)
            {
                IsFirstRowDrawn = false;

                return;
            }

            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

            if (gameObject == null) return;

            var color = new Color(
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f);

            var isLast = IsLastChild(gameObject.transform);
            var hasChilren = HasChilren(gameObject.transform);

            var depth = Mathf.RoundToInt((selectionRect.x - 60) / 14);

            // Before

            if (!IsFirstRowDrawn)
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

                rect.x = 53 + (i * 14) - (Thickness / 2);
                rect.width = Thickness;
                rect.height = (isLast && i == depth - 1) ? 8 + (Thickness / 2) : 16;

                EditorGUI.DrawRect(rect, color);
            }


            // Horizontal

            if (depth > 0)
            {
                var rect = selectionRect;

                rect.x -= 21;
                rect.y += rect.height / 2;
                rect.height = Thickness;
                rect.y -= rect.height / 2;
                rect.width = hasChilren ? 7 : 17;

                EditorGUI.DrawRect(rect, color);
            }


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


        static bool IsLastChild(Transform transform)
        {
            if (transform != null && transform.parent != null)
            {
                var index = transform.parent.childCount - 1;
                var result = transform.parent.GetChild(index);

                return result == transform;
            }

            return false;
        }

        static bool HasChilren(Transform transform)
        {
            if (transform != null)
            {
                return transform.childCount > 0;
            }

            return false;
        }
    }
}