using Newtonsoft.Json;
using System.IO;
using System.Net;
using System;

public static class CheckFileForUpdating
{
    public static bool IsUpdate()
    {
        bool res = false;

        try
        {
            using (WebClient webClient = new WebClient())
            {
                Stream stream = webClient.OpenRead("https://firebasestorage.googleapis.com/v0/b/tower-defend-3d-unity-84f17.appspot.com/o/");
                if (stream.CanRead)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    FileData fileData = JsonConvert.DeserializeObject<FileData>(streamReader.ReadToEnd());

                    foreach (var item in fileData.Items)
                    {
                        Console.WriteLine(item.Name);

                        if (item.Name.Contains(FileLocalLink.NameFolderNodeBuilding) && !File.Exists(FileLocalLink.UserRootLocal + "/" + item.Name))
                        {
                            res = true;
                            break;
                        }
                        if (item.Name.Contains(FileLocalLink.NameFolderNodePath) && !File.Exists(FileLocalLink.UserRootLocal + "/" + item.Name))
                        {
                            res = true;
                            break;
                        }
                    }
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return res;
    }

    public static bool IsNullLocalFolder(string name)
    {
        if (!Directory.Exists(FileLocalLink.UserFolderNodePath + name) || !Directory.Exists(FileLocalLink.UserFolderNodeBuilding + name))
            return true;
        if (Directory.GetFiles(FileLocalLink.UserFolderNodePath + name).Length == 0 || Directory.GetFiles(FileLocalLink.UserFolderNodeBuilding + name).Length == 0)
            return true;
        else return false;
    }
}
