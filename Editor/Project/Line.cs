using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Preference.Editor.Project
{
    public static class Line
    {
        // Fields

        static readonly EditorWindow Window = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser"));
        static readonly MethodInfo Data = typeof(EditorWindow).Assembly.GetType("UnityEditor.IMGUI.Controls.ITreeViewDataSource").GetMethod("GetItem");

        static object data;
        static bool isTwoColumns;

        static List<int> VerticalGaps = new List<int>();

        static int PrevRowDepth;

        static bool IsFirstRowDrawn;


        // Methods

        public static void OnGUI(string guid, Rect selectionRect)
        {
            if (Event.current.type == EventType.Repaint)
            {
                if (selectionRect.height > 16) return;

                var viewMode = Window.GetFieldValue("m_ViewMode");

                isTwoColumns = (int)viewMode == 1;

                var treeView = Window.GetFieldValue(isTwoColumns ? "m_FolderTree" : "m_AssetTree");

                data = treeView.GetPropertyValue("data");

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

            for (int i = 0; i < depth; i++)
            {
                if (VerticalGaps.Contains(i)) continue;

                var rect = selectionRect;

                rect.x = 9 + (i * 14) - (lineThickness / 2);
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
    }

    static class Field
    {
        static readonly Dictionary<string, FieldInfo> FieldInfoCache = new();

        static readonly Dictionary<string, PropertyInfo> PropertyInfoCache = new();


        public static object GetFieldValue(this object value, string name)
        {
            var type = value.GetType();

            FieldInfo fieldInfo;

            if (FieldInfoCache.TryGetValue(name, out var result))
            {
                fieldInfo = result;
            }
            else
            {
                fieldInfo = FieldInfoCache[name] = type.GetField(name, (BindingFlags)62);
            }

            return fieldInfo.GetValue(value);
        }

        public static object GetPropertyValue(this object value, string name)
        {
            var type = value.GetType();

            PropertyInfo propertyInfo;

            if (PropertyInfoCache.TryGetValue(name, out var result))
            {
                propertyInfo = result;
            }
            else
            {
                propertyInfo = PropertyInfoCache[name] = type.GetProperty(name, (BindingFlags)62);
            }

            return propertyInfo.GetValue(value);
        }
    }
}