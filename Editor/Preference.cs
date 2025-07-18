using UnityEditor;

namespace Preference.Editor
{
    public static class Preference
    {
        // Fields

        public const string HierarchyStripingMenuPath = "Tools/Preference/Hierarchy/Striping";
        public const string HierarchyLineMenuPath = "Tools/Preference/Hierarchy/Line";
        public const string HierarchyToggleMenuPath = "Tools/Preference/Hierarchy/Toggle";
        public const string ProjectHoverMenuPath = "Tools/Preference/Project/Hover";
        public const string ProjectStripingMenuPath = "Tools/Preference/Project/Striping";
        public const string ProjectLineMenuPath = "Tools/Preference/Project/Line";

        public static bool HierarchyStripingFlag = true;
        public static bool HierarchyLineFlag = true;
        public static bool HierarchyToggleFlag = true;
        public static bool ProjectHoverFlag = true;
        public static bool ProjectStripingFlag = true;
        public static bool ProjectLineFlag = true;


        // Methods

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // Hierarchy

            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Striping.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Striping.OnGUI;

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


            HierarchyStripingFlag = string.Equals(EditorUserSettings.GetConfigValue(HierarchyStripingMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
            HierarchyLineFlag = string.Equals(EditorUserSettings.GetConfigValue(HierarchyLineMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
            HierarchyToggleFlag = string.Equals(EditorUserSettings.GetConfigValue(HierarchyToggleMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
            ProjectHoverFlag = string.Equals(EditorUserSettings.GetConfigValue(ProjectHoverMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
            ProjectStripingFlag = string.Equals(EditorUserSettings.GetConfigValue(ProjectStripingMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
            ProjectLineFlag = string.Equals(EditorUserSettings.GetConfigValue(ProjectLineMenuPath), true.ToString(), System.StringComparison.OrdinalIgnoreCase);
        }

        [MenuItem(HierarchyStripingMenuPath)]
        private static void HierarchyStriping() => SetMenuChecked(HierarchyStripingMenuPath, ref HierarchyStripingFlag);

        [MenuItem(HierarchyStripingMenuPath, validate = true)]
        private static bool HierarchyStripingValidator()
        {
            Menu.SetChecked(HierarchyStripingMenuPath, HierarchyStripingFlag);

            return true;
        }

        [MenuItem(HierarchyLineMenuPath)]
        private static void HierarchyLine() => SetMenuChecked(HierarchyLineMenuPath, ref HierarchyLineFlag);

        [MenuItem(HierarchyLineMenuPath, validate = true)]
        private static bool HierarchyLineValidator()
        {
            Menu.SetChecked(HierarchyLineMenuPath, HierarchyLineFlag);

            return true;
        }

        [MenuItem(HierarchyToggleMenuPath)]
        private static void HierarchyToggle() => SetMenuChecked(HierarchyToggleMenuPath, ref HierarchyToggleFlag);

        [MenuItem(HierarchyToggleMenuPath, validate = true)]
        private static bool HierarchyToggleValidator()
        {
            Menu.SetChecked(HierarchyToggleMenuPath, HierarchyToggleFlag);

            return true;
        }

        [MenuItem(ProjectHoverMenuPath)]
        private static void ProjectHover() => SetMenuChecked(ProjectHoverMenuPath, ref ProjectHoverFlag);

        [MenuItem(ProjectHoverMenuPath, validate = true)]
        private static bool ProjectHoverValidator()
        {
            Menu.SetChecked(ProjectHoverMenuPath, ProjectHoverFlag);

            return true;
        }

        [MenuItem(ProjectStripingMenuPath)]
        private static void ProjectStriping() => SetMenuChecked(ProjectStripingMenuPath, ref ProjectStripingFlag);

        [MenuItem(ProjectStripingMenuPath, validate = true)]
        private static bool ProjectStripingValidator()
        {
            Menu.SetChecked(ProjectStripingMenuPath, ProjectStripingFlag);

            return true;
        }

        [MenuItem(ProjectLineMenuPath)]
        private static void ProjectLine() => SetMenuChecked(ProjectLineMenuPath, ref ProjectLineFlag);

        [MenuItem(ProjectLineMenuPath, validate = true)]
        private static bool ProjectLineValidator()
        {
            Menu.SetChecked(ProjectLineMenuPath, ProjectLineFlag);

            return true;
        }

        private static void SetMenuChecked(string menuPath, ref bool value)
        {
            value = Menu.GetChecked(menuPath);

            value = !value;

            Menu.SetChecked(menuPath, value);

            EditorUserSettings.SetConfigValue(menuPath, value.ToString());

            EditorApplication.RepaintHierarchyWindow();
            EditorApplication.RepaintProjectWindow();
        }
    }
}