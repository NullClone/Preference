using UnityEditor;

namespace Preference.Editor
{
    public static class Preference
    {
        [InitializeOnLoadMethod]
        static void Initialize()
        {
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