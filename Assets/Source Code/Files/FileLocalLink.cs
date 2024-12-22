using Unity.VisualScripting;

public static class FileLocalLink
{
    public static string UserRootLocal = "C:/Tower Defend 3D";
    public static string DesignerRootLocal = "F:/Tower Defend 3D";

    public static string NameFolderNodePath = "FileNodePath";
    public static string NameFolderNodeBuilding = "FileNodeBuilding";

    public static string UserFolderNodePath = $"{UserRootLocal}/{NameFolderNodePath}/";
    public static string UserFolderNodeBuilding = $"{UserRootLocal}/{NameFolderNodeBuilding}/";

    public static string DesignerFolderNodePath = $"{DesignerRootLocal}/{NameFolderNodePath}/";
    public static string DesignerFolderNodeBuilding = $"{DesignerRootLocal}/{NameFolderNodeBuilding}/";
}
