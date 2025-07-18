using Preference.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Preference.Project
{
    public static class Line
    {
        // Fields

        private static int PrevRowDepth;

        private static bool IsFirstRowDrawn;

        private static readonly List<int> VerticalGaps = new();


        // Properties

        private static EditorWindow Window;

        private static MethodInfo Data;

        private static bool isTwoColumns;

        private static object data;


        // Methods

        public static void OnGUI(string _, Rect selectionRect)
        {
            if (Preference.ProjectLineFlag == false) return;

            if (Window == null)
            {
                var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser");

                Window = EditorWindow.GetWindow(type);
            }

            if (Data == null)
            {
                var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.IMGUI.Controls.ITreeViewDataSource");

                Data = type.GetMethod("GetItem");
            }

            if (Event.current.type == EventType.Repaint)
            {
                if (selectionRect.x == 14 || selectionRect.height != 16) return;

                UpdateState();

                var offest = isTwoColumns ? -15 : -4;

                if ((selectionRect.y + offest) % 16 == 0)
                {
                    var rowIndex = (int)((selectionRect.y + offest) / 16);

                    if (rowIndex < 0 || data == null) return;

                    var tree = (TreeViewItem)Data.Invoke(data, new object[] { rowIndex });

                    if (tree == null) return;

                    Draw(tree, selectionRect);
                }
            }
            else
            {
                IsFirstRowDrawn = false;
            }
        }

        public static void Draw(TreeViewItem tree, Rect selectionRect)
        {
            var lineThickness = 1f;

            var color = new Color(
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f,
                EditorGUIUtility.isProSkin ? 0.35f : 0.55f);

            var isLast = tree.parent?.children?.LastOrDefault() == tree;

            var depth = Mathf.RoundToInt((selectionRect.x - 16) / 14);


            // Before

            if (IsFirstRowDrawn == false)
            {
                VerticalGaps.Clear();

                var curTransform = tree.parent;
                var curDepth = depth - 1;

                while (curTransform != null && curTransform.parent != null)
                {
                    if (isLast) VerticalGaps.Add(curDepth - 1);

                    curTransform = curTransform.parent;
                    curDepth--;
                }
            }


            // Vertical            

            for (int i = 1; i < depth; i++)
            {
                if (VerticalGaps.Contains(i)) continue;

                var rect = selectionRect;

                rect.x = 9 + (i * 14) - (lineThickness / 2);
                rect.width = lineThickness;
                rect.height = (isLast && i == depth - 1) ? 8 + (lineThickness / 2) : 16;

                EditorGUI.DrawRect(rect, color);
            }


            // Horizontal

            if (depth > 1)
            {
                var rect = selectionRect;

                rect.x -= 21;
                rect.y += rect.height / 2;
                rect.height = lineThickness;
                rect.y -= rect.height / 2;
                rect.width = tree.hasChildren ? 7 : 17;

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

        public static void UpdateState()
        {
            if (Window == null) return;

            var viewMode = Window.GetFieldValue("m_ViewMode");

            isTwoColumns = (int)viewMode == 1;

            var treeView = Window.GetFieldValue(isTwoColumns ? "m_FolderTree" : "m_AssetTree");

            data = treeView.GetPropertyValue("data");

            EditorApplication.delayCall -= UpdateState;
            EditorApplication.delayCall += UpdateState;
        }
    }
}