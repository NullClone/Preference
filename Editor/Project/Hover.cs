using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Preference.Editor.Project
{
    public static class Hover
    {
        // Fields

        private static readonly HashSet<EditorWindow> SubscribedWindowsCache = new();


        // Methods

        public static void OnGUI(string guid, Rect selectionRect)
        {
            if (Preference.Flag == false) return;

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


        private static void Execute()
        {
            var window = EditorWindow.mouseOverWindow;

            if (window == null) return;

            var type = window.GetType();

            if (type != null && type.Name == "ProjectBrowser")
            {
                if (SubscribedWindowsCache.Contains(window)) return;

                window.rootVisualElement.parent.RegisterCallback<MouseMoveEvent, EditorWindow>(Callback, window);

                SubscribedWindowsCache.Add(window);

                window.Repaint();
            }
        }

        private static void Callback(MouseMoveEvent moveEvent, EditorWindow window) => window.Repaint();
    }
}