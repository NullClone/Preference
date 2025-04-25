using System;
using UnityEditor;

namespace Preference.Editor
{
    public static class Preference
    {
        // Properties

        public static Type ProjectWindowType;

        public static Type TreeViewDataType;


        // Methods

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            if (ProjectWindowType == null)
            {
                ProjectWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser");
            }

            if (TreeViewDataType == null)
            {
                TreeViewDataType = typeof(EditorWindow).Assembly.GetType("UnityEditor.IMGUI.Controls.ITreeViewDataSource");
            }


            // Hierarchy

            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Striping.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Striping.OnGUI;

            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Line.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Line.OnGUI;

            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Toggle.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Toggle.OnGUI;


            // ProjectWindow

            EditorApplication.projectWindowItemOnGUI -= Project.Hover.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Hover.OnGUI;

            EditorApplication.projectWindowItemOnGUI -= Project.Striping.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Striping.OnGUI;

            EditorApplication.projectWindowItemOnGUI -= Project.Line.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Line.OnGUI;
        }
    }
}