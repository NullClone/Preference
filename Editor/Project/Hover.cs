using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Preference.Editor.Project
{
    public static class Hover
    {
        private static EditorWindow ProjectBrowser;


        public static void OnGUI(string guid, Rect selectionRect)
        {
            if (Preference.Flag == false) return;

            Execute();

            Draw(selectionRect);
        }

        public static void Draw(Rect selectionRect)
        {
            if (selectionRect.height == 16)
            {
                selectionRect.width += selectionRect.x;
                selectionRect.x = 0;
            }

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
            if (ProjectBrowser == null)
            {
                var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser");

                ProjectBrowser = EditorWindow.GetWindow(type);
            }

            ProjectBrowser.rootVisualElement.parent.RegisterCallback<MouseMoveEvent, EditorWindow>(Callback, ProjectBrowser);
        }

        private static void Callback(MouseMoveEvent moveEvent, EditorWindow window) => window.Repaint();
    }
}