using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Preference.Editor.Project
{
    public static class Hover
    {
        // Fields

        static readonly HashSet<EditorWindow> SubscribedWindowsCache = new();


        // Methods

        public static void OnGUI(string guid, Rect selectionRect)
        {
            Execute();

            selectionRect.width += selectionRect.x;
            selectionRect.x = 0;

            if (selectionRect.Contains(Event.current.mousePosition))
            {
                var color = new Color(
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    0.06f);

                EditorGUI.DrawRect(selectionRect, color);
            }
        }


        static void Execute()
        {
            if (EditorWindow.mouseOverWindow == null) return;
            if (EditorWindow.mouseOverWindow.rootVisualElement == null) return;
            if (EditorWindow.mouseOverWindow.rootVisualElement.parent == null) return;

            var type = EditorWindow.mouseOverWindow.GetType();

            if (type != null && type == Preference.ProjectWindowType)
            {
                if (SubscribedWindowsCache.Contains(EditorWindow.mouseOverWindow)) return;

                EditorWindow.mouseOverWindow.rootVisualElement.parent
                    .RegisterCallback<MouseMoveEvent, EditorWindow>(Callback, EditorWindow.mouseOverWindow);

                SubscribedWindowsCache.Add(EditorWindow.mouseOverWindow);

                EditorWindow.mouseOverWindow.Repaint();
            }
        }

        static void Callback(MouseMoveEvent moveEvent, EditorWindow window) => window.Repaint();
    }
}